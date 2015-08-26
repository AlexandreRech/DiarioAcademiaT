using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Prova
    {
        public Prova(DateTime data)
        {
            Data = data;
        }

        public DateTime Data { get; private set; }

        public List<Nota> Notas { get; private set; }

        public void LancarNota(double notaProva, Aluno aluno)
        {
            Nota nota = new Nota(notaProva, aluno);

            if (Notas == null)
                Notas = new List<Nota>();

            Notas.Add(nota);
        }

        /* Notas baixas e notas altas
        public List<Nota> ObtemNotasBaixas()
        {
            return Notas.FindAll(n => n.Valor < 5);
        }

        public List<Nota> ObtemNotasBoas()
        {
            return Notas.FindAll(n => n.Valor >= 9);
        }
         */


    }
}
