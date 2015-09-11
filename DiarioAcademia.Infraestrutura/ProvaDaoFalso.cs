using DiarioAcademia.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Infra.Dao
{
    public class ProvaDaoFalso
    {
        static List<Prova> _provas = new List<Prova>();

        public List<Prova> SelecionarProvasPendentesFeedback()
        {
            return _provas.Where(x => x.FeedbackRealizado == false).ToList();
        }

        public void Salvar(Prova prova)
        {
            _provas.Add(prova);
        }

        public void Atualizar(Prova prova)
        {
        }

        public List<Prova> SelecionarProvasComFeedback()
        {
            return _provas.Where(x => x.FeedbackRealizado == true).ToList();
        }
    }
}
