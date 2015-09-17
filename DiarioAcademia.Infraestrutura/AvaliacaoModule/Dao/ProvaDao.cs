using DiarioAcademia.Dominio.AvaliacaoModule;
using DiarioAcademia.Infra.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DiarioAcademia.Infra.AvaliacaoModule.Dao
{
    public class ProvaDao
    {
        public virtual List<Prova> SelecionarProvasPendentesFeedback(int mes, int ano)
        {
            string sqlProvas = "SELECT ID, DATA, ASSUNTO, FEEDBACK_REALIZADO, GABARITO FROM TBPROVA " + 
                "WHERE FEEDBACK_REALIZADO=0 AND (DATEPART(MONTH, DATA) = {0}MONTH AND DATEPART(YEAR, DATA) = {0}YEAR)";

            List<Prova> provas = Db.GetAll(sqlProvas, ConverterProva, new object[] { "MONTH", mes, "YEAR" , ano });

            CarregarNotas(ref provas);

            return provas;
        }

        public List<Prova> SelecionarProvasFeedbackRealizado(int mes, int ano)
        {
            string sqlProvas = "SELECT ID, DATA, ASSUNTO, FEEDBACK_REALIZADO, GABARITO FROM TBPROVA " +
                "WHERE FEEDBACK_REALIZADO=1 AND (DATEPART(MONTH, DATA) = {0}MONTH AND DATEPART(YEAR, DATA) = {0}YEAR)";

            List<Prova> provas = Db.GetAll(sqlProvas, ConverterProva, new object[] { "MONTH", mes, "YEAR", ano });

            CarregarNotas(ref provas);

            return provas;
        }
      
        public virtual void CancelarFeedback(List<Prova> provas)
        {
            List<int> ids = provas.Select(x => x.Id).ToList();

            var parms = ids.Select((s, i) => "p" + i.ToString()).ToArray();

            string sql = string.Format("UPDATE TBPROVA SET FEEDBACK_REALIZADO=0 WHERE ID IN ({0})", ConfiguraFiltroIn(parms));

            var parmsInClause = new List<object>();

            for (int i = 0; i < parms.Length; i++)
            {
                parmsInClause.Add(parms[i]);
                parmsInClause.Add(ids[i]);
            }

            Db.Update(sql, parmsInClause.ToArray());
        }      

        public void Salvar(Prova prova)
        {
            string sqlProvas = "INSERT INTO TBPROVA (DATA, ASSUNTO, FEEDBACK_REALIZADO, GABARITO) VALUES ({0}DATA, {0}ASSUNTO, {0}FEEDBACK_REALIZADO, {0}GABARITO)";

            prova.Id = Db.Insert(sqlProvas, ParametrosProva(prova));

            string sqlNotas = "INSERT INTO TBNOTA (ID_PROVA, ID_ALUNO, VALOR) VALUES ({0}ID_PROVA, {0}ID_ALUNO, {0}VALOR) ";

            foreach (NotaProva nota in prova.Notas)
            {
                nota.Prova = prova;

                Db.Insert(sqlNotas, ParametrosNota(nota));
            }
        }

        public virtual void Atualizar(Prova prova)
        {
            string sql = "UPDATE TBPROVA SET DATA={0}DATA, ASSUNTO={0}ASSUNTO, FEEDBACK_REALIZADO={0}FEEDBACK_REALIZADO, GABARITO={0}GABARITO WHERE ID={0}ID";
            
            Db.Update(sql, ParametrosProva(prova));
        }

         

        #region métodos privados

        private void CarregarNotas(ref List<Prova> provas)
        {
            string sqlNotasAlunos = "SELECT P.ID ID_NOTA, P.VALOR VALOR_NOTA, A.ID ID_ALUNO, A.NOME NOME_ALUNO FROM TBNOTA P INNER JOIN TBALUNO A ON A.ID = P.ID_ALUNO WHERE P.ID_PROVA = {0}PROVA_ID";

            foreach (Prova prova in provas)
            {
                List<NotaProva> notas = Db.GetAll(sqlNotasAlunos, ConverterNota, new object[] { "PROVA_ID", prova.Id });

                foreach (NotaProva nota in notas)
                {
                    prova.LancarNota(nota);
                }
            }
        }
       
        private object[] ParametrosProva(Prova prova)
        {
            return new Object[]
            {
                "ID", prova.Id, 
                "DATA", prova.Data, 
                "ASSUNTO", prova.Assunto, 
                "FEEDBACK_REALIZADO", (int)prova.Feedback, 
                "GABARITO", prova.Gabarito == null ? "" : prova.Gabarito.ToString()
            };
        }

        private object[] ParametrosNota(NotaProva nota)
        {
            return new Object[]
            {
                "ID", nota.Id, 
                "ID_PROVA", nota.Prova.Id, 
                "ID_ALUNO", nota.Aluno.Id,
                "VALOR", nota.Valor
            };
        }

        private Prova ConverterProva(IDataReader reader)
        {
            int id = Convert.ToInt32(reader["ID"]);
            DateTime data = Convert.ToDateTime(reader["DATA"]);
            string assunto = Convert.ToString(reader["ASSUNTO"]);

            FeedbackEnum feedback;
            Enum.TryParse(reader["FEEDBACK_REALIZADO"].ToString(), out feedback);
           
            GabaritoProva gabarito = new GabaritoProva(Convert.ToString(reader["GABARITO"]));

            Prova prova = new Prova(data, gabarito);
            prova.Id = id;
            prova.Feedback = feedback;
            prova.Assunto = assunto;

            return prova;
        }

        private NotaProva ConverterNota(IDataReader reader)
        {
            int idNota = Convert.ToInt32(reader["ID_NOTA"]);
            double valorNota = Convert.ToDouble(reader["VALOR_NOTA"]);

            int idAluno = Convert.ToInt32(reader["ID_ALUNO"]);
            string nomeAluno = Convert.ToString(reader["NOME_ALUNO"]);

            NotaProva nota = new NotaProva(valorNota, new Aluno(idAluno, nomeAluno));

            return nota;
        }

        private static string ConfiguraFiltroIn(string[] parms)
        {
            var inclause = string.Join(",", parms);

            inclause = inclause.Replace(",", ", {0}");

            if (!inclause.StartsWith("{0}"))
                inclause = "{0}" + inclause;

            return inclause;
        }

        #endregion
              
    }
}