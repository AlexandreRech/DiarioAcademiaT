using System;
using System.Collections.Generic;

namespace DiarioAcademia.Dominio
{
    public class Prova
    {
        public Prova(DateTime data)
        {
            Data = data;
            Notas = new List<Nota>();
        }

        public DateTime Data { get; private set; }

        public List<Nota> Notas { get; private set; }

        public void LancarNota(double notaProva, Aluno aluno)
        {
            if (NaoPodeLancarNotas(aluno))
                return;

            Nota nota = new Nota(notaProva, aluno);

            Notas.Add(nota);
        }


        private bool NaoPodeLancarNotas(Aluno aluno)
        {
            return NotaEstaLancada(aluno) || aluno.EstaReprovado();
        }     

        private bool NotaEstaLancada(Aluno aluno)
        {
            return Notas.Exists(n => n.Aluno.Equals(aluno));
        }
    }
}
