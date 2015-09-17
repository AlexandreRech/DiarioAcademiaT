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
            List<Prova> provas = _dao.SelecionarProvasPendentesFeedback(mes, ano);

            foreach (Prova prova in provas)
            {
                prova.Feedback = FeedbackEnum.Realizado;

                TotalFeedbackRealizados++;

                _dao.Atualizar(prova);
            }

            try
            {
                AvaliadorProva avaliador = new AvaliadorProva();

                FeedbackMensal feedbackMensal = new FeedbackMensal(mes, ano, provas, avaliador);

                _geradorFeedback.SalvarPdf(feedbackMensal);
            }
            catch
            {
                _dao.CancelarFeedback(provas);                
            }
        }

        public int TotalFeedbackRealizados { get; private set; }
    }
}