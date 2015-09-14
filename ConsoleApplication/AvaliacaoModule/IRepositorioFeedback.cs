using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public interface IRepositorioFeedback
    {
        void SalvarPdf(List<FeedbackProva> feedbacks);
    }
}
