using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio
{
    public class Aluno
    {
        private string _nome;
        private List<Nota> _notas;

        public Aluno(string nome)
        {
            this._nome = nome;
            this._notas = new List<Nota>();
        }

        public Aluno(int id, string nome)
        {
            this.Id = id;
            this._nome = nome;
            this._notas = new List<Nota>();
        } 

        public void ReceberAvaliacao(Nota nota)
        {
            _notas.Add(nota);
        }

        public override string ToString()
        {
            return _nome;
        }

        public override bool Equals(object obj)
        {
            return this.Id == ((Aluno)obj).Id;
        }

        public int Faltas { get; set; }

        public string Nome { get { return _nome; } }

        public bool EstaReprovado()
        {
            return Faltas > 5;
        }

        public int Id { get; set; }
    }
}