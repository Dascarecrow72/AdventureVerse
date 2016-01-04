using CYOA.cs;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CYOA.pages
{
    /// <summary>
    /// Interaction logic for StoryGeneratorActionPage.xaml
    /// </summary>
    public partial class StoryGeneratorActionPage : Page
    {
        public StoryGeneratorActionPage()
        {
            InitializeComponent();
            Loaded += StoryGeneratorActionPage_Loaded;
        }
        ExceptionHandling exHand = new ExceptionHandling();

        void StoryGeneratorActionPage_Loaded(object sender, RoutedEventArgs e)
        {
            LookupCreationsList();
        }

        private void LookupCreationsList()
        {
            try
            {
                lvAvailableCreations.Items.Clear();
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir);
                var creationDirs = currentDir.GetDirectories();
                foreach (var adventureTitle in creationDirs)
                {
                    lvAvailableCreations.Items.Add(adventureTitle);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-LookupCreationsList");
            }
        }


        private void lvAvailableCreations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lvAvailableCreations.SelectedItem != null)
                {
                    brdChoiceZero.IsEnabled = true;
                    brdChoiceOne.IsEnabled = true;
                    brdChoiceTwo.IsEnabled = true;
                    brdChoiceThree.IsEnabled = true;
                }
                else
                {
                    brdChoiceZero.IsEnabled = false;
                    brdChoiceOne.IsEnabled = false;
                    brdChoiceTwo.IsEnabled = false;
                    brdChoiceThree.IsEnabled = false;
                }

                //LOADS THE META FILE BASED OFF THE SELETED ITEM
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir);
                DirectoryInfo selection = currentDir.GetDirectories().Where(d => d.Name.Contains(((ListView)sender).SelectedItem.ToString())).First();
                txtLastEdit.Text = selection.GetFiles().OrderByDescending(d => d.LastWriteTime).First().LastWriteTime.ToShortDateString();
                FileInfo selectionCreationMeta = selection.GetFiles().Where(d => d.Name.Contains(((ListView)sender).SelectedItem.ToString() + "-creationmeta.xaml")).First();

                using (FileStream fs = new FileStream(@selectionCreationMeta.FullName, FileMode.Open))
                {
                    try
                    {
                        var document = (FlowDocument)XamlReader.Load(fs);

                        DisplaySelectedCreationDetails(document);
                    }
                    catch (SerializationException err)
                    {
                        exHand.LogException(err, "StoryGeneratorActionPage-lvAvailableCreations_SelectionChanged");
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-lvAvailableCreations_SelectionChanged");
            }
        }

        private void DisplaySelectedCreationDetails(FlowDocument flowDoc)
        {
            try
            {
                //BREAK OUT INTO TAGS TO ASSIGN
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
                        if (data.Contains("%GENRE%"))
                        {
                            txtGenre.Text = data.Split(']').Last();
                        }
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-DisplaySelectedCreationDetails");
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
                        UploadScenesForAdventure();
                        break;
                    case "brdchoicetwo":
                        FinalizeAdventureCreation();
                        break;
                    case "brdchoicethree":
                        DeleteCreation();
                        break;
                    case "brdchoicefour":
                        StartNewAdventure();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-MenuChoice_Click");
            }
        }

        private void StartNewAdventure()
        {
            try
            {
                Adventure newAdv = new Adventure();
                StoryGeneratorStageOne generatorPageStageOne = new StoryGeneratorStageOne(newAdv);
                this.NavigationService.Navigate(generatorPageStageOne);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-StartNewAdventure");
            }
        }

        private void DeleteCreation()
        {
            try
            {
                MessageBoxResult shouldDelete = MessageBox.Show("WARNING: You are about to delete this entire adventure. Do you wish to delete?", "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                if (shouldDelete == MessageBoxResult.Yes)
                {
                    DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir);
                    DirectoryInfo selection = currentDir.GetDirectories().Where(d => d.Name.Contains(lvAvailableCreations.SelectedItem.ToString())).First();
                    selection.Delete(true);
                    LookupCreationsList();
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-DeleteCreation");
            }
        }

        private void FinalizeAdventureCreation()
        {
            try
            {
                MessageBoxResult shouldDelete = MessageBox.Show("NOTICE: You are about to finalize this adventure for play. Do you wish to proceed?", "Finalize Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (shouldDelete == MessageBoxResult.Yes)
                {
                    DirectoryInfo advnDir = new DirectoryInfo(@AppGlobals.advnGameDir);

                    DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir);
                    DirectoryInfo selection = currentDir.GetDirectories().Where(d => d.Name.Contains(lvAvailableCreations.SelectedItem.ToString())).First();
                    foreach (FileInfo files in advnDir.GetFiles())
                    {
                        if (files.Name == selection.Name + ".advn")
                            files.Delete();
                            break;
                    }

                    if (advnDir.GetFiles().Where(d => d.Name == (selection.Name + ".advn")).Count() > 0)
                    {
                        var existingADVN = advnDir.GetFiles().Where(d => d.Name == (selection.Name + ".advn")).First().FullName;
                        File.Delete(existingADVN);
                    }
                    ZipFile.CreateFromDirectory(selection.FullName, @AppGlobals.advnGameDir + "/" + selection.Name + ".advn");

                    if (!Directory.Exists(@AppGlobals.adventureDir + "/" + selection.Name))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.adventureDir + "/" + selection.Name);
                        di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    }
                    else
                    {
                        DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.adventureDir + "/" + selection.Name);
                        di.Delete(true);
                        DirectoryInfo diNew = Directory.CreateDirectory(@AppGlobals.adventureDir + "/" + selection.Name);
                        diNew.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    }

                    foreach (FileInfo files in selection.GetFiles())
                    {
                        if (files.Name.Contains("-creationmeta.xaml"))
                        {
                            File.Copy(files.FullName, @AppGlobals.adventureDir + "/" + selection.Name + "/" + selection.Name + "-adventuremeta.xaml");
                        }
                        else
                        {
                            File.Copy(files.FullName,@AppGlobals.adventureDir + "/" + selection.Name + "/" + files.Name);
                        }
                    }

                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-FinalizeAdventureCreation");
            }
        }

        private void UploadScenesForAdventure()
        {
            try
            {
                System.Windows.Forms.OpenFileDialog fDialog = new System.Windows.Forms.OpenFileDialog();
                fDialog.Title = "AdventureVerse Scene Upload";
                fDialog.Multiselect = true;
                fDialog.InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                fDialog.Filter = "Images (.jpg;.png;.bmp;.gif)|*.*|JPG (.jpg)|*.jpg|PNG (.png)|*.png|Bitmap (.bmp)|*.bmp|GIF (.gif)|*.gif";
                System.Windows.Forms.DialogResult result = fDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (var file in fDialog.FileNames)
                    {
                        SaveSceneImages(file);
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-UploadScenesForAdventure");
            }
        }

        private void SaveSceneImages(string imgPath)
        {
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir);
                DirectoryInfo selection = currentDir.GetDirectories().Where(d => d.Name.Contains(lvAvailableCreations.SelectedItem.ToString())).First();

                string imgSafeName = System.IO.Path.GetFileName(imgPath);

                File.Copy(@imgPath, @selection.FullName + "/" + imgSafeName);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-SaveSceneImages");
            }
        }

        private void SelectAdventure()
        {
            try
            {
                AppGlobals.currGlobalAdventure = new Adventure();
                //LOADS THE META FILE BASED OFF THE SELETED ITEM
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir);
                var exists = currentDir.GetDirectories().Where(d => d.Name.Contains(lvAvailableCreations.SelectedItem.ToString())).Count();
                if (exists == 0)
                {
                    StoryGeneratorStageOne stageOneE = new StoryGeneratorStageOne(AppGlobals.currGlobalAdventure);
                    this.NavigationService.Navigate(stageOneE);
                    return;
                }

                DirectoryInfo selection = currentDir.GetDirectories().Where(d => d.Name.Contains(lvAvailableCreations.SelectedItem.ToString())).First();
                
                FileInfo selectionCreationMeta = selection.GetFiles().Where(d => d.Name.Contains(lvAvailableCreations.SelectedItem.ToString() + "-creationmeta.xaml")).First();

                using (FileStream fs = new FileStream(@selectionCreationMeta.FullName, FileMode.Open))
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
                                    if (data.Contains("%CANPLAYEREDIT%"))
                                        AppGlobals.currGlobalAdventure.CanPlayerEdit = Convert.ToBoolean(data.Split(']').Last());
                                    if (data.Contains("%CHARACTERNAME%"))
                                        AppGlobals.currGlobalAdventure.CharacterName = data.Split(']').Last();
                                    if (data.Contains("%CHARACTERTITLE%"))
                                        AppGlobals.currGlobalAdventure.CharacterTitle = data.Split(']').Last();
                                    if (data.Contains("%GENRE%"))
                                        AppGlobals.currGlobalAdventure.Genre = data.Split(']').Last();
                                    if (data.Contains("%HEALTH%"))
                                        AppGlobals.currGlobalAdventure.Health = Convert.ToInt32(data.Split(']').Last());
                                    if (data.Contains("%LUCK%"))
                                        AppGlobals.currGlobalAdventure.Luck = Convert.ToInt32(data.Split(']').Last());
                                    if (data.Contains("%MAGIC%"))
                                        AppGlobals.currGlobalAdventure.Magic = Convert.ToInt32(data.Split(']').Last());
                                    if (data.Contains("%MELEEWEAPON%"))
                                        AppGlobals.currGlobalAdventure.MeleeWeapon.WeaponName = data.Split(']').Last();
                                    if (data.Contains("%RANGEDWEAPON%"))
                                        AppGlobals.currGlobalAdventure.RangedWeapon.WeaponName = data.Split(']').Last();
                                    if (data.Contains("%RPGENABLED%"))
                                        AppGlobals.currGlobalAdventure.RPGEnabled = Convert.ToBoolean(data.Split(']').Last());
                                    if (data.Contains("%SPEED%"))
                                        AppGlobals.currGlobalAdventure.Speed = Convert.ToInt32(data.Split(']').Last());
                                    if (data.Contains("%SUMMARY%"))
                                        AppGlobals.currGlobalAdventure.Summary = data.Split(']').Last();
                                    if (data.Contains("%THEME%"))
                                        AppGlobals.currGlobalAdventure.Theme = data.Split(']').Last();
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

                StoryGeneratorStageOne stageOne = new StoryGeneratorStageOne(AppGlobals.currGlobalAdventure);
                this.NavigationService.Navigate(stageOne);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorActionPage-SelectAdventure");
            }
        }
    }
}
