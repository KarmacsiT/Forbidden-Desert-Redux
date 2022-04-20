using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUI_20212202_MQ7GIA.Logic
{
    public class Sound
    {
        private MediaPlayer music = new MediaPlayer();
        private MediaPlayer sound = new MediaPlayer();
        public double MusicVolume
        {
            get { return music.Volume; }
            set { music.Volume = value; }
        }
        public double SoundVolume
        {
            get { return sound.Volume; }
            set { sound.Volume = value; }
        }

        public Sound()
        {
            music.Open(new Uri(Path.Combine("Sounds", "hymn2aurora_test.mp3"), UriKind.RelativeOrAbsolute)); //music can be anything just choose it
            music.MediaEnded += Media_Ended;
            music.Play();
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            music.Open(new Uri(Path.Combine("Sounds", "hymn2aurora_test.mp3"), UriKind.RelativeOrAbsolute)); //music can be anything just choose it
            music.Play();
        }
        public void PlaySound(string filename)
        {
            sound.Open(new Uri(Path.Combine("Sounds", filename), UriKind.RelativeOrAbsolute));
            sound.Play();
        }
    }
}
