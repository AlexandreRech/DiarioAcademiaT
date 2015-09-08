using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace DiarioAcademia.Dominio.Tests
{        
    [TestClass]
    public class GabaritoTest
    {
        [TestMethod]
        public void Deveria_calcular_nota_do_aluno_atraves_do_gabarito()
        {
            Gabarito gabarito = new Gabarito('A', 'A', 'C', 'D', 'B');
           
            double nota = gabarito.CalcularNota(new Gabarito('A', 'A', 'C', 'D', 'C'));

            nota.Should().Be(8);
        }


    }
}