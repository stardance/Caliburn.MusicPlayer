using Caliburn.MusicPlayer.ViewModels;
using MusicPlayer.Core.IOC;
using MusicPlayer.Core.Player;
using MusicPlayer.InfraStructure.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Caliburn.MusicPlayer
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public IServiceContainer _container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            _container = new NinjectContainer();

            //注入实现
            _container.AddSingleton<IPlayer,NAudioPlayer>();

            MainWindow mainwindow = new MusicPlayer.MainWindow();
            MainViewModel viewModel = new MainViewModel();
            mainwindow.DataContext = viewModel;
            mainwindow.Show();

        }
    }
}
