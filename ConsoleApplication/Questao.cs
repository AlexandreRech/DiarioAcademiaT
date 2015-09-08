using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio
{
    public class Questao
    {
        private int _numero;
        private char resposta;

        public Questao(int numero, char resposta)
        {
            this._numero = numero;
            this.resposta = resposta;
        }

        public override bool Equals(object obj)
        {
            Questao questao = (Questao)obj;

            return questao.resposta == this.resposta;
        }
    }
}
