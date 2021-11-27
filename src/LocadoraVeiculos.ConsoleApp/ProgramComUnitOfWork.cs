using LocadoraVeiculos.Dominio;
using LocadoraVeiculos.Dominio.ClienteModule;
using LocadoraVeiculos.Dominio.CupomModule;
using LocadoraVeiculos.Infra.Logging;
using LocadoraVeiculos.Infra.ORM;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Linq;

namespace LocadoraVeiculos.ConsoleApp
{
    class ProgramComUnitOfWork
    {

        static void Main2(string[] args)
        {
            LocadoraLoggerManager.ConfigurarLogger();

            Log.Logger.Aqui().Verbose("teste verbose");

            Log.Logger.Aqui().Debug("teste debug");

            Log.Logger.Aqui().Information("teste information");

            Log.Logger.Aqui().Warning("teste warning");

            Log.Logger.Aqui().Error("teste erro");

            Log.CloseAndFlush();

            //using (var db = new LocadoraDbContext())
            //{
            //    IParceiroRepository parceiroRepository = new ParceiroOrmDao(db);

            //    var parceiro = new Parceiro("Deko");

            //    parceiroRepository.Inserir(parceiro);

            //    ICupomRepository cupomRepository = new CupomOrmDao(db);

            //    var cupom = new Cupom("10 Pila", 10, new DateTime(2021, 12, 31),
            //        parceiro, 300, TipoCupomEnum.ValorFixo);

            //    cupomRepository.Inserir(cupom);

            //    db.SaveChanges();
            //}



            //var parceiroId = AdicionarParceiro();

            //var parceiroSelecionado = SelecionarParceiro(parceiroId);

            //AdicionarCupom(parceiroSelecionado);

            //var cliente = AdicionarCliente();

            //AdicionarCondutor(cliente);
        }

        private static Cupom AdicionarCupom(Parceiro parceiro)
        {
            var cupom = new Cupom("10 Pila", 10, new DateTime(2021, 12, 31),
                parceiro, 300, TipoCupomEnum.ValorFixo);

            using var db = new LocadoraDbContext();

            var dbEntityEntry = db.Entry(parceiro);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                db.Attach(parceiro);
            }

            db.Cupons.Add(cupom);

            db.SaveChanges();

            return cupom;
        }

        private static int AdicionarParceiro()
        {
            var parceiro = new Parceiro("Deko");

            using var db = new LocadoraDbContext();

            db.Parceiros.Add(parceiro);

            db.SaveChanges();

            return parceiro.Id;
        }

        private static Parceiro SelecionarParceiro(int id)
        {
            using var db = new LocadoraDbContext();

            var p1 = db.Parceiros.Single(x => x.Id == id);

            return p1;
        }

        private static Condutor AdicionarCondutor(Cliente cliente)
        {
            using var db = new LocadoraDbContext();

            var condutor = new Condutor("Bruno Henrique", "Gávea", "999292107", "3717158",
                "04791277945", "123456789", new DateTime(2022, 05, 26), cliente);

            var dbEntityEntry = db.Entry(cliente);

            if (dbEntityEntry.State == EntityState.Detached)
            {
                db.Attach(cliente);
            }

            db.Condutores.Add(condutor);

            db.SaveChanges();

            return condutor;
        }

        private static Cliente AdicionarCliente()
        {
            using var db = new LocadoraDbContext();

            var cliente = new Cliente("Flamengo", "Gávea", "9524282242",
                "", "", "1234567891234", TipoPessoaEnum.Juridica, "contato@empresa.com.br");

            db.Clientes.Add(cliente);

            db.SaveChanges();

            return cliente;
        }

        static void Main3(string[] args)
        {
            LocadoraLoggerManager.ConfigurarLogger();

            Log.Logger.Aqui().Verbose("teste verbose");

            Log.Logger.Aqui().Debug("teste debug");

            Log.Logger.Aqui().Information("teste information");

            Log.Logger.Aqui().Warning("teste warning");

            Log.Logger.Aqui().Error("teste erro");

            Log.CloseAndFlush();
        }

    }
}
