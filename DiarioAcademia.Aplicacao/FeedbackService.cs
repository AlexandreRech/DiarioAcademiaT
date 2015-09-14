using System.Collections.Generic;

using DiarioAcademia.Dominio.AvaliacaoModule;
using DiarioAcademia.Infra.AvaliacaoModule.Dao;
using DiarioAcademia.Infra.AvaliacaoModule.Arquivo;

namespace DiarioAcademia.Aplicacao
{
    public class FeedbackService
    {
        private ProvaDao _dao;

        public FeedbackService(ProvaDao dao)
        {
            _dao = dao;
        }

        public void GerarFeedbackAlunos(int mes, int ano)
        {            
            List<Prova> provasSemFeedback = _dao.SelecionarProvasPendentesFeedback(mes, ano);

            List<FeedbackProva> feedbacks = new List<FeedbackProva>();

            foreach (Prova prova in provasSemFeedback)
            {
                prova.FeedbackRealizado = true;

                _dao.Atualizar(prova);

                TotalFeedbackRealizados++;

                FeedbackProva feedback = new FeedbackProva(prova, new AvaliadorProva());

                feedbacks.Add(feedback);
            }

            GeradorFeedback _geradorFeedback = new GeradorFeedback();

            _geradorFeedback.SalvarPdf(feedbacks);
        }

        public int TotalFeedbackRealizados { get; private set; }
    }
}