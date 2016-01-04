using CYOA.cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CYOA.utilities
{
    public class MusicPlayer
    {
        MediaPlayer player = new MediaPlayer();
        Random rnd = new Random();
        public bool canLoop = false;
        public bool isPaused = false;
        public bool isMenu = false;

        public void PlayMusic(string music, bool isTheme)
        {
            canLoop = isTheme;
            if (isTheme || (player.Source == null && (music == "" || music == "NULL" || music == null)))
            {
                DirectoryInfo soundFilesDir = new DirectoryInfo(AppGlobals.soundDir);
                FileInfo fullPath = soundFilesDir.GetFiles().Where(d => d.Name.ToLower().Contains(AppGlobals.currGlobalAdventure.Theme.ToLower())).OrderBy(x => rnd.Next()).First();

                player.Open(new Uri(@fullPath.FullName, UriKind.Relative));
                player.MediaEnded += MediaPlayer_Loop;
                player.Play();
                
            }
            else if (player.Source == null && (music != "" || music != "NULL" || music != null))
            {
                DirectoryInfo soundFilesDir = new DirectoryInfo(AppGlobals.soundDir);
                var gg = soundFilesDir.GetFiles();
                FileInfo fullPath = soundFilesDir.GetFiles().Where(d => d.Name.ToLower() == music.ToLower()).First();

                player.Open(new Uri(@fullPath.FullName, UriKind.Relative));
                player.MediaEnded += MediaPlayer_Loop;
                player.Play();
            }
            else if (player.Source.ToString() != (AppGlobals.soundDir + "/" + music.Trim() + ".mp3"))
            {
                var fullPath = AppGlobals.soundDir + "/" + music.Trim();

                player.Open(new Uri(@fullPath, UriKind.Relative));
                player.MediaEnded += MediaPlayer_Loop;
                player.Play();
            }
        }

        private void MediaPlayer_Loop(object sender, EventArgs e)
        {
            MediaPlayer player = sender as MediaPlayer;
            if (player == null)
                return;

            player.Position = new TimeSpan(0);
            if (!canLoop)
                player.Play();
            else
            {
                player.Stop();
                PlayMusic(AppGlobals.currGlobalAdventure.Theme, true);
            }
        }

        public void StopMusic()
        {
            player.Stop();
        }

        public void PauseMusic()
        {
            isPaused = true;
            player.Pause();
        }

        public void UnPauseMusic()
        {
            player.Play();
            isPaused = false;
        }

        public void PlayTheme()
        {
            isMenu = true;
            canLoop = true;
            DirectoryInfo soundFilesDir = new DirectoryInfo(AppGlobals.menuDir);
            FileInfo fullPath = soundFilesDir.GetFiles().OrderBy(x => rnd.Next()).First();

            player.Open(new Uri(@fullPath.FullName, UriKind.Relative));
            player.MediaEnded += MediaPlayer_Loop;
            player.Play();
        }
    }
}
