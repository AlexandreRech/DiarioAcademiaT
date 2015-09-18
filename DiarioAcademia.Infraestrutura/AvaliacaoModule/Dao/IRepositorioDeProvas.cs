using DiarioAcademia.Dominio.AvaliacaoModule;
using System;
using System.Collections.Generic;
namespace DiarioAcademia.Infra.AvaliacaoModule.Dao
{
    public interface IRepositorioDeProvas
    {
        void Atualizar(Prova prova);

        void CancelarFeedback(List<Prova> provas);

        void Salvar(Prova prova);

        List<Prova> SelecionarProvasFeedbackRealizado(int mes, int ano);

        List<Prova> SelecionarProvasPendentesFeedback(int mes, int ano);
    }
}
