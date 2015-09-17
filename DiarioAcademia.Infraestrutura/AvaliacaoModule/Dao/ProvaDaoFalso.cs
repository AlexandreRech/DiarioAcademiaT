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
                .Where(x => x.Feedback == FeedbackEnum.Pendente)
                .Where(x => x.Data.Month == mes)
                .Where(x => x.Data.Year == ano)
                .ToList();
        }

        public List<Prova> SelecionarProvasFeedbackRealizado(int mes, int ano)
        {
            return _provas
             .Where(x => x.Feedback == FeedbackEnum.Realizado)
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
            Prova provaEncontrada = _provas.Find(x => x.Id == prova.Id);

            provaEncontrada.Assunto = prova.Assunto;
            provaEncontrada.Data = prova.Data;
            provaEncontrada.Feedback = prova.Feedback;
            provaEncontrada.Gabarito = prova.Gabarito;            
        }       
    }
}
