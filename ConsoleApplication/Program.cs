using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static DateTime Hoje = DateTime.Now;

        static void Main(string[] args)
        {
            Avaliacao avaliacao = new Avaliacao(Hoje);

            avaliacao.LancarNota(8, new Aluno("João"));
            avaliacao.LancarNota(10, new Aluno("José"));
            avaliacao.LancarNota(5, new Aluno("Maria"));

            // imprimi 10
            Console.WriteLine(avaliacao.ObtemMaiorNota());

            Console.ReadKey();
        }
    }
}