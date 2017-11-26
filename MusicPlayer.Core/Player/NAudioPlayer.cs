using MusicPlayer.InfraStructure.Interface;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace MusicPlayer.Core.Player
{
    public class NAudioPlayer : IPlayer
    {
        public string _filePath { get; set; }
        public double Process
        {
            get =>  SlidePosition;
        }

        const double slideMax = 10.0;


        public Action<double> ProgressChanged { get; set; }

        public double SlidePosition
        {
            get { return slidePosition; }
            set
            {
                if(slidePosition != value)
                {
                    slidePosition = value;
                    if(_reader != null)
                    {
                        var position = (long)(_reader.Length * slidePosition / slideMax);
                        _reader.Position = position;
                    }
                }
                ProgressChanged?.Invoke(slidePosition);
            }
        }

        public bool isPlaying
        {
            get { return _wavePlayer != null && _wavePlayer.PlaybackState == PlaybackState.Playing; }
        }

        public bool isStopped
        {
            get { return _wavePlayer != null && _wavePlayer.PlaybackState == PlaybackState.Stopped; }
        }


        private IWavePlayer _wavePlayer;
        private WaveStream _reader;
        private double slidePosition;
        private Timer timer;

        public void Init()
        {
            timer = new Timer(500);
            timer.AutoReset = true;
            timer.Elapsed += (s,e) =>
            {
                if(_reader != null)
                {
                    SlidePosition = Math.Min(slideMax,_reader.Position * slideMax /_reader.Length);
                }
            };
           
        }

        public void Pause()
        {
            if(_wavePlayer != null)
            {
                _wavePlayer.Pause();
            }
        }

        public void Play(string path)
        {
            _filePath = path;
            if (string.IsNullOrEmpty(_filePath))
            {
                MessageBox.Show("File Path Invalid!");
            }
            if(_wavePlayer  == null)
            {
                CreatePlayer();
            }
            if(_reader == null)
            {
                _reader = new MediaFoundationReader(_filePath);
                _wavePlayer.Init(_reader);
            }
            _wavePlayer.Play();

            timer.Start();
        }

        

        public void Stop()
        {
            if(_wavePlayer != null)
            {
                _wavePlayer.Stop();
            }
        }

       

        private void CreatePlayer()
        {
            _wavePlayer = new WaveOutEvent();
            _wavePlayer.PlaybackStopped += _wavePlayer_PlaybackStopped;
        }

        private void _wavePlayer_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            if(_reader != null)
            {
                SlidePosition = 0;
                timer.Stop();
            }
            if(e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message + " Error Playing Audio");
            }
        }

        
    }
}
