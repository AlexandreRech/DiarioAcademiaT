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
            Prova prova = new Prova(Hoje);

            prova.LancarNota(8, new Aluno("João"));
            prova.LancarNota(10, new Aluno("José"));
            prova.LancarNota(5, new Aluno("Maria"));

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            // imprimi 10
            Console.WriteLine(avaliador.ObtemMaiorNota());

            Console.ReadKey();
        }
    }
}