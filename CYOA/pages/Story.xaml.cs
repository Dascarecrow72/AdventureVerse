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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Windows.Media.Animation;
using CYOA.utilities;

namespace CYOA
{
    /// <summary>
    /// Interaction logic for StoryWindow.xaml
    /// </summary>
    public partial class Story : Page
    {
        public Story(Adventure currAdventure)
        {
            InitializeComponent();
            Loaded += Story_Loaded;
            LoadStoryInfo(currAdventure);
        }

        public List<FileInfo> adventureStoryFiles = new List<FileInfo>();
        public List<FileInfo> adventureSceneFiles = new List<FileInfo>();
        public List<FileInfo> adventureStoryMetaFiles = new List<FileInfo>();
        public string outcomeOne = null;
        public Paragraph textOne = new Paragraph();
        public string outcomeTwo = null;
        public Paragraph textTwo = new Paragraph();
        public string outcomeThree = null;
        public Paragraph textThree = new Paragraph();
        public Adventure currLocalAdventure = new Adventure();
        ExceptionHandling exHand = new ExceptionHandling();
        public string currentMusic = null;
        public bool noTunes = true;
        public bool gameOver = false;

        void Story_Loaded(object sender, RoutedEventArgs e)
        {
            AppGlobals.inStoryMode = true;
            AppGlobals.player.isMenu = false;            
        }

        private void LoadStoryInfo(Adventure currAdventure)
        {
            currLocalAdventure = currAdventure;
            LoadFlowDocsForStory(currLocalAdventure);
            LoadCurrentFlowDocChapter(currLocalAdventure, currLocalAdventure.CurrentChapter);
            //LoadSceneImage(currLocalAdventure, currLocalAdventure.CurrentChapter);
            //LoadSceneMusic(currLocalAdventure, currentMusic);
            LoadStats(currLocalAdventure);
        }

        private void LoadStats(Adventure currLocalAdventure)
        {
            try
            {
                txtProtagonist.Text = currLocalAdventure.CharacterName ?? "Unnamed";
                txtTitle.Text = currLocalAdventure.CharacterTitle ?? "the Titleless";
                if (currLocalAdventure.ArmorSet != null)
                    txtArmorSet.Text = currLocalAdventure.ArmorSet.ArmorName ?? "Naked";
                else
                    txtArmorSet.Text = "Naked";
                if (currLocalAdventure.MeleeWeapon != null)
                    txtMeleeWeapon.Text = currLocalAdventure.MeleeWeapon.WeaponName ?? "Unarmed";
                else
                    txtMeleeWeapon.Text = "Unarmed";
                if (currLocalAdventure.RangedWeapon != null)
                    txtRangedWeapon.Text = currLocalAdventure.RangedWeapon.WeaponName ?? "Unarmed";
                else
                    txtRangedWeapon.Text = "Unarmed";
                txtHealthStat.Text = currLocalAdventure.Health.ToString() ?? "100";
                txtArmorStat.Text = currLocalAdventure.Armor.ToString() ?? "100";
                txtSpeedStat.Text = currLocalAdventure.Speed.ToString() ?? "100";
                txtMagicStat.Text = currLocalAdventure.Magic.ToString() ?? "0";
                txtLuckStat.Text = currLocalAdventure.Luck.ToString() ?? "0";
                txtDecisionsMade.Text = currLocalAdventure.DecisionsMade.ToString();
                txtStoryLength.Text = currLocalAdventure.StoryLength.ToString();
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-LoadStats");
            }
        }

