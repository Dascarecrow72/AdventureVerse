using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CYOA.cs;
using System.IO;
using System.Windows.Markup;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using CYOA.utilities;

namespace CYOA
{
    /// <summary>
    /// Interaction logic for BaseWindow.xaml
    /// </summary>
    public partial class BaseWindow : Window
    {
        public BaseWindow()
        {
            InitializeComponent();
            Loaded += BaseWindow_Loaded;

        }
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AppGlobals.player.isMenu = true;
            AppGlobals.player.PlayTheme();

            //BASE DIRECTORY
            if (!Directory.Exists(@AppGlobals.baseDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.baseDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 
            }

            //SOUND DIRECTORY
            if (!Directory.Exists(@AppGlobals.soundDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.soundDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

                string[] soundFiles = Directory.GetFiles(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.Length - 8) + "/music");
                foreach (string filePath in soundFiles)
                {
                    string fileName = System.IO.Path.GetFileName(filePath);
                    string destFile = System.IO.Path.Combine(@AppGlobals.soundDir, fileName);
                    System.IO.File.Copy(filePath, destFile, true);
                }
            }

            //MENU MUSIC DIRECTORY
            if (!Directory.Exists(@AppGlobals.menuDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.menuDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

                string[] soundFiles = Directory.GetFiles(System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.Length - 8) + "/menu");
                foreach (string filePath in soundFiles)
                {
                    string fileName = System.IO.Path.GetFileName(filePath);
                    string destFile = System.IO.Path.Combine(@AppGlobals.menuDir, fileName);
                    System.IO.File.Copy(filePath, destFile, true);
                }
            }

            //ADVENTURES DIRECTORY
            if (!Directory.Exists(@AppGlobals.adventureDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.adventureDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 
            }

            //AUTHORING DIRECTORY
            if (!Directory.Exists(@AppGlobals.creationGameDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.creationGameDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            //SAVES DIRECTORY
            if (!Directory.Exists(@AppGlobals.saveDir))
            {
               DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.saveDir);
               di.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 
            }

            //SAVEGAME DIRECTORY
            if (!Directory.Exists(@AppGlobals.saveGameDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.saveGameDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            //SAVEMETA DIRECTORY
            if (!Directory.Exists(@AppGlobals.saveGameMetaDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.saveGameMetaDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            //AVDN DIRECTORY FOR EXPORT (STORES ZIP W/ CHANGED EXTENSION)
            if (!Directory.Exists(@AppGlobals.advnGameDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.advnGameDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden; 
            }

            //SYSTEM DIRECTORY FOR EXCEPTION LOGGING AND MISC
            if (!Directory.Exists(@AppGlobals.sysGameDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.sysGameDir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            //MY DOCUMENTS FOLDER FOR EXPORTED FINISHED STORY
            if (!Directory.Exists(@AppGlobals.finishedStoryDir))
            {
                DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.finishedStoryDir);
            }

            //NO EXIT BUTTON
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainMenu mainMenu = new MainMenu();
                this._navFrame.Navigate(mainMenu);
                AppGlobals.player.isMenu = true;
                if (!AppGlobals.player.isMenu)
                    AppGlobals.player.PauseMusic();
                else
                {
                    AppGlobals.player.StopMusic();
                    AppGlobals.player.PlayTheme();
                }
            }
        }

        public void LogException(Exception ex, string exLoc)
        {
            using (FileStream fs = new FileStream(@AppGlobals.sysGameDir + "/" + exLoc + "_" + DateTime.Now.ToLongDateString() + ".err", FileMode.Create))
            {
                CYOA.utilities.XamlWriter.Save(ex, fs);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.saveGameMetaDir);
            foreach (var file in currentDir.GetFiles().Where(d => d.Name.Contains(".xaml")))
            {
                //CHANGE FROM .XAML TO .ADVM TO LOAD UP DATA
                File.Copy(file.FullName, System.IO.Path.ChangeExtension(file.FullName, ".advm"));
            }
            currentDir = null;
            currentDir = new DirectoryInfo(@AppGlobals.saveGameDir);
            foreach (var file in currentDir.GetFiles().Where(d => d.Name.Contains(".xaml")))
            {
                //CHANGE FROM .XAML TO .ADVO TO LOAD UP DATA
                File.Copy(file.FullName, System.IO.Path.ChangeExtension(file.FullName, ".advo"));
            }
        }
    }
}
