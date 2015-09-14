using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace DiarioAcademia.Dominio.AvaliacaoModule.Tests
{        
    [TestClass]
    public class GabaritoProvaTest
    {
        [TestMethod(), TestCategory("Unit Tests")]
        public void Deveria_calcular_nota_do_aluno_atraves_do_gabarito()
        {
            GabaritoProva gabarito = new GabaritoProva('A', 'A', 'C', 'D', 'B');
           
            double nota = gabarito.CalcularNota(new GabaritoProva('A', 'A', 'C', 'D', 'C'));

            nota.Should().Be(8);
        }


    }
}