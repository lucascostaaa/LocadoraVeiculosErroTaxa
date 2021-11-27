using LocadoraVeiculos.Dominio.FuncionarioModule;
using LocadoraVeiculos.Infra.Logging;
using LocadoraVeiculos.Infra.ORM;
using LocadoraVeiculos.Infra.ORM.FuncionarioModule;
using LocadoraVeiculos.WindowsApp.ServiceLocator;
using System;
using System.Windows.Forms;

namespace LocadoraVeiculos.WindowsApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            LocadoraLoggerManager.ConfigurarLogger();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IServiceLocator serviceLocator = new ServiceLocatorManual();
            Funcionario funcionarioLogado = GetFuncionarioLogado();

            Application.Run(new TelaPrincipalForm(funcionarioLogado, serviceLocator));
        }

        private static Funcionario GetFuncionarioLogado()
        {
            LocadoraDbContext dbContext = new LocadoraDbContext();

            IFuncionarioRepository funcionarioRepository = new FuncionarioOrmDao(dbContext);

            return funcionarioRepository.SelecionarFuncionarioLogado();
        }
    }
}
