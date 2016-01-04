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
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using CYOA.utilities;
using System.Windows.Markup;
using System.Windows.Controls.Primitives;
using System.IO.Compression;

namespace CYOA
{
    /// <summary>
    /// Interaction logic for StoryGeneratorStageTwo.xaml
    /// </summary>
    public partial class StoryGeneratorStageTwo : Page
    {
        public StoryGeneratorStageTwo(Adventure currCreation)
        {
            InitializeComponent();
            Loaded += StoryGenerator_Loaded;
            PopulateAdventure(currCreation);
        }

        ExceptionHandling exHand = new ExceptionHandling();
        Adventure newCreation = new Adventure();
        public List<FileInfo> creationSceneFiles = new List<FileInfo>();
        public List<FileInfo> creationMusicFiles = new List<FileInfo>();
        private int chapterNumGl = 1;
        FlowDocument currChapter = new FlowDocument();
        private string outcomeOne = "";
        private string outcomeTwo = "";
        private string outcomeThree = "";
        private bool continueSuccess = false;
        public string navChapterMetaDataPath = "";
        public string navChapterStoryDataPath = "";

        private void PopulateAdventure(Adventure currCreation)
        {
            newCreation = currCreation;
            GetSceneFiles();
            GetMusicFiles();
        }

        private void GetMusicFiles()
        {
            try
            {
                if (Directory.Exists(@AppGlobals.soundDir))
                {
                    DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.soundDir);

                    foreach (FileInfo files in currentDir.GetFiles())
                    {
                        creationMusicFiles.Add(files);
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-GetSceneFiles");
            }
        }

        private void GetSceneFiles()
        {
            try
            {
                if (Directory.Exists(@AppGlobals.creationGameDir + "/" + newCreation.Title))
                {
                    DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir + "/" + newCreation.Title);
                    creationSceneFiles.Clear();

                    foreach (FileInfo files in currentDir.GetFiles())
                    {
                        if (files.Extension == ".jpg" || files.Extension == ".png" || files.Extension == ".gif" || files.Extension == ".bmp")
                        {
                            creationSceneFiles.Add(files);
                        }
                    }
                    cmbScene.ItemsSource = null;
                    cmbScene.ItemsSource = creationSceneFiles;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-GetSceneFiles");
            }
        }

        void StoryGenerator_Loaded(object sender, RoutedEventArgs e)
        {
            imgLLTT.ToolTip = AppGlobals.StoryGeneratorStageOneToolTip;
            imgLRTT.ToolTip = AppGlobals.StoryGeneratorStageOneToolTip;    
            cmbMusic.ItemsSource = creationMusicFiles;
            cmbScene.ItemsSource = creationSceneFiles;
            cmbChoice.Items.Add("Choice One");
            cmbChoice.Items.Add("Choice Two");
            cmbChoice.Items.Add("Choice Three");
            for (int i = 1; i < 1000; i++)
                cmbOutcome.Items.Add(i);
            txtCurrChapter.Text = chapterNumGl.ToString();
            storyTree.BuildDir(newCreation.Title);
            storyTree.ReadFolderTree(newCreation.Title);
            storyTree.selectionChanged += new EventHandler(StoryTreeSelection_Changed);
        }

        private void txtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("^[^0-9\\s]+$"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void MenuChoice_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                switch (((Border)sender).Name.ToString().ToLower())
                {
                    case "brdchoicezero":
                        DisplayStoryTree();
                        break;
                    case "brdchoiceone":
                        NextChapter();
                        break;
                    case "brdchoicetwo":
                        SaveChapter();
                        break;
                    case "brdchoicethree":
                        DiscardChapter();
                        break;
                    case "brdchoicefour":
                        UploadSceneImage();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-MenuChoice_Click");
            }
        }

        private void UploadSceneImage()
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
                    GetSceneFiles();
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-UploadScenesForAdventure");
            }
        }

        private void SaveSceneImages(string imgPath)
        {
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir);
                DirectoryInfo selection = currentDir.GetDirectories().Where(d => d.Name.Contains(newCreation.Title)).First();

                string imgSafeName = System.IO.Path.GetFileName(imgPath);

