using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class Aluno
    {
        public List<NotaProva> Notas { get; set; }

        public Aluno(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
            Notas = new List<NotaProva>();
        }         

        public int Id { get; set; }
        
        public string Nome { get; set; }

        public int Faltas { get; set; }
      
        public void ReceberAvaliacao(NotaProva nota)
        {
            Notas.Add(nota);
        }

        public bool EstaReprovado()
        {
            return Faltas > 5;
        }

        public override string ToString()
        {
            return Nome;
        }

        public override bool Equals(object obj)
        {
            return this.Id == ((Aluno)obj).Id;
        }
               
        
    }
}