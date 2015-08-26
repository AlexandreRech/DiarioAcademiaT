using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(5, new Aluno("Maria"));
            prova.LancarNota(8, new Aluno("João"));
            prova.LancarNota(10, new Aluno("José"));
            

            AvaliadorProva avaliador = new AvaliadorProva();

            avaliador.Avaliar(prova);

            // imprimi 10
            Console.WriteLine(avaliador.ObtemMaiorNota());

            // imprimi 5
            Console.WriteLine(avaliador.ObtemMenorNota());

            Console.ReadKey();
        }
    }
}