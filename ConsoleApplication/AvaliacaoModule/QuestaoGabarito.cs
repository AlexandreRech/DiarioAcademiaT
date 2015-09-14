using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class QuestaoGabarito
    {
        private int _numero;
        private char resposta;

        public QuestaoGabarito(int numero, char resposta)
        {
            this._numero = numero;
            this.resposta = resposta;
        }

        public char Resposta { get { return resposta; } }

        public override bool Equals(object obj)
        {
            QuestaoGabarito questao = (QuestaoGabarito)obj;

            return questao.resposta == this.resposta;
        }
    }
}
