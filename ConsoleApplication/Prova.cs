using System;
using System.Collections.Generic;

namespace DiarioAcademia.Dominio
{
    public class Prova
    {
        public Gabarito Gabarito { get; private set; }

        public Prova(DateTime data)
        {
            Data = data;
            Notas = new List<Nota>();
        }

        public Prova(DateTime data, Gabarito gabarito)
            : this(data)
        {
            this.Gabarito = gabarito;
        }

        public DateTime Data { get; private set; }

        public List<Nota> Notas { get; private set; }

        public bool FeedbackRealizado { get; set; }

        public void LancarNota(double notaProva, Aluno aluno)
        {
            if (NaoPodeLancarNota(aluno))
                return;

            Nota nota = new Nota(notaProva, aluno);

            Notas.Add(nota);
        }

        public void LancarNota(Gabarito respostas, Aluno aluno)
        {
            LancarNota(Gabarito.CalcularNota(respostas), aluno);
        }

        private bool NaoPodeLancarNota(Aluno aluno)
        {
            return NotaJaRegistrada(aluno) || aluno.EstaReprovado();
        }

        private bool NotaJaRegistrada(Aluno aluno)
        {
            bool notaRegistrada = false;

            foreach (Nota n in Notas)
            {
                if (n.Aluno.Equals(aluno))
                {
                    notaRegistrada = true;
                    break;
                }
            }
            return notaRegistrada;
        }




       
    }
}
