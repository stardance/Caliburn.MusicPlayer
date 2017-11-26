using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Core.Utils
{
    public class VolumeHelper
    {
        [DllImport("Winmm.dll")]
        private static extern int waveOutSetVolume(int hwo, System.UInt32 pdwVolume);

        [DllImport("Winmm.dll")]
        private static extern uint waveOutGetVolume(int hwo, out System.UInt32 pdwVolume);
        

        private  int volumeMinScope = 0;
        private  int volumeMaxScope = 100;
        private  int volumeSize = 100;

        /// <summary>
        /// 音量控制，但不改变系统音量设置
        /// </summary>
        public int VolumeSize
        {
            get { return volumeSize; }
            set { volumeSize = value; }
        }

        public void SetCurrentVolume(int volumn)
        {
            VolumeSize = volumn;
            if (volumeSize < 0)
            {
                volumeSize = 0;
            }

            if (volumeSize > 100)
            {
                volumeSize = 100;
            }
            System.UInt32 Value = (System.UInt32)((double)0xffff * (double)volumeSize / (double)(volumeMaxScope - volumeMinScope));//先把trackbar的value值映射到0x0000～0xFFFF范围


            //限制value的取值范围
            if (Value < 0)
            {
                Value = 0;
            }

            if (Value > 0xffff)
            {
                Value = 0xffff;
            }

            System.UInt32 left = (System.UInt32)Value;//左声道音量
            System.UInt32 right = (System.UInt32)Value;//右
            waveOutSetVolume(0, left << 16 | right); //"<<"左移，“|”逻辑或运算
        }
    }
}
