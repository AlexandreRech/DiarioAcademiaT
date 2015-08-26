using System;
using System.Collections.Generic;

namespace DiarioAcademia.Dominio
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
    }
}
