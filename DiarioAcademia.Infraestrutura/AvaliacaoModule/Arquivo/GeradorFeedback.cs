using DiarioAcademia.Dominio.AvaliacaoModule;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioAcademia.Infra.AvaliacaoModule.Arquivo
{
    public class GeradorFeedback
    {
        public virtual void SalvarPdf(FeedbackMensal feedback)
        {

            FileStream fs = new FileStream(feedback.NomeArquivo(), FileMode.Create);

            Document document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.Open();

            ConfigurarCabecalho(feedback, document);

            ConfigurarConteudo(feedback, document);

            document.Close();

            writer.Close();

            fs.Close();
        }

        private static void ConfigurarConteudo(FeedbackMensal feedback, Document document)
        {
            foreach (Prova prova in feedback.Provas)
            {
                PdfPTable table = new PdfPTable(2);

                string tituloDaProva = "Prova sobre: " + prova.Assunto;

                PdfPCell cell = new PdfPCell(new Phrase(tituloDaProva));
                cell.Colspan = 2;
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                table.AddCell(cell);

                table.AddCell("Aluno");
                table.AddCell("Nota");

                foreach (NotaProva nota in prova.Notas)
                {
                    table.AddCell(nota.Aluno.Nome);
                    table.AddCell(nota.Valor.ToString());
                }

                document.Add(table);

                document.Add(new Paragraph(" "));
            }
        }

        private static void ConfigurarCabecalho(FeedbackMensal feedback, Document document)
        {
            document.Add(new Paragraph(feedback.NomeRelatorio()) { Alignment = 1 });

            document.Add(new Paragraph("Média mensal: " + feedback.MediaMensal) { Alignment = 1 });

            document.Add(new Paragraph(" "));
        }
    }
}
