using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ShortcutDrawer.UI.WPF.ViewModels;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace ShortcutDrawer.UI.WPF.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindowViewModel ViewModel { get; }

    public MainWindow(MainWindowViewModel viewModel, INavigationService navService, IPageService pageService)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();

        this.Left = 0;
        this.Top = 0;

        SetPageService(pageService);

        // navService.SetNavigationControl(null);
    }

    public void CloseWindow() => Close();

    public INavigationView GetNavigation()
    {
        throw new NotImplementedException();
    }

    public bool Navigate(Type pageType)
    {
        throw new NotImplementedException();
    }

    public void SetPageService(IPageService pageService)
    {
    }

    public void SetServiceProvider(IServiceProvider serviceProvider)
    {
        throw new NotImplementedException();
    }

    public void ShowWindow() => Show();

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        this.Visibility = Visibility.Visible;
    }

    private void FluentWindow_Deactivated(object sender, EventArgs e)
    {
        this.Visibility = Visibility.Hidden;
    }

    private void FluentWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel = true;
        this.Visibility = Visibility.Hidden;
    }

    private void Window_KeyUp(object sender, KeyEventArgs e)
    {
    }
}
