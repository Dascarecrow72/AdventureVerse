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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Collections;
using System.Windows.Markup;
using CYOA.pages;
using CYOA.utilities;

namespace CYOA
{
    /// <summary>
    /// Interaction logic for StoryWindow.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            Loaded += MainMenu_Loaded;
        }
        ExceptionHandling exHand = new ExceptionHandling();
                
        void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void MenuChoice_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                switch (((Border)sender).Name.ToString().ToLower())
                {
                    case "brdchoicezero":
                        ContinueAdventure();
                        break;
                    case "brdchoiceone":
                        StartNewAdventure();
                        break;
                    case "brdchoicetwo":
                        LoadAdventure();
                        break;
                    case "brdchoicethree":
                        CreateAdventure();
                        break;
                    case "brdchoicefour":
                        AppShutdown();
                        break;
                    default:
                        break;
                }
            }
            catch(Exception err)
            {
                exHand.LogException(err, "MainMenu-MenuChoice_Click");
            }
        }

        private void ContinueAdventure()
        {
            try
            {
                //IF IN ADVENTURE = RESUME, ELSE IF PREVIOUSLY IN ADVENTURE = RESUM, ELSE START NEW
                if (AppGlobals.currGlobalAdventure != null)
                {
                    if (AppGlobals.player.isPaused)
                        AppGlobals.player.UnPauseMusic();

                    Story storyPage = new Story(AppGlobals.currGlobalAdventure);
                    this.NavigationService.Navigate(storyPage);
                }
                else if (AppGlobals.currGlobalAdventure == null)
                {
                    DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.saveGameDir);
                    FileInfo mostRecentGame = null;
                    if (currentDir.GetFiles().Count() > 0)
                    {
                        mostRecentGame = currentDir.GetFiles().OrderByDescending(d => d.LastWriteTime).Where(d => d.Name.Contains(".advo")).First();
                        //CHANGE FROM .ADVO TO .XAML TO LOAD UP DATA
                        File.Move(mostRecentGame.FullName, System.IO.Path.ChangeExtension(mostRecentGame.FullName, ".xaml"));

                        using (FileStream fs = new FileStream(@System.IO.Path.ChangeExtension(mostRecentGame.FullName, ".xaml"), FileMode.Open))
                        {
                            try
                            {
                                var document = (FlowDocument)XamlReader.Load(fs);

                                AppGlobals.currGlobalAdventure = new Adventure();
                                AppGlobals.currGlobalAdventure.ongoingStory = document;
                            }
                            catch (SerializationException err)
                            {
                                exHand.LogException(err, "MainMenu-ContinueAdventure");
                            }
                            finally
                            {
                                fs.Close();
                            }
                        }
                    }

                    currentDir = new DirectoryInfo(@AppGlobals.saveGameMetaDir);
                    if (currentDir.GetFiles().Count() > 0)
                    {
                        FileInfo mostRecentGameMeta = currentDir.GetFiles().OrderByDescending(d => d.LastWriteTime).Where(d => d.Name.Contains(".advm")).First();
                        //CHANGE FROM .ADVM TO .XAML TO LOAD UP DATA
                        File.Move(mostRecentGameMeta.FullName, System.IO.Path.ChangeExtension(mostRecentGameMeta.FullName, ".xaml"));

                        using (FileStream fs = new FileStream(@System.IO.Path.ChangeExtension(mostRecentGameMeta.FullName, ".xaml"), FileMode.Open))
                        {
                            try
                            {
                                var document = (FlowDocument)XamlReader.Load(fs);
                                if (document != null)
                                {
                                    foreach (var block in document.Blocks)
                                    {
                                        TextRange textRange = new TextRange(block.ContentStart, block.ContentEnd);
                                        string[] storyMeta = textRange.Text.Split('[');
                                        foreach (string data in storyMeta)
                                        {
                                            if (data.Contains("%ARMOR%"))
                                                AppGlobals.currGlobalAdventure.Armor = Convert.ToInt32(data.Split(']').Last());
                                            if (data.Contains("%ARMORSET%"))
                                                AppGlobals.currGlobalAdventure.ArmorSet.ArmorName = data.Split(']').Last();
                                            if (data.Contains("%AUTHOR%"))
                                                AppGlobals.currGlobalAdventure.Author = data.Split(']').Last();
                                            if (data.Contains("%GENRE%"))
                                                AppGlobals.currGlobalAdventure.Genre = data.Split(']').Last();
                                            if (data.Contains("%CHARACTERNAME%"))
                                                AppGlobals.currGlobalAdventure.CharacterName = data.Split(']').Last();
                                            if (data.Contains("%CHARACTERTITLE%"))
                                                AppGlobals.currGlobalAdventure.CharacterTitle = data.Split(']').Last();
                                            if (data.Contains("%CURRENTCHAPTER%"))
                                                AppGlobals.currGlobalAdventure.CurrentChapter = Convert.ToInt32(data.Split(']').Last());
                                            if (data.Contains("%DECISIONSMADE%"))
                                                AppGlobals.currGlobalAdventure.DecisionsMade = Convert.ToInt32(data.Split(']').Last());
                                            if (data.Contains("%FOLDERPATH%"))
                                                AppGlobals.currGlobalAdventure.folderPath = data.Split(']').Last();
                                            if (data.Contains("%HEALTH%"))
                                                AppGlobals.currGlobalAdventure.Health = Convert.ToInt32(data.Split(']').Last());
                                            if (data.Contains("%LUCK%"))
                                                AppGlobals.currGlobalAdventure.Luck = Convert.ToInt32(data.Split(']').Last());
                                            if (data.Contains("%MAGIC%"))
                                                AppGlobals.currGlobalAdventure.Magic = Convert.ToInt32(data.Split(']').Last());
                                            if (data.Contains("%MELEEWEAPON%"))
                                                AppGlobals.currGlobalAdventure.MeleeWeapon.WeaponName = data.Split(']').Last();
                                            if (data.Contains("%PUBLISHDATE%"))
                                                AppGlobals.currGlobalAdventure.PublishDate = data.Split(']').Last();
                                            if (data.Contains("%RANGEDWEAPON%"))
                                                AppGlobals.currGlobalAdventure.RangedWeapon.WeaponName = data.Split(']').Last();
                                            if (data.Contains("%SPEED%"))
                                                AppGlobals.currGlobalAdventure.Speed = Convert.ToInt32(data.Split(']').Last());
                                            if (data.Contains("%STORYLENGTH%"))
                                                AppGlobals.currGlobalAdventure.StoryLength = Convert.ToInt32(data.Split(']').Last());
                                            if (data.Contains("%SUMMARY%"))
                                                AppGlobals.currGlobalAdventure.Summary = data.Split(']').Last();
                                            if (data.Contains("%TITLE%"))
                                                AppGlobals.currGlobalAdventure.Title = data.Split(']').Last();
                                        }
                                    }
                                }
                            }
                            catch (SerializationException err)
                            {
                                exHand.LogException(err, "StoryGenerator-SaveAdventure");
                            }
                            finally
                            {
                                fs.Close();
                            }
                        }
                    }

                    if (AppGlobals.currGlobalAdventure == null)
                    {
                        StorySelection storySelectionPage = new StorySelection();
                        this.NavigationService.Navigate(storySelectionPage);
                    }
                    else
                    {
                        Story storyPage = new Story(AppGlobals.currGlobalAdventure);
                        this.NavigationService.Navigate(storyPage);
                    }
                }
                else
                    StartNewAdventure();
            }
            catch (Exception err)
            {
                exHand.LogException(err, "MainMenu-ContinueAdventure");
            }
        }

        private void StartNewAdventure()
        {
            try
            {
                StorySelection selectionPage = new StorySelection();
                this.NavigationService.Navigate(selectionPage);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "MainMenu-StartNewAdventure");
            }
        }

        private void LoadAdventure()
        {
            try
            {
                if (AppGlobals.inStoryMode)
                    SaveStory();
                else
                {
                    LoadPage loadPage = new LoadPage();
                    this.NavigationService.Navigate(loadPage);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "MainMenu-LoadAdventure");
            }
        }

        private void CreateAdventure()
        {
            try
            {
                if (AppGlobals.inStoryMode)
                    SaveStory();
                else
                {
                    StoryGeneratorActionPage actionPage = new StoryGeneratorActionPage();
                    this.NavigationService.Navigate(actionPage);

                    //StoryGeneratorOLD
                    //StoryGeneratorOLD generatorPage = new StoryGeneratorOLD();
                    //this.NavigationService.Navigate(generatorPage);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "MainMenu-CreateAdventure");
            }
        }

        private void AppShutdown()
        {
            try
            {
                if (AppGlobals.inStoryMode)
                {
                    SaveStory();
                    Application.Current.Shutdown();
                }
                else
                    Application.Current.Shutdown();
            }
            catch (Exception err)
            {
                exHand.LogException(err, "MainMenu-AppShutdown");
            }

        }

        private void SaveStory()
        {
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.saveGameDir);

                var saveFile = currentDir.GetFiles().Where(d => d.Name == AppGlobals.currGlobalAdventure.Title + ".advo");
                if (saveFile.Count() > 0)
                {
                    File.Delete(saveFile.First().FullName);
                }
                FileInfo mostRecentGame = currentDir.GetFiles().OrderByDescending(d => d.LastWriteTime).Where(d => d.Name.Contains(".xaml")).First();
                //CHANGE FROM .ADVO TO .XAML TO LOAD UP DATA
                File.Move(mostRecentGame.FullName, System.IO.Path.ChangeExtension(mostRecentGame.FullName, ".advo"));

                currentDir = new DirectoryInfo(@AppGlobals.saveGameMetaDir);
                var metaFile = currentDir.GetFiles().Where(d => d.Name == AppGlobals.currGlobalAdventure.Title + ".advm");
                if (metaFile.Count() > 0)
                {
                    File.Delete(metaFile.First().FullName);
                }
                FileInfo mostRecentGameMeta = currentDir.GetFiles().OrderByDescending(d => d.LastWriteTime).Where(d => d.Name.Contains(".xaml")).First();
                //CHANGE FROM .ADVM TO .XAML TO LOAD UP DATA
                File.Move(mostRecentGameMeta.FullName, System.IO.Path.ChangeExtension(mostRecentGameMeta.FullName, ".advm"));
            }
            catch (SerializationException err)
            {
                exHand.LogException(err, "MainMenu-SaveStory");
            }
            finally
            {
                AppGlobals.currGlobalAdventure = null;
                AppGlobals.inStoryMode = false;
            }
            
        }
    }
}
