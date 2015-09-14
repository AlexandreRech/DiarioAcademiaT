using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class FeedbackProva
    {
        private Prova _prova;
        private AvaliadorProva _avaliador;

        public FeedbackProva(Prova prova, AvaliadorProva avaliador)
        {
            this._prova = prova;
            this._avaliador = avaliador;
        }

        public List<NotaProva> PioresNotas
        {
            get
            {
                _avaliador.Avaliar(_prova);

                return _avaliador.PioresNotas;
            }
        }

        public double MediaProva
        {
            get
            {
                _avaliador.Avaliar(_prova);

                return _avaliador.ObtemMedia();
            }
        }
    }
}