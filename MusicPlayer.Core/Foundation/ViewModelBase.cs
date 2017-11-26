using MusicPlayer.Core.IOC;
using MusicPlayer.Core.Player;
using MusicPlayer.InfraStructure.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Core.Foundation
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IServiceContainer _container;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase()
        {
            _container = new NinjectContainer();
            //注入实现
            _container.AddSingleton<IPlayer, NAudioPlayer>();

        }
    }
}
