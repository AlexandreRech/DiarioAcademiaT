using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio
{
    public class FeedbackProva
    {
        private Prova _prova;

        public FeedbackProva(Prova prova)
        {
            this._prova = prova;
        }

        public List<Nota> SelecionarPioresNotas()
        {
            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(_prova);

            return avaliador.PioresNotas;                
        }

    }
}