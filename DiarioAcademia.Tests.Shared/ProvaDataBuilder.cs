using DiarioAcademia.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Tests.Shared
{
    public class ProvaDataBuilder
    {

        Prova prova = null;

        public ProvaDataBuilder Sobre(string assunto)
        {
            prova = new Prova( DateTime.Now );

            prova.Assunto = assunto;

            return this;
        }

        public ProvaDataBuilder NaData(DateTime data)
        {
            prova.Data = data;

            return this;
        }

        public ProvaDataBuilder ComNotaDe(Aluno aluno, double nota)
        {
            prova.LancarNota(new Nota(nota, aluno));

            return this;
        }

        public Prova Build()
        {
            return prova;
        }

       
    }
}
