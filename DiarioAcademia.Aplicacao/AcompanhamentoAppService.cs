using DiarioAcademia.Dominio;
using DiarioAcademia.Infra.Dao;
using System;
using System.Linq;
using System.Collections.Generic;


namespace DiarioAcademia.Aplicacao
{
    public class AcompanhamentoAppService
    {
        private int total = 0;
        private NotificadorAppService _notificador;
        private ProvaDao _dao;

        public AcompanhamentoAppService(ProvaDao dao)
        {
            _notificador = new NotificadorAppService();
            _dao = dao;
        }

        public void RepassarFeedbackDasProvas()
        {
            List<Prova> provasSemFeedback = _dao.SelecionarProvasPendentesFeedback();

            List<FeedbackProva> feedbacks = new List<FeedbackProva>();

            foreach (Prova prova in provasSemFeedback)
            {
                if (AconteceuNesteMes(prova))
                {
                    prova.FeedbackRealizado = true;

                    _dao.Atualizar(prova);

                    total++;

                    FeedbackProva feedback = new FeedbackProva(prova);

                    feedbacks.Add(feedback);
                }
            }

            if(feedbacks.Any())
                _notificador.Enviar(feedbacks);
        }

        public int TotalEncerrados
        {
            get
            {
                return total;
            }
        }

        private bool AconteceuNesteMes(Prova prova)
        {
            return prova.Data.Month == DateTime.Now.Month;
        }


    }
}