        private void LoadFlowDocsForStory(Adventure currLocalAdventure)
        {
            try
            {
                DirectoryInfo currentDir = new DirectoryInfo(@currLocalAdventure.folderPath);
                foreach (FileInfo files in currentDir.GetFiles())
                {
                    if (files.Name.Contains(".png") || files.Name.Contains(".jpg") || files.Name.Contains(".gif") || files.Name.Contains(".bmp"))
                        adventureSceneFiles.Add(files);
                    else if (!files.Name.Contains("-meta") && !files.Name.Contains("-adventuremeta"))
                        adventureStoryFiles.Add(files);
                    else
                        adventureStoryMetaFiles.Add(files);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-LoadFlowDocsForStory");
            }
        }

        private void LoadSceneImage(string sceneName)
        {
            try
            {
                if (adventureSceneFiles.Count() > 0)
                {
                    var imgPath = currLocalAdventure.folderPath + "/" + adventureSceneFiles.Where(d => d.Name == sceneName).First();
                    if (imgPath != null)
                    {
                        BitmapImage imgNewSceneImage =
                            new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), imgPath));

                        imgSceneImage.ImageSource = imgNewSceneImage;
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-LoadSceneImage");
            }
        }

        private void LoadSceneMusic(string currSound)
        {
            try
            {
                if (currSound != "" && currSound != null)
                {
                    AppGlobals.player.PlayMusic(currSound, false);
                }
                else
                    AppGlobals.player.PlayMusic(currLocalAdventure.Theme, true);

                noTunes = true;
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-LoadSceneMusic");
            }
        }

