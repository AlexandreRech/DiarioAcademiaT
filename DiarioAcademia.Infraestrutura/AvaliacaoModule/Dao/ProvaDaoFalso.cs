using DiarioAcademia.Dominio.AvaliacaoModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Infra.AvaliacaoModule.Dao
{
    public class ProvaDaoFalso
    {
        static List<Prova> _provas = new List<Prova>();

        public List<Prova> SelecionarProvasPendentesFeedback(int mes, int ano)
        {
            return _provas
                .Where(x => x.FeedbackRealizado == false)
                .Where(x => x.Data.Month == mes)
                .Where(x => x.Data.Year == ano)
                .ToList();
        }

        public List<Prova> SelecionarProvasFeedbackRealizado(int mes, int ano)
        {
            return _provas
             .Where(x => x.FeedbackRealizado == true)
             .Where(x => x.Data.Month == mes)
             .Where(x => x.Data.Year == ano)
             .ToList();
        }

        public void Salvar(Prova prova)
        {
            _provas.Add(prova);
        }

        public void Atualizar(Prova prova)
        {
        }       
    }
}
