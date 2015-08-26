using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    public class AvaliadorProva
    {
        private double _maiorNota = double.MinValue;

        public double ObtemMaiorNota()
        {
            return _maiorNota;
        }

        public void Avaliar(Prova prova)
        {
            foreach (Nota nota in prova.Notas)
            {
                if (nota.Valor > _maiorNota)
                {
                    _maiorNota = nota.Valor;
                }
            }
        }


    }
}
