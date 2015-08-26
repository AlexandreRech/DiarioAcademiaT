using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Dominio
{
    public class Nota
    {
        private Aluno aluno;

        public Nota(double nota, Aluno aluno)
        {
            if (nota > 10 || nota < 0)
                throw new ArgumentOutOfRangeException("nota", "Nota inválida");

            this.Valor = nota;
            this.aluno = aluno;

            aluno.ReceberAvaliacao(this);
        }

        public Aluno Dono
        {
            get;
            set;
        }

        public double Valor
        {
            get;
            set;
        }
    }
}