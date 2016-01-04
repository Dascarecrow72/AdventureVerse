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
using System.Runtime.Serialization;

namespace CYOA
{
    /// <summary>
    /// Interaction logic for LoadPage.xaml
    /// </summary>
    public partial class LoadPage : Page
    {
        public LoadPage()
        {
            InitializeComponent();
            Loaded += LoadPage_Loaded;
        }
        ExceptionHandling exHand = new ExceptionHandling();

        void LoadPage_Loaded(object sender, RoutedEventArgs e)
        {
            LookupSavesList();
        }

        private void LookupSavesList()
        {
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.saveGameDir);
                var gg = currentDir.GetFiles();
                foreach (FileInfo files in currentDir.GetFiles())
                {
                    if (files.FullName.Contains(".advo"))
                    {
                        string adventureTitle = files.Name;
                        int index = adventureTitle.LastIndexOf(".");
                        if (index > 0)
                            adventureTitle = adventureTitle.Substring(0, index);
                        lvAvailableSaves.Items.Add(adventureTitle);
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "LoadPage-LookupSavesList");
            }
        }


        private void lvAvailableSaves_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lvAvailableSaves.SelectedItem != null)
                    brdChoiceZero.IsEnabled = true;
                else
                    brdChoiceZero.IsEnabled = false;

                //LOADS THE META FILE BASED OFF THE SELETED ITEM
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.saveGameMetaDir);
                var gg = currentDir.GetFiles();
                FileInfo mostRecentGame = currentDir.GetFiles().Where(d => d.Name.Contains(((ListView)sender).SelectedItem.ToString() + ".advm")).First();
                //CHANGE FROM .ADVM TO .XAML TO LOAD UP DATA
                File.Copy(mostRecentGame.FullName, System.IO.Path.ChangeExtension(mostRecentGame.FullName, ".xaml"));

                using (FileStream fs = new FileStream(@System.IO.Path.ChangeExtension(mostRecentGame.FullName, ".xaml"), FileMode.Open))
                {
                    try
                    {
                        var document = (FlowDocument)XamlReader.Load(fs);

                        DisplaySelectedSaveDetails(document);
                    }
                    catch (SerializationException err)
                    {
                        exHand.LogException(err, "LoadPage-lvAvailableSaves_SelectionChanged");
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "LoadPage-lvAvailableSaves_SelectionChanged");
            }
        }

        private void DisplaySelectedSaveDetails(FlowDocument flowDoc)
        {
            try
            {
                //BREAK OUT INTO TAGS TO ASSIGN
                var characterName = "";
                var characterTitle = "";
                bool charNameDone = false;
                bool charTitleDone = false;
                List<Block> docBlocks = flowDoc.Blocks.ToList();
                foreach (Block indivBlock in docBlocks)
                {
                    TextRange textRange = new TextRange(indivBlock.ContentStart, indivBlock.ContentEnd);
                    string[] storyMeta = textRange.Text.Split('[');
                    foreach (string data in storyMeta)
                    {
                        if (data.Contains("%TITLE%"))
                        {
                            txtTitle.Text = data.Split(']').Last();
                        }
                        if (data.Contains("%AUTHOR%"))
                        {
                            txtAuthor.Text = data.Split(']').Last();
                        }
                        if (data.Contains("%PUBLISHDATE%"))
                        {
                            txtSaveDate.Text = data.Split(']').Last();
                        }
                        if (data.Contains("%STORYLENGTH%"))
                        {
                            txtStoryLength.Text = data.Split(']').Last();
                        }
                        if (data.Contains("%DECISIONSMADE%"))
                        {
                            txtDecisionsMade.Text = data.Split(']').Last();
                        }
                        if (data.Contains("%CHARACTERNAME%"))
                        {
                            characterName = data.Split(']').Last();
                            charNameDone = true;
                        }
                        if (data.Contains("%CHARACTERTITLE%"))
                        {
                            characterTitle = data.Split(']').Last();
                            charTitleDone = true;
                        }
                        if (charNameDone == true && charTitleDone == true)
                            txtCharacter.Text = characterName + ", " + characterTitle;
                    }
                }
                //FIND AND DELETE THE TEMP XAML FILE
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.saveGameMetaDir);
                FileInfo mostRecentGame = currentDir.GetFiles().Where(d => d.Name.Contains((lvAvailableSaves.SelectedItem.ToString() + ".xaml"))).First();
                File.Delete(mostRecentGame.FullName);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "LoadPage-DisplaySelectedSaveDetails");
            }
        }

        private void MenuChoice_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                switch (((Border)sender).Name.ToString().ToLower())
                {
                    case "brdchoicezero":
                        SelectSave();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "LoadPage-MenuChoice_Click");
            }
        }

        private void SelectSave()
        {
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.saveGameDir);
                FileInfo mostRecentGame = currentDir.GetFiles().Where(d => d.Name.Contains((lvAvailableSaves.SelectedItem.ToString() + ".advo"))).First();
                //CHANGE FROM .ADVO TO .XAML TO LOAD UP DATA
                File.Move(mostRecentGame.FullName, System.IO.Path.ChangeExtension(mostRecentGame.FullName, ".xaml"));

                currentDir = new DirectoryInfo(@AppGlobals.saveGameMetaDir);
                FileInfo mostRecentGameMeta = currentDir.GetFiles().Where(d => d.Name.Contains((lvAvailableSaves.SelectedItem.ToString() + ".advm"))).First();
                //CHANGE FROM .ADVM TO .XAML TO LOAD UP DATA
                File.Move(mostRecentGameMeta.FullName, System.IO.Path.ChangeExtension(mostRecentGameMeta.FullName, ".xaml"));

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
                        exHand.LogException(err, "LoadPage-SelectSave");
                    }
                    finally
                    {
                        fs.Close();
                    }
                }

                Story storyPage = new Story(AppGlobals.currGlobalAdventure);
                this.NavigationService.Navigate(storyPage);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "LoadPage-SelectSave");
            }
        }
    }
}
