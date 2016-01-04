using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace CYOA.cs
{
    [Serializable]
    public class Chapter
    {
        public int ChapterID = 1;
        public string Scene = "NULL";
        public string Music = "NULL";
        public string Choice = "NULL";
        public string AnswerOne = "NULL";
        public string OutcomeOne = "NULL";
        public string AnswerTwo = "NULL";
        public string OutcomeTwo = "NULL";
        public string AnswerThree = "NULL";
        public string OutcomeThree = "NULL";

        public int Armor = 0;
        public int Health = 0;
        public int Luck = 0;
        public int Magic = 0;
        public int Speed = 0;

        public string ArmorSet = "NULL";
        public string MeleeWeapon = "NULL";
        public string RangedWeapon = "NULL";

        public FlowDocument chapterStory = new FlowDocument();
    }
}
