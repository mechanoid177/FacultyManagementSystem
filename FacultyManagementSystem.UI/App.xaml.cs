using FacultyManagementSystem.Database.Interfaces;
using FacultyManagementSystem.Library.Interfaces;
using FacultyManagementSystem.View.UserControls;
using FacultyManagementSystem.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FacultyManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();
            host.Start();
            App app = new App();
            app.InitializeComponent();
            app.MainWindow = host.Services.GetRequiredService<MainWindow>();
            app.MainWindow.Visibility = Visibility.Visible;
            app.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<userContolLibrary>();
                    services.AddSingleton<LibraryViewModel>();
                    services.AddSingleton<IMySqlDatabase, Database.MySqlDatabase>();
                    services.AddSingleton<Database.DatabaseContext>();
                    services.AddSingleton<IDatabaseManager, Database.DatabaseManager>();
                    services.AddSingleton<ILibrary, Library.Library>();
                    

                }).ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddXmlFile("appsettings.xml", optional: false, reloadOnChange: true);
                });
    }

}
