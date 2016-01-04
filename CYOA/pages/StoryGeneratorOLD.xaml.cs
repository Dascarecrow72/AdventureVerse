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
using System.Diagnostics;

namespace CYOA
{
    /// <summary>
    /// Interaction logic for StoryGeneratorOLD.xaml
    /// </summary>
    public partial class StoryGeneratorOLD : Page
    {
        public StoryGeneratorOLD()
        {
            InitializeComponent();
            Loaded += StoryGenerator_Loaded;
        }

        private int chapterNumGl = 1;
        private bool workComplete = false;
        private string storyTitle = string.Empty;
        private string currDir = string.Empty;
        ExceptionHandling exHand = new ExceptionHandling();

        void StoryGenerator_Loaded(object sender, RoutedEventArgs e)
        {
            imgLRTT.ToolTip = AppGlobals.StoryGeneratorStageOneToolTip;
            imgLLTT.ToolTip = AppGlobals.StoryGeneratorStageOneToolTip;
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
                    case "brdchoiceone":
                        UploadStory();
                        break;
                    case "brdchoicetwo":
                        UploadImages();
                        break;
                    case "brdchoicethree":
                        SaveAdventure();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-MenuChoice_Click");
            }
        }

        private void UploadStory()
        {
            try
            {
                OpenFileDialog fDialog = new OpenFileDialog();
                fDialog.Title = "AdventureVerse Story Upload";
                fDialog.InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                fDialog.Filter = "Text (.txt;.doc;.docx;.rtf)|*.*|Text (.txt)|*.txt|Word (.doc)|*.doc|Wordx (.docx)|*.docx|Rich Text (.rtf)|*.rtf";
                DialogResult result = fDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    if (fDialog.FileName.Contains(".docx"))
                    {
                        storyTitle = fDialog.SafeFileName;
                        int index = storyTitle.LastIndexOf(".");
                        if (index > 0)
                            storyTitle = storyTitle.Substring(0, index);

                        GenerateFolders();
                        ReadDocx(fDialog.FileName);
                        if (workComplete)
                        {
                            spLeftLower.IsEnabled = false;
                            spRightLower.IsEnabled = false;
                            spRightUpper.IsEnabled = false;
                            pupWorkingBar.IsOpen = true;
                        }
                    }
                    else
                    {
                        ReadDocs(fDialog.FileName);
                        if (workComplete)
                        {
                            spLeftLower.IsEnabled = false;
                            spRightLower.IsEnabled = false;
                            spRightUpper.IsEnabled = false;
                            pupWorkingBar.IsOpen = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-UploadStory");
            }
        }

        //CONVERTS FROM DOCX TO FLOWDOC, THEN CALLS SERIALIZESTORYFLOWDOC()
        private void ReadDocx(string path)
        {
            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    var flowDocumentConverter = new DocxToFlowDocumentConverter(stream);
                    flowDocumentConverter.Read();
                    FlowDocument flowDoc = flowDocumentConverter.Document;

                    SerializeStoryFlowDoc(flowDoc);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-ReadDocx");
            }
        }

        private void ReadDocs(string path)
        {
            try
            {
                //NEED IMPLEMENT
                throw new NotImplementedException();
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-ReadDocs");
            }
        }

        private void UploadImages()
        {
            try
            {
                OpenFileDialog fDialog = new OpenFileDialog();
                fDialog.Title = "AdventureVerse Scene Upload";
                fDialog.InitialDirectory = @Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                fDialog.Filter = "Images (.jpg;.png;.bmp;.gif)|*.*|JPG (.jpg)|*.jpg|PNG (.png)|*.png|Bitmap (.bmp)|*.bmp|GIF (.gif)|*.gif";
                DialogResult result = fDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    workComplete = false;
                    SaveSceneImages(fDialog.FileName, fDialog.SafeFileName);
                    if (workComplete)
                    {
                        spLeftLower.IsEnabled = false;
                        spRightLower.IsEnabled = false;
                        spRightUpper.IsEnabled = false;
                        pupText.Text = "Image Uploaded";
                        pupWorkingBar.IsOpen = true;
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-UploadImages");
            }
        }

        private void SaveSceneImages(string imgPath, string imgSafeName)
        {
            try
            {
                string newDir = currDir + "/" + imgSafeName;
                //SaveAdventure FLOWDOC TO XAML
                using (FileStream fs = File.Open(@currDir + "/" + storyTitle + ".xaml", FileMode.Create))
                {
                    File.Copy(@imgPath, @newDir);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-SaveSceneImages");
            }
            workComplete = true;
        }

        private void SaveAdventure()
        {
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.advnGameDir);
                foreach (FileInfo files in currentDir.GetFiles())
                {
                    if (files.Name == storyTitle + ".advn")
                        break;
                }
                ZipFile.CreateFromDirectory(@currDir, @AppGlobals.advnGameDir + "/" + storyTitle + ".advn");
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-SaveAdventure");
            }
        }

        private void SerializeStoryFlowDoc(FlowDocument flowDoc)
        {
            try
            {
                //WILL NEED TO IMPLEMENT GEAR/STAT INTERACTIONS

                //FOR SAVING---CREATE NEW XAML DOC W/ PROGRESS. IE CHAPTER 1 + DECISION + CHAPTER 2 OR SOMETHIGN LIKE THAT

                List<Block> docBlocks = flowDoc.Blocks.ToList();
                FlowDocument newChapter = new FlowDocument();
                FlowDocument chapterMetaData = new FlowDocument();
                FlowDocument storyMetaData = new FlowDocument();
                foreach (Block indivBlock in docBlocks)
                {
                    TextRange range = new TextRange(indivBlock.ContentStart, indivBlock.ContentEnd);
                    //NEED END TAG AT THE END
                    if (range.Text != "")
                    {
                        if (!range.Text.StartsWith("[%END%]"))
                        {
                            TextRange nextRange = new TextRange(indivBlock.NextBlock.ContentStart, indivBlock.NextBlock.ContentEnd);
                            switch (range.Text.Substring(0, 2))
                            {
                                case "[%":
                                    {
                                        //SAVE THE TEXT CHAPTER AND CLEAR FOR CHAPTER METADATA
                                        if (newChapter.Blocks.Count > 0)
                                        {
                                            SaveSerializedFlowDocChapter(newChapter, chapterNumGl);
                                            newChapter.Blocks.Clear();
                                        }

                                        //STORY METADATA
                                        if (range.Text.StartsWith("[%TITLE%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%AUTHOR%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%PUBLISHDATE%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%GENRE%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%SUMMARY%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%MELEEWEAPON%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%RANGEDWEAPON%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%ARMORSET%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%CHARACTERNAME%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%CHARACTERTITLE%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%HEALTH%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%ARMOR%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%SPEED%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%MAGIC%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }
                                        if (range.Text.StartsWith("[%LUCK%]"))
                                        {
                                            storyMetaData.Blocks.Add(indivBlock);
                                        }

                                        //CHAPTER METADATA
                                        if (range.Text.StartsWith("[%CHAPTER%]"))
                                        {
                                            string chapNum = Regex.Replace(range.Text, "[^.0-9]", "");
                                            chapterNumGl = Convert.ToInt32(chapNum);

                                            chapterMetaData.Blocks.Add(indivBlock);
                                            if (!nextRange.Text.StartsWith("[%") && !nextRange.Text.StartsWith(" "))
                                                SaveSerializedFlowDocChapterMetaData(chapterMetaData, chapterNumGl);
                                        }
                                        if (range.Text.StartsWith("[%SCENE%]"))
                                        {
                                            chapterMetaData.Blocks.Add(indivBlock);
                                            if (!nextRange.Text.StartsWith("[%") && !nextRange.Text.StartsWith(" "))
                                                SaveSerializedFlowDocChapterMetaData(chapterMetaData, chapterNumGl);
                                        }
                                        if (range.Text.StartsWith("[%MUSIC%]"))
                                        {
                                            chapterMetaData.Blocks.Add(indivBlock);
                                            if (!nextRange.Text.StartsWith("[%") && !nextRange.Text.StartsWith(" "))
                                                SaveSerializedFlowDocChapterMetaData(chapterMetaData, chapterNumGl);
                                        }
                                        if (range.Text.StartsWith("[%CHOICE%]"))
                                        {
                                            chapterMetaData.Blocks.Add(indivBlock);
                                            if (!nextRange.Text.StartsWith("[%") && !nextRange.Text.StartsWith(" "))
                                                SaveSerializedFlowDocChapterMetaData(chapterMetaData, chapterNumGl);
                                        }
                                        if (range.Text.StartsWith("[%ANSWERONE%]"))
                                        {
                                            chapterMetaData.Blocks.Add(indivBlock);
                                            if (!nextRange.Text.StartsWith("[%") && !nextRange.Text.StartsWith(" "))
                                                SaveSerializedFlowDocChapterMetaData(chapterMetaData, chapterNumGl);
                                        }
                                        if (range.Text.StartsWith("[%ANSWERTWO%]"))
                                        {
                                            chapterMetaData.Blocks.Add(indivBlock);
                                            if (!nextRange.Text.StartsWith("[%") && !nextRange.Text.StartsWith(" "))
                                                SaveSerializedFlowDocChapterMetaData(chapterMetaData, chapterNumGl);
                                        }
                                        if (range.Text.StartsWith("[%ANSWERTHREE%]"))
                                        {
                                            chapterMetaData.Blocks.Add(indivBlock);
                                            if (!nextRange.Text.StartsWith("[%") && !nextRange.Text.Trim().StartsWith(" "))
                                                SaveSerializedFlowDocChapterMetaData(chapterMetaData, chapterNumGl);
                                        }


                                        //GEAR/STATS METADATA (TIED TO CHAPTER STILL...)
                                        //PLACEHOLDER
                                        //if (range.Text.StartsWith("[%LOOT%]"))
                                        //{
                                        //    //WHAT TO DO HERE.... WHAT TO DO....
                                        //}
                                        //PLACEHOLDER
                                    }
                                    break;
                                default:
                                    newChapter.Blocks.Add(indivBlock);
                                    break;
                            }
                        }
                        else
                        {
                            SaveSerializedFlowDocChapter(newChapter, chapterNumGl);
                            SaveStoryMetaData(storyMetaData);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-SerializeStoryFlowDoc");
            }
        }

        private void GenerateFolders()
        {
            try
            {
                currDir = AppGlobals.adventureDir + '/' + storyTitle;
                if (!Directory.Exists(@currDir))
                {
                    Directory.CreateDirectory(@currDir);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-GenerateFloders");
            }
        }

        private void SaveSerializedFlowDocChapter(FlowDocument flowDoc, int chapterNum)
        {
            try
            {
                //SaveAdventure FLOWDOC TO XAML
                if (chapterNum > 0)
                {
                    using (FileStream fs = File.Open(@currDir + "/" + storyTitle + chapterNum + ".xaml", FileMode.Create))
                    {
                        CYOA.utilities.XamlWriter.Save(flowDoc, fs);
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-SaveSerializedFlowDocChapter");
            }
        }

        private void SaveSerializedFlowDocChapterMetaData(FlowDocument flowDoc, int chapterNum)
        {
            try
            {
                string descrip = "-meta";
                //SaveAdventure FLOWDOC TO XAML
                if (chapterNum > 0)
                    using (FileStream fs = File.Open(@currDir + "/" + storyTitle + "-" + chapterNum + descrip + ".xaml", FileMode.Create))
                    {
                        CYOA.utilities.XamlWriter.Save(flowDoc, fs);
                    }
                chapterNumGl++;
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-SaveSerializedFlowDocChapterMetaData");
            }
        }

        private void SaveStoryMetaData(FlowDocument flowDoc)
        {
            try
            {
                string descrip = "-adventuremeta";
                //SaveAdventure FLOWDOC TO XAML
                using (FileStream fs = File.Open(@currDir + "/" + storyTitle + descrip + ".xaml", FileMode.Create))
                {
                    CYOA.utilities.XamlWriter.Save(flowDoc, fs);
                    workComplete = true;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-SaveStoryMetaData");
            }
        }

        private void pupBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (pupText.Text == "Adventure Files Generated")
                {
                    brdChoiceTwo.IsEnabled = true;

                    spLeftLower.IsEnabled = true;
                    spRightLower.IsEnabled = true;
                    spRightUpper.IsEnabled = true;
                    pupWorkingBar.IsOpen = false;
                }
                else if (pupText.Text == "Image Uploaded")
                {
                    brdChoiceThree.IsEnabled = true;

                    spLeftLower.IsEnabled = true;
                    spRightLower.IsEnabled = true;
                    spRightUpper.IsEnabled = true;
                    pupWorkingBar.IsOpen = false;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGenerator-pupBtn_Click");
            }
        }
    }
}
