using DiarioAcademia.Dominio;
using System;
using System.Collections.Generic;
using System.Data;

namespace DiarioAcademia.Infraestrutura
{
    public class ProvaDao
    {
        public List<Prova> SelecionarProvasSemFeedback()
        {
            string sql = "SELECT ID, DATA, ASSUNTO, FEEDBACKREALIZADO, GABARITO FROM TBPROVA WHERE FEEDBACKREALIZADO=FALSE";

            return Db.GetAll(sql, ConverterProva);
        }

        public void Salvar(Prova prova) 
        {            
            string sql = "INSERT INTO TBPROVA (DATA, ASSUNTO, FEEDBACKREALIZADO, GABARITO) VALUES ({0}DATA, {0}ASSUNTO, {0}FEEDBACKREALIZADO, {0}GABARITO)";
            
            Db.Insert(sql, Parametros(prova));
        }

        public void Atualizar(Prova prova) 
        {
            string sql = "UPDATE TBPROVA SET DATA={0}DATA, ASSUNTO={0}ASSUNTO, FEEDBACKREALIZADO={0}FEEDBACKREALIZADO, GABARITO={0}GABARITO)";

            Db.Update(sql, Parametros(prova));
        }

        #region métodos privados
        private object[] Parametros(Prova prova)
        {
            return new Object[]
            {
                "ID", prova.Id, 
                "DATA", prova.Data, 
                "ASSUNTO", prova.Assunto, 
                "FEEDBACKREALIZADO", prova.FeedbackRealizado, 
                "GABARITO", prova.Gabarito
            };
        }

        private Prova ConverterProva(IDataReader reader)
        {
            int id = Convert.ToInt32(reader["ID"]);
            DateTime data = Convert.ToDateTime(reader["DATA"]);
            string assunto = Convert.ToString(reader["ASSUNTO"]);
            bool feedbackRealizado = Convert.ToBoolean(reader["FEEDBACKREALIZADO"]);
            Gabarito gabarito = new Gabarito(Convert.ToString(reader["GABARITO"]));

            Prova prova = new Prova(data, gabarito);
            prova.Id = id;
            prova.FeedbackRealizado = feedbackRealizado;
            prova.Assunto = assunto;

            return prova;
        }
        #endregion
    }
}