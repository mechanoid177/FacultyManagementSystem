using FacultyManagementSystem.Database.Interfaces;
using FacultyManagementSystem.Library.Interfaces;
using FacultyManagementSystem.UI;
using FacultyManagementSystem.UI.View.UserControls;
using FacultyManagementSystem.UI.ViewModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using Serilog;

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
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("FMS-log-.log", rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 200_000_000L)
                .CreateLogger();

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
                    //services.AddSingleton<UserContolLibrary>();
                    services.AddSingleton<LibraryViewModel>();
                    services.AddSingleton<IMySqlDatabase, Database.MySqlDatabase>();
                    services.AddSingleton<Database.DatabaseContext>();
                    services.AddSingleton<IDatabaseManager, Database.DatabaseManager>();
                    services.AddSingleton<ILibrary, Library.Library>();


                }).ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddXmlFile("appsettings.xml", optional: false, reloadOnChange: true);
                }).UseSerilog();
    }

}
