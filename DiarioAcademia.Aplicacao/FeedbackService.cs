using System.Collections.Generic;

using DiarioAcademia.Dominio.AvaliacaoModule;
using DiarioAcademia.Infra.AvaliacaoModule.Dao;
using DiarioAcademia.Infra.AvaliacaoModule.Arquivo;
using log4net.Core;
using System;
using DiarioAcademia.Infra.Shared;

namespace DiarioAcademia.Aplicacao
{
    public class FeedbackService
    {
        private ProvaDao _dao;
        private GeradorFeedback _geradorFeedback;

        public FeedbackService(ProvaDao dao, GeradorFeedback gerador)
        {
            _dao = dao;
            _geradorFeedback = gerador;
        }

        public void GerarFeedbackAlunos(int mes, int ano)
        {
            List<Prova> provasSemFeedback = _dao.SelecionarProvasPendentesFeedback(mes, ano);

            foreach (Prova prova in provasSemFeedback)
            {
                try
                {
                    prova.Feedback = FeedbackEnum.Realizado;

                    _dao.Atualizar(prova);

                    FeedbackProva feedback = new FeedbackProva(prova, new AvaliadorProva());

                    _geradorFeedback.SalvarPdf(feedback);
                }
                catch (DaoException ex)
                {                    
                }
            }
        }

        public int TotalFeedbackRealizados { get; private set; }
    }
}