using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.InfraStructure.Model
{

    public class SongList
    {
        public string SongListName { get; set; }

        public List<Audio> Songs { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
