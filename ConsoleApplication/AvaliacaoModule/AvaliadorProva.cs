using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class AvaliadorProva
    {
        private double _maiorNota = double.MinValue;
        private double _menorNota = double.MaxValue;
        private double _mediaNotas = 0;
        
        public List<NotaProva> MelhoresNotas { get; private set; }

        public List<NotaProva> PioresNotas { get; private set; }

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
            if (!prova.Notas.Any())
                throw new InvalidOperationException("Não é possível avaliar uma prova sem notas");

            double somaNotas = 0;

            foreach (NotaProva nota in prova.Notas)
            {
                if (nota.Valor > _maiorNota)
                {
                    _maiorNota = nota.Valor;
                }
                if (nota.Valor < _menorNota)
                {
                    _menorNota = nota.Valor;
                }

                somaNotas += nota.Valor;
            }

            CalcularMedia(prova.Notas.Count, somaNotas);

            PioresNotas = ObtemPioresNotas(prova);
        }

        private List<NotaProva> ObtemPioresNotas(Prova prova)
        {
            return prova.Notas
                .OrderBy(x => x.Valor)
                .ToList()
                .GetRange(0, prova.Notas.Count < 3 ? prova.Notas.Count : 3 );
        }

        private void CalcularMedia(int quantidadeProvas, double total)
        {
            _mediaNotas = Math.Round(total / quantidadeProvas, 2);
        }

        public double ObtemMedia()
        {
            return new MediaNota(_mediaNotas).Arredondar();
        }
    }

   
}
