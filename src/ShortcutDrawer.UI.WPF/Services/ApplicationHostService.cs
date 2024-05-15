using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Wpf.Ui;

namespace ShortcutDrawer.UI.WPF.Services;

internal class ApplicationHostService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private Views.MainWindow? _navigationWindow;

    public ApplicationHostService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await HandleActivationAsync();
    }

    private async Task HandleActivationAsync()
    {
        await Task.CompletedTask;
        if (Application.Current.Windows.OfType<Views.MainWindow>().Any() == false)
        {
            //_navigationWindow = (
            //    _serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow
            //    )!;
            _navigationWindow = (
                (Views.MainWindow)_serviceProvider.GetService(typeof(Views.MainWindow))
                )!;
            _navigationWindow.ShowWindow();

            // _ = _navigationWindow.Navigate()
        }

        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
