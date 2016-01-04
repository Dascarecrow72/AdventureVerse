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
using System.Text.RegularExpressions;

namespace CYOA.utilities
{
    /// <summary>
    /// Interaction logic for StoryTree.xaml
    /// </summary>
    public partial class StoryTree : UserControl
    {
        public StoryTree()
        {
            InitializeComponent();
        }

        ExceptionHandling exHand = new ExceptionHandling();
        //COMPOSITE LIST OF CHAPTERS THAT NEED TO BE TREEVIEWITEMS
        public List<Chapter> chapters = new List<Chapter>();
        //COMPOSITE LIST OF FILE PATHS
        public List<string> filesFromDir = new List<string>();

        public EventHandler selectionChanged;


        public void BuildDir(string advenTitle)
        {
            try
            {
                if (Directory.Exists(@AppGlobals.creationGameDir + "/" + advenTitle))
                {
                    var dir = Directory.GetFiles(@AppGlobals.creationGameDir + "/" + advenTitle);
                    foreach (var path in dir)
                    {
                        filesFromDir.Add(path);
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryTree-BuildDir");
            }
        }

        public void AddNewToBuiltDir(string newChapterPath)
        {
            try
            {
                filesFromDir.Add(newChapterPath);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryTree-AddNewToBuiltDir");
            }
        }


        public void ReadFolderTree(string adventureTitle)
        {
            foreach (var path in filesFromDir)
            {
                if (path.ToString().Contains("-meta.xaml"))
                {
                    string chapTitle = "";

                    string theFileNameOnly = path.Substring(path.LastIndexOf("\\") + 1);
                    string filename = Regex.Replace(theFileNameOnly, "[^.0-9]", "");
                    char[] trim = { '.' };
                    chapTitle = "CHAPTER: " + filename.TrimEnd(trim);


                    TreeViewItem newChapter = new TreeViewItem();
                    newChapter.Header = chapTitle;
                    newChapter.FontWeight = FontWeights.SemiBold;
                    newChapter.MouseDoubleClick += StoryTreeChapter_MouseDoubleClick;
                    using (FileStream fs = File.OpenRead(@path))
                    {
                        FlowDocument document = (FlowDocument)XamlReader.Load(fs);

                        //BREAK OUT INTO TAGS
                        List<Block> docBlocks = document.Blocks.ToList();
                        foreach (Block indivBlock in docBlocks)
                        {
                            TextRange range = new TextRange(indivBlock.ContentStart, indivBlock.ContentEnd);
                            string[] metaTexts = range.Text.Split('[');
                            foreach (string indivString in metaTexts)
                            {
                                if (indivString.Contains("%OUTCOMEONE%"))
                                {
                                    if (indivString.Split(']').Last() != "")
                                    {
                                        TreeViewItem outcomeOne = new TreeViewItem();
                                        outcomeOne.Header = "CHOICE A - CHAPTER " + indivString.Split(']').Last();
                                        outcomeOne.FontWeight = FontWeights.Light;
                                        outcomeOne.FontStyle = FontStyles.Italic;
                                        newChapter.Items.Add(outcomeOne);
                                    }
                                }
                                if (indivString.Contains("%OUTCOMETWO%"))
                                {
                                    if (indivString.Split(']').Last() != "")
                                    {
                                        TreeViewItem outcomeTwo = new TreeViewItem();
                                        outcomeTwo.Header = "CHOICE B - CHAPTER " + indivString.Split(']').Last();
                                        outcomeTwo.FontWeight = FontWeights.Light;
                                        outcomeTwo.FontStyle = FontStyles.Italic;
                                        newChapter.Items.Add(outcomeTwo);
                                    }
                                }
                                if (indivString.Contains("%OUTCOMETHREE%"))
                                {
                                    if (indivString.Split(']').Last() != "")
                                    {
                                        TreeViewItem outcomeThree = new TreeViewItem();
                                        outcomeThree.Header = "CHOICE C - CHAPTER " + indivString.Split(']').Last();
                                        outcomeThree.FontWeight = FontWeights.Light;
                                        outcomeThree.FontStyle = FontStyles.Italic;
                                        newChapter.Items.Add(outcomeThree);
                                    }
                                }
                            }
                        }
                    }
                    tvStoryTree.Items.Add(newChapter);
                }
            }
        }

        public void StoryTreeChapter_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //CHECK IF NAVIGATION IS THE CHAPTER HEADER, ELSE CHECK WHICH CHAPTER CHOICE TO NAVIGATE TO
                TreeViewItem item = sender as TreeViewItem;
                if (item.IsSelected)
                {
                    string theFileNameOnly = item.Header.ToString().Substring(item.Header.ToString().LastIndexOf("-") + 1);
                    string filename = Regex.Replace(theFileNameOnly, "[^.0-9]", "");
                    char[] trim = { '.' };
                    string chapNum = filename.TrimEnd(trim);
                    this.selectionChanged(chapNum, new EventArgs());
                }
                else
                {
                    foreach (TreeViewItem child in item.Items)
                    {
                        if (child.IsSelected)
                        {
                            string theFileNameOnly = child.Header.ToString().Substring(child.Header.ToString().LastIndexOf("-") + 1);
                            string filename = Regex.Replace(theFileNameOnly, "[^.0-9]", "");
                            char[] trim = { '.' };
                            string chapNum = filename.TrimEnd(trim);
                            this.selectionChanged(chapNum, new EventArgs());

                        }
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-StoryChoice_Click");
            }
        }
    }
}
