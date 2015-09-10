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
    public class ProvaTest
    {
        [TestMethod]
        public void Deveria_receber_um_lancamento_de_nota()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(new Nota(10, new Aluno("Rech")));

            prova.Notas.Should().HaveCount(1);
            prova.Notas[0].Valor.Should().Be(10);
        }

        [TestMethod]
        public void Deveria_receber_varios_lancamentos_de_notas()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(new Nota(10, new Aluno("Rech")));
            prova.LancarNota(new Nota(10, new Aluno("Carla")));

            prova.Notas.Should().HaveCount(2);
            prova.Notas[0].Valor.Should().Be(10);
            prova.Notas[1].Valor.Should().Be(10);
        }

        [TestMethod]
        public void Nao_deveria_lancar_2_notas_para_o_mesmo_aluno()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(new Nota(10, new Aluno("Rech")));
            prova.LancarNota(new Nota(9, new Aluno("Rech")));

            prova.Notas.Should().HaveCount(1);
            prova.Notas[0].Valor.Should().Be(10);
        }

        [TestMethod]
        public void Nao_deveria_lancar_nota_para_alunos_reprovados_por_falta()
        {
            Prova prova = new Prova(DateTime.Now);

            prova.LancarNota(new Nota(10, new Aluno("Rech") { Faltas = 6 }));
            prova.LancarNota(new Nota(9, new Aluno("Carla")));

            prova.Notas.Should().HaveCount(1);
            prova.Notas[0].Valor.Should().Be(9);
        }


    }
}