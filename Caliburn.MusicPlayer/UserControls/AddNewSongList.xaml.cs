using GalaSoft.MvvmLight.Messaging;
using MusicPlayer.Core.Utils;
using MusicPlayer.InfraStructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Caliburn.MusicPlayer.UserControls
{
    /// <summary>
    /// AddNewSongList.xaml 的交互逻辑
    /// </summary>
    public partial class AddNewSongList : Window
    {
        public AddNewSongList()
        {
            InitializeComponent();
        }

        private void MetroButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            if(!string.IsNullOrEmpty(dialog.SelectedPath))
            {
                text_Path.Text = dialog.SelectedPath;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(text_Path.Text))
            {
                return;
            }
            if(string.IsNullOrEmpty(SongListName.Text))
            {
                return;
            }
            Messenger.Default.Send<SongList>(
                new SongList {CreateTime = DateTime.Now,
                              SongListName = SongListName.Text,
                              Songs = FileHelper.GetAllAudioFiles(text_Path.Text)
                });
        }
    }
}
