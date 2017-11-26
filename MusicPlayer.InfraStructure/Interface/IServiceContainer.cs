using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.InfraStructure.Interface
{
    public interface IServiceContainer
    {
        void Add<TInterface, TImplement>() where TImplement : TInterface;
        void AddSingleton<TInterface, TImplement>() where TImplement : TInterface;

        bool Contains<T>();

        TInterface GetService<TInterface>();
    }
}
