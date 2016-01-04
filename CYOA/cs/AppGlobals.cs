using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CYOA.utilities;

namespace CYOA.cs
{
    public class AppGlobals
    {
        public static bool inStoryMode = false;
        public static bool inCreationMode = false;
        public static string StoryGeneratorStageOneToolTip = "Here you can modify both the labels and starting gear/stats of your protagonist. \n"
                                                            + "NOTE: The updates are cosmetic only, the underlying implications the gear/stats \n"
                                                            + "do not change. \n"
                                                            + "Theme both affect the audio events of your adventure so please choose them\n"
                                                            + "carefully so as to enrich your adventure.";
        public static string baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/CYOA";
        public static string adventureDir = baseDir + "/Adventures";
        public static string saveDir = baseDir + "/Saves";
        public static string saveGameDir = saveDir + "/SaveGames";
        public static string saveGameMetaDir = saveDir + "/SaveMeta";
        public static string finishedStoryDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/CYOA";
        public static string advnGameDir = baseDir + "/ADVNs";
        public static string creationGameDir = baseDir + "/Authored";
        public static string sysGameDir = baseDir + "/System";
        public static string soundDir = baseDir + "/Sounds";
        public static string menuDir = baseDir + "/MenuSounds";
        public static Adventure currGlobalAdventure = null;
        public static MusicPlayer player = new MusicPlayer();
    }
}
