using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShortcutDrawer.UI.WPF.Services;
using System;
using System.IO;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace ShortcutDrawer.UI.WPF;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private static readonly IHost _Host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c =>
        {
            var basePath = Path.GetDirectoryName(AppContext.BaseDirectory) ?? throw new DirectoryNotFoundException("Unable to find the base directory of the application.");
            _ = c.SetBasePath(basePath);
        })
        .ConfigureServices(
            (context, services) =>
            {
                _ = services.AddHostedService<ApplicationHostService>();
                _ = services.AddSingleton<IPageService, PageService>();
                _ = services.AddSingleton<INavigationService, NavigationService>();
                _ = services.AddSingleton<Views.MainWindow>();
                _ = services.AddSingleton<ViewModels.MainWindowViewModel>();
            }
        )
        .Build();

    public App()
    {
    }

    private async void Application_Startup(object sender, System.Windows.StartupEventArgs e)
    {
        await _Host.StartAsync();
    }
}
