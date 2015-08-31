using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Dominio
{
    public class Nota
    {
       
        public Nota(double nota, Aluno aluno)
        {
            if (nota > 10 || nota < 0)
                throw new ArgumentOutOfRangeException("nota", "Nota inválida");

            this.Valor = nota;
            this.Aluno = aluno;

            Aluno.ReceberAvaliacao(this);
        }

        public Aluno Aluno
        {
            get;
            private set;
        }

        public double Valor
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Valor.ToString();
        }
    }
}