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

namespace CYOA
{
    /// <summary>
    /// Interaction logic for StoryWindow.xaml
    /// </summary>
    public partial class StorySelection : Page
    {
        public StorySelection()
        {
            InitializeComponent();
            Loaded += StorySelection_Loaded;
        }
        ExceptionHandling exHand = new ExceptionHandling();

        void StorySelection_Loaded(object sender, RoutedEventArgs e)
        {
            LookupAdventuresList();
        }

        private void LookupAdventuresList()
        {
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.adventureDir);

                foreach (var adventureDir in currentDir.GetDirectories())
                {
                    foreach (FileInfo files in adventureDir.GetFiles())
                    {
                        if (files.FullName.Contains("adventuremeta"))
                        {
                            string adventureTitle = files.Name;
                            int index = adventureTitle.LastIndexOf("-");
                            if (index > 0)
                                adventureTitle = adventureTitle.Substring(0, index);
                            lvAvailableAdventures.Items.Add(adventureTitle);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StorySelection-LookupAdventuresList");
            }
        }


        private void lvAvailableAdventures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //LOADS THE META FILE BASED OFF THE SELETED ITEM
                var reconstrucedPath = ((ListView)sender).SelectedItem.ToString().ToLower() + "-adventuremeta.xaml";
                using (FileStream fs = File.OpenRead(@AppGlobals.adventureDir + "/" + ((ListView)sender).SelectedItem.ToString().ToLower() + "/" + reconstrucedPath))
                {
                    FlowDocument document = (FlowDocument)XamlReader.Load(fs);
                    DisplaySelectedAdventureDetails(document);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StorySelection-lvAvailableAdventures_SelectionChanged");
            }
        }

        private void DisplaySelectedAdventureDetails(FlowDocument flowDoc)
        {
            try
            {
                //BREAK OUT INTO TAGS TO ASSIGN
                List<Block> docBlocks = flowDoc.Blocks.ToList();
                foreach (Block indivBlock in docBlocks)
                {
                    TextRange range = new TextRange(indivBlock.ContentStart, indivBlock.ContentEnd);
                    string[] metaTexts = range.Text.Split('[');
                    foreach (string indivString in metaTexts)
                    {
                        if (indivString.Contains("%TITLE%"))
                        {
                            txtTitle.Text = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%AUTHOR%"))
                        {
                            txtAuthor.Text = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%PUBLISHDATE%"))
                        {
                            txtPublishDate.Text = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%SUMMARY%"))
                        {
                            txtSummary.Text = indivString.Split(']').Last();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StorySelection-DisplaySelectedAdventureDetails");
            }
        }

        private void MenuChoice_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                switch (((Border)sender).Name.ToString().ToLower())
                {
                    case "brdchoicezero":
                        SelectAdventure();
                        break;
                    case "brdchoiceone":
                        DownloadAdventures();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StorySelection-MenuChoice_Click");
            }
        }

        private void DownloadAdventures()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StorySelection-DownloadAdventures");
            }
        }

        private void SelectAdventure()
        {
            try
            {
                //LOADS THE ADVENTURE BASED OFF THE SELECTED ITEM
                Adventure selectedAdventure = new Adventure();
                var reconstrucedPath = lvAvailableAdventures.SelectedItem.ToString().ToLower() + "-adventuremeta.xaml";
                using (FileStream fs = File.OpenRead(@AppGlobals.adventureDir + "/" + lvAvailableAdventures.SelectedItem.ToString().ToLower() + "/" + reconstrucedPath))
                {
                    FlowDocument document = (FlowDocument)XamlReader.Load(fs);

                    //BREAK OUT INTO TAGS TO ASSIGN
                    List<Block> docBlocks = document.Blocks.ToList();
                    foreach (Block indivBlock in docBlocks)
                    {
                        TextRange range = new TextRange(indivBlock.ContentStart, indivBlock.ContentEnd);
                        string[] metaTexts = range.Text.Split('[');
                    foreach (string indivString in metaTexts)
                    {
                        if (indivString.Contains("%TITLE%"))
                        {
                            selectedAdventure.Title = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%AUTHOR%"))
                        {
                            selectedAdventure.Author = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%PUBLISHDATE%"))
                        {
                            selectedAdventure.PublishDate = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%SUMMARY%"))
                        {
                            selectedAdventure.Summary = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%GENRE%"))
                        {
                            selectedAdventure.Genre = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%THEME%"))
                        {
                            selectedAdventure.Theme = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%MELEEWEAPON%"))
                        {
                            selectedAdventure.MeleeWeapon = new Weapon();
                            selectedAdventure.MeleeWeapon.WeaponName = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%RANGEDWEAPON%"))
                        {
                            selectedAdventure.RangedWeapon = new Weapon();
                            selectedAdventure.RangedWeapon.WeaponName = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%ARMORSET%"))
                        {
                            selectedAdventure.ArmorSet = new Armor();
                            selectedAdventure.ArmorSet.ArmorName = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%CHARACTERNAME%"))
                        {
                            selectedAdventure.CharacterName = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%CHARACTERTITLE%"))
                        {
                            selectedAdventure.CharacterTitle = indivString.Split(']').Last();
                        }
                        if (indivString.Contains("%HEALTH%"))
                        {
                            selectedAdventure.Health = Convert.ToInt32(indivString.Split(']').Last());
                        }
                        if (indivString.Contains("%ARMOR%"))
                        {
                            selectedAdventure.Armor = Convert.ToInt32(indivString.Split(']').Last());
                        }
                        if (indivString.Contains("%SPEED%"))
                        {
                            selectedAdventure.Speed = Convert.ToInt32(indivString.Split(']').Last());
                        }
                        if (indivString.Contains("%MAGIC%"))
                        {
                            selectedAdventure.Magic = Convert.ToInt32(indivString.Split(']').Last());
                        }
                        if (indivString.Contains("%LUCK%"))
                        {
                            selectedAdventure.Luck = Convert.ToInt32(indivString.Split(']').Last());
                        }
                        
                    }
                        selectedAdventure.folderPath = AppGlobals.adventureDir + "/" + lvAvailableAdventures.SelectedItem.ToString().ToLower();
                    }
                }
                AppGlobals.currGlobalAdventure = selectedAdventure;
                Story storyPage = new Story(AppGlobals.currGlobalAdventure);
                this.NavigationService.Navigate(storyPage);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StorySelection-SelectAdventure");
            }
        }
    }
}
