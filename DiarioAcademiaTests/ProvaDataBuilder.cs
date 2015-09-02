using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio.Tests
{
    public class ProvaDataBuilder
    {

        Prova prova = null;

        public ProvaDataBuilder NaData(DateTime data)
        {
            prova = new Prova(data);

            return this;
        }

        public ProvaDataBuilder ComNotaDe(Aluno maria, int nota)
        {
            prova.LancarNota(nota, maria);

            return this;
        }

        public Prova Build()
        {
            return prova;
        }
    }
}