                File.Copy(@imgPath, @selection.FullName + "/" + imgSafeName);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-SaveSceneImages");
            }
        }

        private void NextChapter()
        {
            try
            {
                SaveChapter();
                if (txtDirections != null && txtDocument != null)
                {
                    if (continueSuccess == true)
                    {
                        ClearData();
                        //CHECK IF NEXT CHAPTER EXISTS, IF SO GO FIND LATEST CHAPTER ++
                        if (Directory.Exists(@AppGlobals.creationGameDir + "/" + newCreation.Title))
                        {
                            var dir = Directory.GetFiles(@AppGlobals.creationGameDir + "/" + newCreation.Title);
                            var latest = dir.Last();

                            string latestFNO = latest.Substring(latest.LastIndexOf("\\") + 1);
                            string num = Regex.Replace(latestFNO, "[^.0-9]", "");
                            if (num.Any(char.IsDigit))
                            {
                                char[] trim = { '.' };
                                chapterNumGl = Convert.ToInt32(num.TrimEnd(trim));
                            }
                        }
                        if (chapterNumGl > 1)
                            chapterNumGl++;
                        txtCurrChapter.Text = chapterNumGl.ToString();

                        //REFRESH STORY TREE
                        storyTree.filesFromDir.Clear();
                        storyTree.tvStoryTree.Items.Clear();
                        storyTree.BuildDir(newCreation.Title);
                        storyTree.ReadFolderTree(newCreation.Title);
                    }
                }

            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-NextChapter");
            }
        }

        private void DiscardChapter()
        {
            try
            {
                ClearData();
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-DiscardChapter");
            }
        }

        private void SaveChapter()
        {
            try
            {
                if (ValidateData() == true)
                {
                    SaveChapterFlowDoc();
                    SaveChapterMetaDataFlowDoc();
                    continueSuccess = true;
                }
                else
                {
                    MessageBoxResult validationFailed = System.Windows.MessageBox.Show("ERROR: Please confirm that all critical fields are filled in.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-SaveChapter");
            }
        }

        private bool ValidateData()
        {
            if (txtDocument.Document.Blocks.Count > 0)
            {
                if (txtDirections.Text != "" && txtDirections.Text != "NULL")
                {
                    if (txtCurrChapter.Text != "" && txtCurrChapter.Text != "NULL")
                    {
                        if (txtChoiceOne.Text != "" && txtChoiceOne.Text != "NULL")
                        {
                            if (txtDirections.Text != "" && txtDirections.Text != "NULL")
                                return true;
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        private void ClearData()
        {
            try
            {
                txtArmorSet.Text = "";
                txtArmorStat.Text = "";
                txtChoiceOne.Text = "";
                txtChoiceTwo.Text = "";
                txtChoiceThree.Text = "";
                txtDirections.Text = "";
                txtDocument.Document.Blocks.Clear();
                txtHealthStat.Text = "";
                txtLuckStat.Text = "";
                txtMagicStat.Text = "";
                txtMeleeWeapon.Text = "";
                txtRangedWeapon.Text = "";
                txtSpeedStat.Text = "";
                cmbMusic.SelectedItem = null;
                cmbScene.SelectedItem = null;
                cmbChoice.SelectedItem = null;
                cmbOutcome.SelectedItem = null;
                continueSuccess = false;
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-ClearData");
            }
        }

        private void DisplayStoryTree()
        {
            try
            {
                if (storyTreePanel.Visibility == System.Windows.Visibility.Visible)
                    storyTreePanel.Visibility = System.Windows.Visibility.Collapsed;
                else if (storyTreePanel.Visibility == System.Windows.Visibility.Collapsed)
                    storyTreePanel.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-DisplayStoryTree");
            }
        }

        private void StoryTreeSelection_Changed(object sender, EventArgs e)
        {
            try
            {
                SaveChapter();

                if (Directory.Exists(@AppGlobals.creationGameDir + "/" + newCreation.Title))
                {
                    DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.creationGameDir + "/" + newCreation.Title);

                    foreach (FileInfo files in currentDir.GetFiles())
                    {
                        //FIND THE METADATA FOR CHAPTER
                        if (files.Name.Contains("-" + sender.ToString() + "-meta.xaml"))
                        {
                            navChapterMetaDataPath = files.FullName;
                        }
                        //FIND THE CHAPTER STORY
                        if (files.Name.Contains(sender.ToString() + ".xaml"))
                        {
                            navChapterStoryDataPath = files.FullName;
                        }
                    }

                    if (navChapterStoryDataPath != "" && navChapterMetaDataPath != "")
                    {
                        //ASSIGN NEW CHAPTER NUM
                        chapterNumGl = Convert.ToInt32(sender.ToString());
                        PopulateNavChapter();
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-StoryTreeSelection_Changed");
            }
        }

        private void PopulateNavChapter()
        {
            try
            {
                //LOAD CHAPTER STORY
                using (FileStream fs = File.OpenRead(@navChapterStoryDataPath))
                {
                    txtDocument.Document.Blocks.Clear();
                    FlowDocument document = (FlowDocument)XamlReader.Load(fs);
                    txtDocument.Document = document;
                }

                string theFileNameOnly = navChapterStoryDataPath.Substring(navChapterStoryDataPath.LastIndexOf("\\") + 1);
                string filename = Regex.Replace(theFileNameOnly, "[^.0-9]", "");
                char[] trim = { '.' };
                txtCurrChapter.Text = filename.TrimEnd(trim);

                //LOAD CHAPTER METADATA
                using (FileStream fs = File.OpenRead(@navChapterMetaDataPath))
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
                                if (indivString.Contains("%CHOICE%"))
                                {
                                    txtDirections.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%ANSWERONE%"))
                                {
                                    txtChoiceOne.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%ANSWERTWO%"))
                                {
                                    txtChoiceTwo.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%ANSWERTHREE%"))
                                {
                                    txtChoiceThree.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%OUTCOMEONE%"))
                                {
                                    outcomeOne = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%OUTCOMETWO%"))
                                {
                                    outcomeTwo = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%OUTCOMETHREE%"))
                                {
                                    outcomeThree = indivString.Split(']').Last();
                                }

                                if (indivString.Contains("%CHARMOR%"))
                                {
                                    txtArmorStat.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%CHHEALTH%"))
                                {
                                    txtHealthStat.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%CHLUCK%"))
                                {
                                    txtLuckStat.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%CHMAGIC%"))
                                {
                                    txtMagicStat.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%CHSPEED%"))
                                {
                                    txtSpeedStat.Text = indivString.Split(']').Last();
                                }

                                if (indivString.Contains("%CHARMORSET%"))
                                {
                                    txtArmorSet.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%CHMELEEWEAPON%"))
                                {
                                    txtMeleeWeapon.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%CHRANGEDWEAPON%"))
                                {
                                    txtRangedWeapon.Text = indivString.Split(']').Last();
                                }

                                if (indivString.Contains("%MUSIC%"))
                                {
                                    cmbMusic.SelectedItem = creationMusicFiles.First(d => d.Name == indivString.Split(']').Last());
                                    
                                }
                                if (indivString.Contains("%SCENE%"))
                                {
                                    cmbScene.SelectedValue = creationSceneFiles.First(d => d.Name == indivString.Split(']').Last());
                                }
                            }
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-PopulateNavChapter");
            }
        }

        private void SaveChapterFlowDoc()
        {
            try
            {
                //Saves story FLOWDOC TO XAML
                if (chapterNumGl > 0)
                {
                    string txt = string.Empty;
                    List<Block> docBlocks = txtDocument.Document.Blocks.ToList();
                    foreach (Block indivBlock in docBlocks)
                    {
                        TextRange range = new TextRange(indivBlock.ContentStart, indivBlock.ContentEnd);
                        txt = txt + range.Text;
                    }

                    if (txt != null && txt != string.Empty && txt != "")
                    {
                        using (FileStream fs = File.Open(@AppGlobals.creationGameDir + "/" + newCreation.Title + "/" + newCreation.Title + chapterNumGl + ".xaml", FileMode.Create))
                        {
                            CYOA.utilities.XamlWriter.Save(txtDocument.Document, fs);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-SaveChapterFlowDoc");
            }
        }

        private void SaveChapterMetaDataFlowDoc()
        {
            try
            {
                if (txtDirections.Text != null && txtDirections.Text != "" && txtDirections.Text != "ex: Decision?")
                {
                    //Saves chapter metadata
                    string descrip = "-meta";

                    FlowDocument currSaveMeta = new FlowDocument();
                    Paragraph newBlock = new Paragraph();

                    newBlock.Inlines.Add("[%CHOICE%]" + txtDirections.Text);
                    newBlock.Inlines.Add("[%ANSWERONE%]" + txtChoiceOne.Text);
                    newBlock.Inlines.Add("[%ANSWERTWO%]" + txtChoiceTwo.Text);
                    newBlock.Inlines.Add("[%ANSWERTHREE%]" + txtChoiceThree.Text);
                    newBlock.Inlines.Add("[%OUTCOMEONE%]" + outcomeOne);
                    newBlock.Inlines.Add("[%OUTCOMETWO%]" + outcomeTwo);
                    newBlock.Inlines.Add("[%OUTCOMETHREE%]" + outcomeThree);

                    newBlock.Inlines.Add("[%CHARMOR%]" + txtArmorStat.Text);
                    newBlock.Inlines.Add("[%CHHEALTH%]" + txtHealthStat.Text);
                    newBlock.Inlines.Add("[%CHLUCK%]" + txtLuckStat.Text);
                    newBlock.Inlines.Add("[%CHMAGIC%]" + txtMagicStat.Text);
                    newBlock.Inlines.Add("[%CHSPEED%]" + txtSpeedStat.Text);

                    newBlock.Inlines.Add("[%CHARMORSET%]" + txtArmorSet.Text);
                    newBlock.Inlines.Add("[%CHMELEEWEAPON%]" + txtMeleeWeapon.Text);
                    newBlock.Inlines.Add("[%CHRANGEDWEAPON%]" + txtRangedWeapon.Text);

                    //NEED TO POPULATE LIST THEN WIRE UP
                    if (cmbMusic.SelectedValue != null)
                        newBlock.Inlines.Add("[%MUSIC%]" + cmbMusic.SelectedValue.ToString());

                    if (cmbScene.SelectedValue != null)
                        newBlock.Inlines.Add("[%SCENE%]" + cmbScene.SelectedValue.ToString());

                    currSaveMeta.Blocks.Add(newBlock);

                    //SaveAdventure FLOWDOC TO XAML
                    if (chapterNumGl > 0)
                        using (FileStream fs = File.Open(@AppGlobals.creationGameDir + "/" + newCreation.Title + "/" + newCreation.Title + "-" + chapterNumGl + descrip + ".xaml", FileMode.Create))
                        {
                            CYOA.utilities.XamlWriter.Save(currSaveMeta, fs);
                        }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-SaveChapterMetaDataFlowDoc");
            }
        }

        private void cmbChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmbOutcome.IsEnabled = true;
                if (cmbChoice.SelectedValue != null && cmbChoice.SelectedValue.ToString() == "Choice One")
                {
                    if (outcomeOne != "")
                        cmbOutcome.SelectedItem = outcomeOne;
                    else
                        cmbOutcome.SelectedItem = null;
                }
                else if (cmbChoice.SelectedValue != null && cmbChoice.SelectedValue.ToString() == "Choice Two")
                {
                    if (outcomeTwo != "")
                        cmbOutcome.SelectedItem = outcomeTwo;
                    else
                        cmbOutcome.SelectedItem = null;
                }
                else if (cmbChoice.SelectedValue != null && cmbChoice.SelectedValue.ToString() == "Choice Three")
                {
                    if (outcomeThree != "")
                        cmbOutcome.SelectedItem = outcomeThree;
                    else
                        cmbOutcome.SelectedItem = null;
                }
                else
                    cmbOutcome.SelectedItem = null;
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-cmbChoice_SelectionChanged");
            }
        }

        private void cmbOutcome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbChoice.SelectedValue != null && cmbChoice.SelectedValue.ToString() == "Choice One")
                {
                    if (cmbOutcome.SelectedValue != null)
                        outcomeOne = cmbOutcome.SelectedValue.ToString();
                }
                if (cmbChoice.SelectedValue != null && cmbChoice.SelectedValue.ToString() == "Choice Two")
                {
                    if (cmbOutcome.SelectedValue != null)
                        outcomeTwo = cmbOutcome.SelectedValue.ToString();
                }
                if (cmbChoice.SelectedValue != null && cmbChoice.SelectedValue.ToString() == "Choice Three")
                {
                    if (cmbOutcome.SelectedValue != null)
                        outcomeThree = cmbOutcome.SelectedValue.ToString();
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageTwo-cmbOutcome_SelectionChanged");
            }
        }
    }
}
