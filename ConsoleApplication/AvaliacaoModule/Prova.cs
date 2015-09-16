using System;
using System.Collections.Generic;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class Prova
    {       
        public Prova(DateTime data)
        {
            Data = data;
            Notas = new List<NotaProva>();
        }

        public Prova(DateTime data, GabaritoProva gabarito)
            : this(data)
        {
            this.Gabarito = gabarito;
        }

        public int Id { get; set; }

        public GabaritoProva Gabarito { get; private set; }

        public DateTime Data { get; set; }

        public List<NotaProva> Notas { get; private set; }

        public FeedbackEnum Feedback { get; set; }

        public string Assunto { get; set; }
              
        public void LancarNota(NotaProva nota)
        {
            if (NaoPodeLancarNota(nota.Aluno))
                return;

            nota.Prova = this;

            Notas.Add(nota);
        }

        public void LancarNota(GabaritoProva respostas, Aluno aluno)
        {
            double valor = Gabarito.CalcularNota(respostas);

            NotaProva nota = new NotaProva(valor, aluno);

            LancarNota(nota);
        }

        private bool NaoPodeLancarNota(Aluno aluno)
        {
            return NotaJaRegistrada(aluno) || aluno.EstaReprovado();
        }

        private bool NotaJaRegistrada(Aluno aluno)
        {
            bool notaRegistrada = false;

            foreach (NotaProva n in Notas)
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
