using DiarioAcademia.Dominio.AvaliacaoModule;
using DiarioAcademia.Infra.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace DiarioAcademia.Infra.AvaliacaoModule.Dao
{
    public class ProvaDao
    {
        private DbTransaction _transacao;

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

        public void Salvar(Prova prova)
        {
            string sql = "INSERT INTO TBPROVA (DATA, ASSUNTO, FEEDBACK_REALIZADO, GABARITO) VALUES ({0}DATA, {0}ASSUNTO, {0}FEEDBACK_REALIZADO, {0}GABARITO)";

            prova.Id = Db.Insert(sql, Parametros(prova), _transacao);
        }

        public virtual void Atualizar(Prova prova)
        {
            string sql = "UPDATE TBPROVA SET DATA={0}DATA, ASSUNTO={0}ASSUNTO, FEEDBACK_REALIZADO={0}FEEDBACK_REALIZADO, GABARITO={0}GABARITO WHERE ID={0}ID";
            
            Db.Update(sql, Parametros(prova), _transacao);
        }

        #region métodos privados

        private void CarregarNotas(ref List<Prova> provas)
        {
            string sqlNotasAlunos = "SELECT P.ID ID_NOTA, P.VALOR VALOR_NOTA, A.ID ID_ALUNO, A.NOME NOME_ALUNO FROM TBNOTA P INNER JOIN TBALUNO A ON A.ID = P.ID_ALUNO WHERE P.ID_PROVA = {0}PROVA_ID";

            foreach (Prova prova in provas)
            {
                List<NotaProva> notas = Db.GetAll(sqlNotasAlunos, ConverterNotaAluno, new object[] { "PROVA_ID", prova.Id });

                foreach (NotaProva nota in notas)
                {
                    prova.LancarNota(nota);
                }
            }
        }
       
        private object[] Parametros(Prova prova)
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

        private NotaProva ConverterNotaAluno(IDataReader reader)
        {
            int idNota = Convert.ToInt32(reader["ID_NOTA"]);
            double valorNota = Convert.ToDouble(reader["VALOR_NOTA"]);

            int idAluno = Convert.ToInt32(reader["ID_ALUNO"]);
            string nomeAluno = Convert.ToString(reader["NOME_ALUNO"]);

            NotaProva nota = new NotaProva(valorNota, new Aluno(idAluno, nomeAluno));

            return nota;
        }

        #endregion


    }
}