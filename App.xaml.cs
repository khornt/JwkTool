using JwkTool2.Models;
using JwkTool2.Models.Jwk;
using JwkTool2.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using PemConverter.Jwk;
using System;
using System.Configuration;
using System.Data;
using System.Windows;

namespace JwkTool2;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

    private readonly IServiceProvider _serviceProvider;

    public App()
    {

        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindowView>(s => new MainWindowView()
        {
            DataContext = s.GetRequiredService<MainWindowViewModel>()
        });

        services.AddSingleton<AppConfig>();

        services.AddTransient<IJwkCreator, JwkCreator>();
        services.AddTransient<IPubKeyHandler, PubKeyHandler>();

        _serviceProvider = services.BuildServiceProvider();

    }


    protected override void OnStartup(StartupEventArgs e)
    {

        MainWindow = _serviceProvider.GetRequiredService<MainWindowView>();

        //SetupNotify();
        MainWindow.Show();
        base.OnStartup(e);
    }


    protected override async void OnExit(ExitEventArgs e)
    {
        //await AppHost!.StopAsync();
        base.OnExit(e);
    }
}

