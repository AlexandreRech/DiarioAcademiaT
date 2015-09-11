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

        public Prova(DateTime data, Gabarito gabarito)
            : this(data)
        {
            this.Gabarito = gabarito;
        }

        public int Id { get; set; }

        public Gabarito Gabarito { get; private set; }

        public DateTime Data { get; set; }

        public List<Nota> Notas { get; private set; }

        public bool FeedbackRealizado { get; set; }

        public string Assunto { get; set; }
              
        public void LancarNota(Nota nota)
        {
            if (NaoPodeLancarNota(nota.Aluno))
                return;

            nota.Prova = this;

            Notas.Add(nota);
        }

        public void LancarNota(Gabarito respostas, Aluno aluno)
        {
            double valor = Gabarito.CalcularNota(respostas);

            Nota nota = new Nota(valor, aluno);

            LancarNota(nota);
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