        private void LoadCurrentFlowDocChapter(Adventure currLocalAdventure, int currChapter)
        {
            try
            {
                string newChapterStoryPath = string.Empty;
                string newChapterStoryMetaPath = string.Empty;
                currentMusic = null;
                foreach (var file in adventureStoryFiles)
                {
                    string filename = Regex.Replace(file.Name, "[^.0-9]", "");
                    char[] trim = { '.' };
                    if (filename.TrimEnd(trim) == currChapter.ToString())
                    {
                        newChapterStoryPath = file.Name;
                        break;
                    }
                }
                foreach (var file in adventureStoryMetaFiles)
                {
                    string filename = Regex.Replace(file.Name, "[^.0-9]", "");
                    char[] trim = { '.' };
                    if (filename.TrimEnd(trim) == currChapter.ToString())
                    {
                        newChapterStoryMetaPath = file.Name;
                        break;
                    }
                }

                //READ XAML FOR STORY
                using (FileStream fs = File.OpenRead(@currLocalAdventure.folderPath + "/" + newChapterStoryPath))
                {
                    FlowDocument document = (FlowDocument)XamlReader.Load(fs);
                    fdrDocumentReader.Document = document;
                }

                using (FileStream fs = File.OpenRead(@currLocalAdventure.folderPath + "/" + newChapterStoryPath))
                {
                    FlowDocument document = (FlowDocument)XamlReader.Load(fs);
                    UpdateOngoing(currLocalAdventure, document);
                }

                //READ META FOR CHOICE
                try
                {
                    using (FileStream fs = File.OpenRead(@currLocalAdventure.folderPath + "/" + newChapterStoryMetaPath))
                    {
                        FlowDocument document = (FlowDocument)XamlReader.Load(fs);

                        //RPG STATS WORK
                        int armor = 0;
                        int health = 0;
                        int luck = 0;
                        int magic = 0;
                        int speed = 0;

                        //BREAK OUT INTO TAGS TO ASSIGN
                        List<Block> docBlocks = document.Blocks.ToList();
                        foreach (Block indivBlock in docBlocks)
                        {
                            char[] trimmer = { '%', '}' };
                            TextRange range = new TextRange(indivBlock.ContentStart, indivBlock.ContentEnd);
                            string[] metaTexts = range.Text.Split('[');
                            foreach (string indivString in metaTexts)
                            {
                                if (indivString.Contains("%MUSIC%"))
                                {
                                    currentMusic = indivString.Split(']').Last();
                                    LoadSceneMusic(currentMusic);
                                    noTunes = false;
                                }
                                if (indivString.Contains("%SCENE%"))
                                {
                                    var currentScene = "";
                                    currentScene = indivString.Split(']').Last();
                                    LoadSceneImage(currentScene);
                                }
                                if (indivString.Contains("%CHOICE%"))
                                {
                                    txtDirections.Text = indivString.Split(']').Last();
                                }
                                if (indivString.Contains("%ANSWERONE%"))
                                {
                                    var text = indivString.Split(']').Last();
                                    txtChoiceOne.Text = text;

                                    if (textOne.Inlines.Count > 0)
                                        textOne.Inlines.Clear();
                                    textOne.Inlines.Add(text);
                                }
                                if (indivString.Contains("%ANSWERTWO%"))
                                {
                                    var text = indivString.Split(']').Last();
                                    txtChoiceTwo.Text = text;

                                    if (textTwo.Inlines.Count > 0)
                                        textTwo.Inlines.Clear();
                                    textTwo.Inlines.Add(text);
                                }
                                if (indivString.Contains("%ANSWERTHREE%"))
                                {
                                    var text = indivString.Split(']').Last();
                                    txtChoiceThree.Text = text;

                                    if (textThree.Inlines.Count > 0)
                                        textThree.Inlines.Clear();
                                    textThree.Inlines.Add(text);
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
                                    //IF RPGENABLED, STATS BECOME COOL
                                    if (currLocalAdventure.RPGEnabled)
                                    {
                                        var g = indivString.Split(']').Last();
                                        if (g == "")
                                            g = "0";
                                        armor = Convert.ToInt32(g);
                                    }
                                        currLocalAdventure.Armor = currLocalAdventure.Armor + Convert.ToInt32(indivString.Split(']').Last());
                                        txtArmorStat.Text = currLocalAdventure.Armor.ToString();
                                }
                                if (indivString.Contains("%CHHEALTH%"))
                                {
                                    //IF RPGENABLED, STATS BECOME COOL
                                    if (currLocalAdventure.RPGEnabled)
                                    {
                                        var g = indivString.Split(']').Last();
                                        if (g == "")
                                            g = "0";
                                        health = Convert.ToInt32(g);
                                    }
                                    currLocalAdventure.Health = currLocalAdventure.Health + Convert.ToInt32(indivString.Split(']').Last());
                                    txtHealthStat.Text = currLocalAdventure.Health.ToString();
                                    if (currLocalAdventure.Health < 0)
                                    {
                                        gameOver = true;
                                    }
                                }
                                if (indivString.Contains("%CHLUCK%"))
                                {
                                    //IF RPGENABLED, STATS BECOME COOL
                                    if (currLocalAdventure.RPGEnabled)
                                    {
                                        var g = indivString.Split(']').Last();
                                        if (g == "")
                                            g = "0";
                                        luck = Convert.ToInt32(g);
                                    }
                                        currLocalAdventure.Luck = currLocalAdventure.Luck + Convert.ToInt32(indivString.Split(']').Last());
                                        txtLuckStat.Text = currLocalAdventure.Luck.ToString();
                                }
                                if (indivString.Contains("%CHMAGIC%"))
                                {
                                    //IF RPGENABLED, STATS BECOME COOL
                                    if (currLocalAdventure.RPGEnabled)
                                    {
                                        var g = indivString.Split(']').Last();
                                        if (g == "")
                                            g = "0";
                                        magic = Convert.ToInt32(g);
                                    }
                                        currLocalAdventure.Magic = currLocalAdventure.Magic + Convert.ToInt32(indivString.Split(']').Last());
                                        txtMagicStat.Text = currLocalAdventure.Magic.ToString();
                                }
                                if (indivString.Contains("%CHSPEED%"))
                                {
                                    //IF RPGENABLED, STATS BECOME COOL
                                    if (currLocalAdventure.RPGEnabled)
                                    {
                                        var g = indivString.Split(']').Last();
                                        if (g == "")
                                            g = "0";
                                        speed = Convert.ToInt32(g);
                                    }
                                        currLocalAdventure.Speed = currLocalAdventure.Speed + Convert.ToInt32(indivString.Split(']').Last());
                                        txtSpeedStat.Text = currLocalAdventure.Speed.ToString();
                                }

                                if (indivString.Contains("%CHARMORSET%"))
                                {
                                    currLocalAdventure.ArmorSet.ArmorName = indivString.Split(']').Last();
                                    txtArmorSet.Text = currLocalAdventure.ArmorSet.ArmorName;
                                }
                                if (indivString.Contains("%CHMELEEWEAPON%"))
                                {
                                    currLocalAdventure.MeleeWeapon.WeaponName = indivString.Split(']').Last();
                                    txtMeleeWeapon.Text = currLocalAdventure.MeleeWeapon.WeaponName;
                                }
                                if (indivString.Contains("%CHRANGEDWEAPON%"))
                                {
                                    currLocalAdventure.RangedWeapon.WeaponName = indivString.Split(']').Last();
                                    txtRangedWeapon.Text = currLocalAdventure.RangedWeapon.WeaponName;
                                }
                            }
                        }
                        if (currLocalAdventure.RPGEnabled)
                            CalculateStatsPost(armor,  health,  luck,  magic,  speed);
                    }

                    if (gameOver)
                    {
                        txtDirections.Text = "Your adventure has ended.";
                        txtChoiceOne.Text = "";
                        txtChoiceTwo.Text = "";
                        txtChoiceThree.Text = "";
                        brdChoiceOne.IsEnabled = false;
                        brdChoiceTwo.IsEnabled = false;
                        brdChoiceThree.IsEnabled = false;
                        ProcessChoice("finished", 0);
                        AppGlobals.player.StopMusic();
                    }
                }
                catch
                {
                    txtDirections.Text = "Your adventure has ended.";
                    txtChoiceOne.Text = "";
                    txtChoiceTwo.Text = "";
                    txtChoiceThree.Text = "";
                    brdChoiceOne.IsEnabled = false;
                    brdChoiceTwo.IsEnabled = false;
                    brdChoiceThree.IsEnabled = false;
                    ProcessChoice("finished", 0);
                    AppGlobals.player.StopMusic();
                }

                if (txtChoiceOne.Text == "")
                    brdChoiceOne.IsEnabled = false;
                else
                    brdChoiceOne.IsEnabled = true;
                if (txtChoiceTwo.Text == "")
                    brdChoiceTwo.IsEnabled = false;
                else
                    brdChoiceTwo.IsEnabled = true;
                if (txtChoiceThree.Text == "")
                    brdChoiceThree.IsEnabled = false;
                else
                    brdChoiceThree.IsEnabled = true;
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-LoadCurrentFlowDocChapter");
            }
        }

