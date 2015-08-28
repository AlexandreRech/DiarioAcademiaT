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
        private double _mediaNotas = 0;

        public List<Nota> MelhoresNotas { get; private set; }

        public List<Nota> PioresNotas { get; private set; }

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
            double somaNotas = 0;

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

                somaNotas += nota.Valor;
            }

            CalcularMedia(prova.Notas.Count, somaNotas);
            
            PioresNotas = ObtemPioresNotas(prova);
        }

        private List<Nota> ObtemPioresNotas(Prova prova)
        {
            return prova.Notas.FindAll(x => x.Valor < 7)
                .OrderBy(x => x.Valor)
                .ToList()
                .GetRange(0, 3);
        }

        private void CalcularMedia(int quantidadeProvas, double total)
        {
            _mediaNotas = Math.Round(total / quantidadeProvas, 2);
        }

        public double ObtemMedia()
        {
            //return new MediaNota(_mediaNotas).Arredondar();

            return ArredondarMedia(_mediaNotas);
        }

        private double ArredondarMedia(double media)
        {
            double decimais = media % 1;

            if (decimais < 0.25) decimais = 0;

            else if (decimais < 0.75) decimais = 0.50;

            else decimais = 1;

            return ((int)media) + decimais;
        }

        
    }

    public class MediaNota
    {
        private double _mediaComDecimais;

        public MediaNota(double media)
        {
            this._mediaComDecimais = media;
        }

        public double Arredondar()
        {
            double decimais = _mediaComDecimais - Math.Truncate(_mediaComDecimais);

            if (decimais < 0.25) decimais = 0;

            else if (decimais < 0.50 || decimais < 0.75) decimais = 0.50;

            else decimais = 1;

            return ((int)_mediaComDecimais) + decimais;
        }

    }
}
