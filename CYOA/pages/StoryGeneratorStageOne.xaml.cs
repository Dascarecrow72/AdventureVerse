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
    /// Interaction logic for StoryGeneratorStageOne.xaml
    /// </summary>
    public partial class StoryGeneratorStageOne : Page
    {
        public StoryGeneratorStageOne(Adventure currCreation)
        {
            InitializeComponent();
            Loaded += StoryGeneratorStageOne_Loaded;
            PopulateAdventure(currCreation);
        }

        ExceptionHandling exHand = new ExceptionHandling();
        Adventure newCreation = new Adventure();

        private void PopulateAdventure(Adventure currCreation)
        {
            newCreation = currCreation;

            txtAdventureTitle.Text = newCreation.Title;
            txtArmorSet.Text = newCreation.ArmorSet.ArmorName;
            txtArmorStat.Text = newCreation.Armor.ToString();
            txtAuthor.Text = newCreation.Author;
            txtHealthStat.Text = newCreation.Health.ToString();
            txtLuckStat.Text = newCreation.Luck.ToString();
            txtMagicStat.Text = newCreation.Magic.ToString();
            txtMeleeWeapon.Text = newCreation.MeleeWeapon.WeaponName;
            txtProtagonist.Text = newCreation.CharacterName;
            txtRangedWeapon.Text = newCreation.RangedWeapon.WeaponName;
            txtSpeedStat.Text = newCreation.Speed.ToString();
            txtSummary.Text = newCreation.Summary;
            txtTitle.Text = newCreation.CharacterTitle;
            cmbGenre.SelectedValue = newCreation.Genre;
            cmbTheme.SelectedValue = newCreation.Theme;
        }

        void StoryGeneratorStageOne_Loaded(object sender, RoutedEventArgs e)
        {
            cmbGenre.ItemsSource = GenreClass.Genres;
            cmbTheme.ItemsSource = GenreClass.Themes; 
            imgLRTT.ToolTip = AppGlobals.StoryGeneratorStageOneToolTip;
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

        private void Choice_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                switch (((Border)sender).Name.ToString().ToLower())
                {
                    case "brdchoiceone":
                        ContinueCreation();
                        break;
                    case "brdchoicetwo":
                        SaveData();
                        break;
                    case "brdchoicethree":
                        DiscardCreation();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageOne-Choice_Click");
            }
        }

        private void ContinueCreation()
        {
            try
            {
                if (ValidateData() == true)
                {
                    SaveData();
                    AppGlobals.currGlobalAdventure = newCreation;
                    StoryGeneratorStageTwo storyGenratorStageTwo = new StoryGeneratorStageTwo(AppGlobals.currGlobalAdventure);
                    this.NavigationService.Navigate(storyGenratorStageTwo);
                }
                else
                {
                    MessageBoxResult validationFailed = System.Windows.MessageBox.Show("ERROR: Please confirm that all critical fields are filled in.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageOne-ContinueCreation");
            }
        }

        private bool ValidateData()
        {
            if (txtAdventureTitle.Text != "" && txtAdventureTitle.Text != "NULL")
            {
                if (txtAuthor.Text != "" && txtAuthor.Text != "NULL")
                {
                    if (txtProtagonist.Text != "" && txtProtagonist.Text != "NULL")
                    {
                        if (txtSummary.Text != "" && txtSummary.Text != "NULL")
                        {
                            if (txtTitle.Text != "" && txtTitle.Text != "NULL")
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

        private void SaveData()
        {
            try
            {
                newCreation.Armor = Convert.ToInt32(txtArmorStat.Text);
                newCreation.ArmorSet.ArmorName = txtArmorSet.Text;
                newCreation.Author = txtAuthor.Text;
                newCreation.CanPlayerEdit = (bool)chkPlayerEditable.IsChecked;
                newCreation.CharacterName = txtProtagonist.Text;
                newCreation.CharacterTitle = txtTitle.Text;
                newCreation.Genre = cmbGenre.SelectedValue.ToString();
                newCreation.Health = Convert.ToInt32(txtHealthStat.Text);
                newCreation.Luck = Convert.ToInt32(txtLuckStat.Text);
                newCreation.Magic = Convert.ToInt32(txtMagicStat.Text);
                newCreation.MeleeWeapon.WeaponName = txtMeleeWeapon.Text;
                newCreation.RangedWeapon.WeaponName = txtRangedWeapon.Text;
                newCreation.RPGEnabled = (bool)chkRPGEnabled.IsChecked;
                newCreation.Speed = Convert.ToInt32(txtSpeedStat.Text);
                newCreation.Summary = txtSummary.Text;
                newCreation.Theme = cmbTheme.SelectedValue.ToString();
                newCreation.Title = txtAdventureTitle.Text;

                if (!Directory.Exists(@AppGlobals.creationGameDir + "/" + newCreation.Title))
                {
                    DirectoryInfo di = Directory.CreateDirectory(@AppGlobals.creationGameDir + "/" + newCreation.Title);
                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }

                FlowDocument currSaveMeta = new FlowDocument();
                Paragraph newBlock = new Paragraph();

                newBlock.Inlines.Add("[%ARMOR%]" + newCreation.Armor.ToString());
                newBlock.Inlines.Add("[%ARMORSET%]" + newCreation.ArmorSet.ArmorName);
                newBlock.Inlines.Add("[%AUTHOR%]" + newCreation.Author);
                newBlock.Inlines.Add("[%CANPLAYEREDIT%]" + newCreation.CanPlayerEdit);
                newBlock.Inlines.Add("[%CHARACTERNAME%]" + newCreation.CharacterName);
                newBlock.Inlines.Add("[%CHARACTERTITLE%]" + newCreation.CharacterTitle);
                newBlock.Inlines.Add("[%GENRE%]" + newCreation.Genre);
                newBlock.Inlines.Add("[%HEALTH%]" + newCreation.Health.ToString());
                newBlock.Inlines.Add("[%LUCK%]" + newCreation.Luck.ToString());
                newBlock.Inlines.Add("[%MAGIC%]" + newCreation.Magic.ToString());
                newBlock.Inlines.Add("[%MELEEWEAPON%]" + newCreation.MeleeWeapon.WeaponName);
                newBlock.Inlines.Add("[%RANGEDWEAPON%]" + newCreation.RangedWeapon.WeaponName);
                newBlock.Inlines.Add("[%RPGENABLED%]" + newCreation.RPGEnabled);
                newBlock.Inlines.Add("[%SPEED%]" + newCreation.Speed.ToString());
                newBlock.Inlines.Add("[%SUMMARY%]" + newCreation.Summary);
                newBlock.Inlines.Add("[%PUBLISHDATE%]" + DateTime.Now.ToShortDateString());
                newBlock.Inlines.Add("[%THEME%]" + newCreation.Theme);
                newBlock.Inlines.Add("[%TITLE%]" + newCreation.Title);

                currSaveMeta.Blocks.Add(newBlock);

                using (FileStream fs = File.Open(@AppGlobals.creationGameDir + "/" + newCreation.Title + "/" + newCreation.Title + "-creationmeta" + ".xaml", FileMode.Create))
                {
                    CYOA.utilities.XamlWriter.Save(currSaveMeta, fs);
                }
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageOne-SaveData");
            }
        }

        private void DiscardCreation()
        {
            try
            {
                AppGlobals.inCreationMode = false;
                newCreation = null;
                MainMenu mainMenu = new MainMenu();
                this.NavigationService.Navigate(mainMenu);
            }
            catch (Exception err)
            {
                exHand.LogException(err, "StoryGeneratorStageOne-DiscardCreation");
            }
        }
    }
}
