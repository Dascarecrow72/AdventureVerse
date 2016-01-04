using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CYOA.cs
{
    [Serializable]
    public class Adventure
    {
        public string Title = "NULL";
        public string Author = "NULL";
        public string PublishDate = "NULL";
        public string Summary = "NULL";
        public string Genre = "NULL";
        public string Theme = "NULL";

        public string CharacterName = "NULL";
        public string CharacterTitle = "NULL";
        public Weapon MeleeWeapon = new Weapon();
        public Weapon RangedWeapon = new Weapon();
        public Armor ArmorSet = new Armor();

        public int Health = 0;
        public int Armor = 0;
        public int Speed = 0;
        public int Magic = 0;
        public int Luck = 0;

        public int DecisionsMade = 0;
        public int StoryLength = 0;

        public string folderPath = "NULL";
        public int CurrentChapter = 1;
        public FlowDocument ongoingStory = new FlowDocument();

        public bool RPGEnabled = true;
        public bool CanPlayerEdit = true;
    }
}
