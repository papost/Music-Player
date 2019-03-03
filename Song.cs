using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp9
{
    [Serializable]
    public class song
    {
        public String Song { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public String Year { get; set; }
        public String Gender { get; set; }
        public String Source { get; set; }
        public int Fame { get; set; }
        public song addSong(String Song)
        {
            this.Song = Song;
            return this;
        }
        public song addArtist(String Artist)
        {
            this.Artist = Artist;
            return this;
        }
        public song addAlbum(String Album)
        {
            this.Album = Album;
            return this;
        }
        public song addYear(String Year)
        {
            this.Year = Year;
            return this;
        }
        public song addGender(String Gender)
        {
            this.Gender = Gender;
            return this;
        }
        public song addSource(String Source)
        {
            this.Source = Source;
            return this;
        }
        public song addFame(int Fame)
        {
            this.Fame = Fame;
            return this;
        }
    }
}
