using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class NenhumaProvaEncontradaException : Exception
    {
        private int _mes;
        private int _ano;

        public NenhumaProvaEncontradaException(int mes, int ano)
        {
            // TODO: Complete member initialization
            this._mes = mes;
            this._ano = ano;
        }
    }
}
