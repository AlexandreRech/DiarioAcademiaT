using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class GabaritoProva
    {
        private static int CONTADOR = 0;

        private List<QuestaoGabarito> _questoes = new List<QuestaoGabarito>();

        public GabaritoProva(string respostas)
            : this(respostas.ToCharArray())
        {
        }

        public GabaritoProva(params char[] respostas)
        {
            foreach (char resposta in respostas)
            {
                QuestaoGabarito questao = new QuestaoGabarito(++CONTADOR, resposta);

                _questoes.Add(questao);
            }
        }

        public double CalcularNota(GabaritoProva respostas)
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

        public override string ToString()
        {
            if(_questoes.Any())
                return string.Join("", _questoes.Select(q => q.Resposta).ToArray());

            return "";
        }
    }
}
