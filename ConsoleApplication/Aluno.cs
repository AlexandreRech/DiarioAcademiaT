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

        internal void ReceberAvaliacao(Nota nota)
        {
            _notas.Add(nota);
        }

        public override string ToString()
        {
            return _nome;
        }
    }
}