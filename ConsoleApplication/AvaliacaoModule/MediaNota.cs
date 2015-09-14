using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class MediaNota
    {
        private double _mediaComDecimais;

        public MediaNota(double media)
        {
            this._mediaComDecimais = media;
        }

        public double Arredondar()
        {
            double decimais = _mediaComDecimais % 1;

            if (decimais < 0.25) decimais = 0;

            else if (decimais < 0.75) decimais = 0.50;

            else decimais = 1;

            return ((int)_mediaComDecimais) + decimais;
        }

    }
}
