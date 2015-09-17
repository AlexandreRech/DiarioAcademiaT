using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace DiarioAcademia.Dominio.AvaliacaoModule
{
    public class FeedbackMensal
    {
        private List<Prova> _provas;
        private AvaliadorProva _avaliador;
        private const string DIRETORIO_FEEDBACKS = @"C:\temp\diario academia";
        private string _mes;
        private int _ano;


        public FeedbackMensal(int mes, int ano, List<Prova> provas)
        {
            this._mes = DateTimeFormatInfo.CurrentInfo.GetMonthName(mes);
            this._ano = ano;
            this._provas = provas;
        }

        public string NomeRelatorio()
        {           
            return string.Format("Feedback mensal das provas realizadas em {0} de {1}", _mes, _ano);
        }

        public string NomeArquivo()
        {          
            string nomeArquivo = string.Format("feedback-provas-{0}-{1}.pdf", _mes, _ano);

            int contador = 0;

            while (File.Exists(Diretorio + "\\" + nomeArquivo))
            {
                nomeArquivo = string.Format("feedback-provas-{0}-{1}({2}).pdf", _mes, _ano, ++contador);
            }

            return Diretorio + "\\" + nomeArquivo;
        }

        public List<Prova> Provas { get { return _provas; } }

        public string Diretorio
        {
            get
            {
                if (!Directory.Exists(DIRETORIO_FEEDBACKS))
                    Directory.CreateDirectory(DIRETORIO_FEEDBACKS);

                return DIRETORIO_FEEDBACKS;
            }
        }

    }
}