using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Avaliacao
    {
        private double _maiorNota = double.MinValue;

        private List<Nota> _notas = new List<Nota>();

        public Avaliacao(DateTime dataAvaliacao)
        {
            Data = dataAvaliacao;
        }

        public DateTime Data { get; private set; }
       
        public void LancarNota(double notaAvalicao, Aluno aluno)
        {
            Nota nota = new Nota(notaAvalicao, aluno);

            _notas.Add(nota);
        }

        /* Notas baixas e notas altas
        public List<Nota> ObtemNotasBaixas()
        {
            return Notas.FindAll(n => n.Valor < 5);
        }

        public List<Nota> ObtemNotasBoas()
        {
            return Notas.FindAll(n => n.Valor >= 9);
        }
         */

        public double ObtemMaiorNota()
        {
            foreach (Nota nota in _notas)
            {
                if (nota.Valor > _maiorNota)
                {
                    _maiorNota = nota.Valor;
                }
            }

            return _maiorNota;
        }
    }
}
