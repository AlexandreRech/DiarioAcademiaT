using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio
{
    public class Gabarito
    {
        private static int CONTADOR = 0;

        private List<Questao> _questoes = new List<Questao>();

        public Gabarito(string respostas) 
            : this(respostas.ToCharArray())
        {
        }

        public Gabarito(params char[] respostas)
        {
            foreach (char resposta in respostas)
            {
                Questao questao = new Questao(++CONTADOR, resposta);

                _questoes.Add(questao);
            }
        }

        public double CalcularNota(Gabarito respostas)
        {
            double quantidadeAcertos = 0;

            double valorQuestao = 10 / _questoes.Count;

            for (int i = 0; i < _questoes.Count; i++)
            {
                if (_questoes[i].Equals(respostas._questoes[i]))
                {
                    quantidadeAcertos++;
                }
            }

            return quantidadeAcertos * valorQuestao;
        }
    }
}