        private void CalculateStatsPost(int armor, int health, int luck, int magic, int speed)
        {
            try
            {
                //ALL UNNEEDED UNTIL FURTHER FLESHED OUT
                decimal armorPerc = 0;
                decimal luckPerc = 0;
                decimal magicPerc = 0;
                decimal speedPerc = 0;

                if (currLocalAdventure.Armor != armor)
                    armorPerc = (1 - (Convert.ToDecimal(currLocalAdventure.Armor) / Convert.ToDecimal(currLocalAdventure.Armor - armor)));
                if (currLocalAdventure.Luck != luck)
                    luckPerc = (1 - (Convert.ToDecimal(currLocalAdventure.Luck) / Convert.ToDecimal(currLocalAdventure.Luck - luck)));
                if (currLocalAdventure.Magic != magic)
                    magicPerc = (1 - (Convert.ToDecimal(currLocalAdventure.Magic) / Convert.ToDecimal(currLocalAdventure.Magic - magic)));
                if (currLocalAdventure.Speed != speed)
                    speedPerc = (1 - (Convert.ToDecimal(currLocalAdventure.Speed) / Convert.ToDecimal(currLocalAdventure.Speed - speed)));

                decimal trueHealth = currLocalAdventure.Health + health;
                var newHealth = 0;

                if (trueHealth < currLocalAdventure.Health)
                {
                    double modifiedDmg = 0;

                    //LUCK = % CHANCE TO AVOID ATTACK
                    if (luckPerc != null && luckPerc != 0)
                    {
                        var luckVars = 100 - Convert.ToInt32(100 * luckPerc);
                        Random dumbLuck = new Random();
                        Random dumbLuckPool = new Random();
                        int dumbLuckPoolInt = dumbLuckPool.Next(0, luckVars);
                        int dumbLuckInt = dumbLuck.Next(0, luckVars);

                        if (dumbLuckInt == dumbLuckPoolInt)
                        {
                            currLocalAdventure.Health = Convert.ToInt32(trueHealth);
                            return;
                        }
                    }

                    //SPEED = % CHANCE TO AVOID ATTACK
                    if (speedPerc != null && speedPerc != 0)
                    {
                        var speedVars = 100 - Convert.ToInt32(100 * speedPerc);
                        Random dumbSpeed = new Random();
                        Random dumbSpeedPool = new Random();
                        int dumbSpeedPoolInt = dumbSpeedPool.Next(0, speedVars);
                        int dumbSpeedInt = dumbSpeed.Next(0, speedVars);

                        if (dumbSpeedInt == dumbSpeedPoolInt)
                        {
                            currLocalAdventure.Health = Convert.ToInt32(trueHealth);
                            return;
                        }
                    }

                    //MAGIC = % CHANCE TO TURN ATTACK INTO HEALTH
                    if (magicPerc != null && magicPerc != 0)
                    {
                        var magicVars = 100 - Convert.ToInt32(100 * magicPerc);
                        Random dumbMagic = new Random();
                        Random dumbMagicPool = new Random();
                        int dumbMagicPoolInt = dumbMagicPool.Next(0, magicVars);
                        int dumbMagicInt = dumbMagic.Next(0, magicVars);

                        if (dumbMagicInt == dumbMagicPoolInt)
                        {
                            newHealth = Convert.ToInt32(trueHealth) - health;
                        }
                    }

                    //ARMOR = % DAMAGE MITIGATION
                    if (armorPerc != null && armorPerc != 0)
                    {
                        if (newHealth == 0)
                        {
                            modifiedDmg = modifiedDmg + (health / Math.Pow(10, (double)armorPerc));
                            trueHealth = trueHealth + Convert.ToDecimal(modifiedDmg);
                        }
                    }

                    if (newHealth > 0)
                    {
                        currLocalAdventure.Health = newHealth;
                        txtHealthStat.Text = Convert.ToInt32(newHealth).ToString();
                    }
                    else
                    {
                        currLocalAdventure.Health = Convert.ToInt32(trueHealth);
                        txtHealthStat.Text = currLocalAdventure.Health.ToString();
                    }
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-CalculateStatsPost");
            }
        }

        private void UpdateOngoing(Adventure currLocalAdventure, FlowDocument doc)
        {
            try
            {
                List<Block> docBlocks = doc.Blocks.ToList();
                foreach (Block indivBlock in docBlocks)
                {
                    TextRange range = new TextRange(indivBlock.ContentStart, indivBlock.ContentEnd);
                    Paragraph gg = new Paragraph();
                    gg.Inlines.Add(range.Text);
                    if (currLocalAdventure.ongoingStory.Blocks.Count == 0)
                        currLocalAdventure.ongoingStory.Blocks.Add(indivBlock);
                    else
                        currLocalAdventure.ongoingStory.Blocks.InsertAfter(currLocalAdventure.ongoingStory.Blocks.Last(), gg);
                    //currLocalAdventure.ongoingStory.Blocks.Add(indivBlock);

                    currLocalAdventure.StoryLength = currLocalAdventure.StoryLength + CountWords(range.Text);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-UpdateOngoing");
            }
        }

        private void StoryChoice_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                switch (((Border)sender).Name.ToString().ToLower())
                {
                    case "brdchoiceone":
                        ProcessChoice(outcomeOne, 1);
                        break;
                    case "brdchoicetwo":
                        ProcessChoice(outcomeTwo, 2);
                        break;
                    case "brdchoicethree":
                        ProcessChoice(outcomeThree, 3);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-StoryChoice_Click");
            }
        }

        private void ProcessChoice(string sender, int choice)
        {
            try
            {
                if (sender != "finished")
                {
                    currLocalAdventure.CurrentChapter = Convert.ToInt32(sender);
                    Paragraph newBlock = new Paragraph();
                    switch (choice)
                    {
                        case 1:
                            TextRange range1 = new TextRange(textOne.ContentStart, textOne.ContentEnd);
                            newBlock.Inlines.Add(range1.Text);
                            currLocalAdventure.ongoingStory.Blocks.InsertAfter(currLocalAdventure.ongoingStory.Blocks.Last(), newBlock);
                            break;
                        case 2:
                            TextRange range2 = new TextRange(textOne.ContentStart, textOne.ContentEnd);
                            newBlock.Inlines.Add(range2.Text);
                            currLocalAdventure.ongoingStory.Blocks.InsertAfter(currLocalAdventure.ongoingStory.Blocks.Last(), newBlock);
                            break;
                        case 3:
                            TextRange range3 = new TextRange(textOne.ContentStart, textOne.ContentEnd);
                            newBlock.Inlines.Add(range3.Text);
                            currLocalAdventure.ongoingStory.Blocks.InsertAfter(currLocalAdventure.ongoingStory.Blocks.Last(), newBlock);
                            break;
                        default:
                            break;
                    }
                    noTunes = true;
                    SaveAdventure(currLocalAdventure);
                    LoadCurrentFlowDocChapter(currLocalAdventure, Convert.ToInt32(sender));
                    //LoadSceneImage(sender);
                    if (noTunes == true)
                        LoadSceneMusic(currentMusic);
                }
                else
                {
                    using (FileStream fs = new FileStream(@AppGlobals.finishedStoryDir + "/" + AppGlobals.currGlobalAdventure.Title + "_" + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Year + ".rtf", FileMode.Create))
                    {
                        TextRange textRange = new TextRange(currLocalAdventure.ongoingStory.ContentStart, currLocalAdventure.ongoingStory.ContentEnd);
                        textRange.Save(fs, DataFormats.Rtf);
                    }

                    //DELETE THE IN PROGRESS FILES AS THE STORY IS FINISHED
                    DirectoryInfo currentDir = new DirectoryInfo(@AppGlobals.saveGameDir);
                    var saveFile = currentDir.GetFiles().Where(d => d.Name == AppGlobals.currGlobalAdventure.Title + ".advo");
                    if (saveFile.Count() > 0)
                    {
                        File.Delete(saveFile.First().FullName);
                    }
                    currentDir = new DirectoryInfo(@AppGlobals.saveGameMetaDir);
                    var metaFile = currentDir.GetFiles().Where(d => d.Name == AppGlobals.currGlobalAdventure.Title + ".advm");
                    if (metaFile.Count() > 0)
                    {
                        File.Delete(metaFile.First().FullName);
                    }

                    AppGlobals.currGlobalAdventure = new Adventure();
                    MainMenu mainMenu = new MainMenu();
                    this.NavigationService.Navigate(mainMenu);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "Story-ProcessChoice");
            }
            finally
            {
                currLocalAdventure.DecisionsMade++;
                LoadStats(currLocalAdventure);
            }
        }

        public static int CountWords(string input)
        {
            MatchCollection collection = Regex.Matches(input, @"[\S]+");
            return collection.Count;
        }

        public void SaveAdventure(Adventure currLocalAdventure)
        {
            //SAVE ONGOING STORY
            using (FileStream fs = new FileStream(@AppGlobals.saveGameDir + "/" + AppGlobals.currGlobalAdventure.Title + ".xaml", FileMode.Create))
            {
                try
                {
                    CYOA.utilities.XamlWriter.Save(currLocalAdventure.ongoingStory, fs);
                }
                catch (Exception err)
                {
                    exHand.LogException(err, "Story-SaveAdventure");
                }
                finally
                {
                    fs.Close();
                }
                    
            }

            //SAVE CURRENT STORY META FOR LOADING
            using (FileStream fs = new FileStream(@AppGlobals.saveGameMetaDir + "/" + AppGlobals.currGlobalAdventure.Title + ".xaml", FileMode.Create))
            {
                try
                {
                    FlowDocument currSaveMeta = new FlowDocument();
                    Paragraph newBlock = new Paragraph();
                    newBlock.Inlines.Add("[%ARMOR%]" + AppGlobals.currGlobalAdventure.Armor.ToString());
                    newBlock.Inlines.Add("[%ARMORSET%]" + AppGlobals.currGlobalAdventure.ArmorSet.ArmorName);
                    newBlock.Inlines.Add("[%AUTHOR%]" + AppGlobals.currGlobalAdventure.Author);
                    newBlock.Inlines.Add("[%CHARACTERNAME%]" + AppGlobals.currGlobalAdventure.CharacterName);
                    newBlock.Inlines.Add("[%CHARACTERTITLE%]" + AppGlobals.currGlobalAdventure.CharacterTitle);
                    newBlock.Inlines.Add("[%CURRENTCHAPTER%]" + AppGlobals.currGlobalAdventure.CurrentChapter.ToString());
                    newBlock.Inlines.Add("[%DECISIONSMADE%]" + AppGlobals.currGlobalAdventure.DecisionsMade.ToString());
                    newBlock.Inlines.Add("[%FOLDERPATH%]" + AppGlobals.currGlobalAdventure.folderPath);
                    newBlock.Inlines.Add("[%HEALTH%]" + AppGlobals.currGlobalAdventure.Health.ToString());
                    newBlock.Inlines.Add("[%LUCK%]" + AppGlobals.currGlobalAdventure.Luck.ToString());
                    newBlock.Inlines.Add("[%MAGIC%]" + AppGlobals.currGlobalAdventure.Magic.ToString());
                    newBlock.Inlines.Add("[%MELEEWEAPON%]" + AppGlobals.currGlobalAdventure.MeleeWeapon.WeaponName);
                    newBlock.Inlines.Add("[%PUBLISHDATE%]" + AppGlobals.currGlobalAdventure.PublishDate);
                    newBlock.Inlines.Add("[%RANGEDWEAPON%]" + AppGlobals.currGlobalAdventure.RangedWeapon.WeaponName);
                    newBlock.Inlines.Add("[%SPEED%]" + AppGlobals.currGlobalAdventure.Speed.ToString());
                    newBlock.Inlines.Add("[%STORYLENGTH%]" + AppGlobals.currGlobalAdventure.StoryLength.ToString());
                    newBlock.Inlines.Add("[%SUMMARY%]" + AppGlobals.currGlobalAdventure.Summary);
                    newBlock.Inlines.Add("[%TITLE%]" + AppGlobals.currGlobalAdventure.Title);
                    newBlock.Inlines.Add("[%GENRE%]" + AppGlobals.currGlobalAdventure.Genre);
                    newBlock.Inlines.Add("[%THEME%]" + AppGlobals.currGlobalAdventure.Theme);

                    currSaveMeta.Blocks.Add(newBlock);

                    CYOA.utilities.XamlWriter.Save(currSaveMeta, fs);
                }
                catch (Exception err)
                {
                    exHand.LogException(err, "Story-SaveAdventure");
                }
                finally
                {
                    fs.Close();
                }

            }
        }

    }
}
