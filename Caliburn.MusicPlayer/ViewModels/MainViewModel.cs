//using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using MusicPlayer.InfraStructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;
using System.Windows.Forms;
using MusicPlayer.Core.Utils;
using MusicPlayer.InfraStructure.Model;


using MusicPlayer.Core.Foundation;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System.IO;
using Caliburn.MusicPlayer.UserControls;

namespace Caliburn.MusicPlayer.ViewModels
{
    public class MainViewModel: ViewModelBase,IDisposable
    {

        IPlayer player;

        private VolumeHelper volumeHelper = new VolumeHelper();

        public Audio selectedAudio { get; set; }

        private double audioProgress;

        public double AudioProgress
        {
            get { return audioProgress; }
            set
            {
                audioProgress = value;
                RaisePropertyChanged(nameof(AudioProgress));
            }
        }



        private double programVolume = 30;

        public double ProgramsVolume
        {
            get { return programVolume; }
            set
            {
                programVolume = value;
                RaisePropertyChanged(nameof(ProgramsVolume));
            }
        }

        private List<SongList> songLists = new List<SongList>();

        public List<SongList> SongLists
        {
            get { return songLists; }
            set
            {
                songLists = value;
                RaisePropertyChanged(nameof(SongLists));
            }
        }



        private RelayCommand<double> volumeChangedCommand;

        public RelayCommand<double> VolumeChangedCommand
        {
            get
            {
                if (volumeChangedCommand == null)
                {
                    volumeChangedCommand = new RelayCommand<double>((p) =>
                    {
                        volumeHelper.SetCurrentVolume(Convert.ToInt32(p));
                    });
                }
                return volumeChangedCommand;
            }

        }

        private RelayCommand start_StopCommand;

        public RelayCommand Start_StopCommand
        {
            get
            {
                if (start_StopCommand == null)
                {
                    start_StopCommand = new RelayCommand(() =>
                    {
                        
                        player.Init();
                        player.Play(selectedAudio.FullPath);
                    });
                }
                return start_StopCommand;
            }

        }



        private RelayCommand openFileCommand;

        public RelayCommand OpenFileCommand
        {
            get
            {
                if (openFileCommand == null)
                {
                    openFileCommand = new RelayCommand(()=>
                    {
                        AddNewSongList addWindow = new AddNewSongList();
                        addWindow.Show();
                        //FolderBrowserDialog dialog = new FolderBrowserDialog();
                        //dialog.ShowDialog();
                        //if (!string.IsNullOrEmpty(dialog.SelectedPath))
                        //{
                        //    PlayList = FileHelper.GetAllAudioFiles(dialog.SelectedPath);
                        //}
                    });
                }
                return openFileCommand;
            }
            
        }

        private RelayCommand<Audio> playCommand;

        public RelayCommand<Audio> PlayCommand
        {
            get
            {
                if(playCommand == null)
                {
                    playCommand = new RelayCommand<Audio>((p) =>
                    {
                        player = _container.GetService<IPlayer>();
                        player.Init();
                        player.Play(p.FullPath);
                    });
                }
                return playCommand;
            }
        }

        private List<Audio> playList;

        public List<Audio> PlayList
        {
            get { return playList; }
            set
            {
                playList = value;
                RaisePropertyChanged(nameof(PlayList));
            }
        }


        public MainViewModel()
        {
            player = _container.GetService<IPlayer>();
            //注册新增歌单消息
            Messenger.Default.Register<SongList>(this, new Action<SongList>((p) =>
            {
                SongLists.Add(p);
            }
           ));
            //注册进度条委托
            player.ProgressChanged += (p) =>
            {
                AudioProgress = p;
            };
            //加载默认歌单
            string configPath = System.IO.Path.Combine(System.Environment.CurrentDirectory,(string)Properties.Settings.Default["DefaultSongListConfig"]);
            if(File.Exists(configPath))
            {
                string config = File.ReadAllText(configPath);
                SongLists = JsonConvert.DeserializeObject<List<SongList>>(config);
            }
        }

        public void Dispose()
        {
            Messenger.Default.Unregister<SongList>(this);
            string strSongList = JsonConvert.SerializeObject(songLists);
            string configPath = System.IO.Path.Combine(System.Environment.CurrentDirectory, (string)Properties.Settings.Default["DefaultSongListConfig"]);
            File.WriteAllText(configPath, strSongList);
        }
    }
}
