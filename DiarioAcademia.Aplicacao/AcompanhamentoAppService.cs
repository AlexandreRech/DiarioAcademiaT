using DiarioAcademia.Dominio;
using DiarioAcademia.Infra.Dao;
using System;
using System.Collections.Generic;


namespace DiarioAcademia.Aplicacao
{
    public class AcompanhamentoAppService
    {
        private int total = 0;

        public void RepassarFeedbackDasProvas()
        {
            ProvaDao dao = new ProvaDao();

            List<Prova> provasSemFeedback = dao.SelecionarProvasSemFeedback();

            foreach (Prova prova in provasSemFeedback)
            {
                if (AconteceuSemanaPassada(prova))
                {
                    prova.FeedbackRealizado = true;

                    total++;

                    dao.Atualizar(prova);
                }
            }
        }

        public int TotalEncerrados
        {
            get
            {
                return total;
            }
        }

        private bool AconteceuSemanaPassada(Prova prova)
        {
            return DiasEntre(prova.Data, DateTime.Now) >= 7;
        }

        private int DiasEntre(DateTime inicio, DateTime fim)
        {
            DateTime data = new DateTime(inicio.Ticks);

            int diasNoIntervalo = 0;

            while (data < fim)
            {
                data = data.AddDays(1);

                diasNoIntervalo++;
            }

            return diasNoIntervalo;
        }
    }
}
