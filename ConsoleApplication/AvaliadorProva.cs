using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Dominio
{
    public class AvaliadorProva
    {
        private double _maiorNota = double.MinValue;
        private double _menorNota = double.MaxValue;

        public double ObtemMaiorNota()
        {
            return _maiorNota;
        }

        public double ObtemMenorNota()
        {
            return _menorNota;
        }

        public void Avaliar(Prova prova)
        {
            foreach (Nota nota in prova.Notas)
            {
                if (nota.Valor > _maiorNota)
                {
                    _maiorNota = nota.Valor;
                }
                if (nota.Valor < _menorNota)
                {
                    _menorNota = nota.Valor;
                }
            }
        }


    }
}
