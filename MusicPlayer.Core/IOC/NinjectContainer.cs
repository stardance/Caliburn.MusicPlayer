using MusicPlayer.InfraStructure.Interface;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Core.IOC
{
    /// <summary>
    /// Ninject容器管理器
    /// </summary>
    public class NinjectContainer : IServiceContainer
    {
        StandardKernel _container;
        public NinjectContainer()
        {
            _container = new StandardKernel();
        }
        public void Add<TInterface, TImplement>() where TImplement : TInterface 
        {
            _container.Bind<TInterface>().To<TImplement>();
        }

        public void AddSingleton<TInterface, TImplement>() where TImplement : TInterface
        {
            _container.Bind<TInterface>().To<TImplement>().InSingletonScope();
        }

        public bool Contains<T>()
        {
            return _container.CanResolve<T>();
        }

        public TInterface GetService<TInterface>()
        {
            return _container.Get<TInterface>();
        }
    }
}
