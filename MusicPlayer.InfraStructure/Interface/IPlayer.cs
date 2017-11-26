using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.InfraStructure.Interface
{
    public interface IPlayer
    {
        /// <summary>
        /// 初始化播放器
        /// </summary>
        /// <returns></returns>
        void Init();
        /// <summary>
        /// 播放音频
        /// </summary>
        void Play(string path);
        /// <summary>
        /// 暂停音频播放
        /// </summary>
        void Pause();
        /// <summary>
        /// 停止播放音频
        /// </summary>
        void Stop();
        
        /// <summary>
        ///播放进度
        /// </summary>
        double Process { get; }

         Action<double> ProgressChanged { get; set; }
    }
}
