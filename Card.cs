using System;
using System.Drawing;

namespace decision_making
{
    internal class Card
    {
        public string text = "";
        public string Description_text = "";
        public string End_description_text = "";

        public string name;
        public string Variable = "";
        public int number;
        public Color color;

        public int card_clone = 0;
        public int source_map = 0;
        public bool modifiedmodified = false;

        public int[] presence = new int[10];
        public int Counter1 = 0;

        public int[] content = new int[10];
        public int[] content_Temporary = new int[10];
        public int Counter2 = 0;

        public int ReturnЕoСard = -1;

        public string CreateTextCard()
        {
            text = number.ToString() + "|" + name + "|" + Variable + "|"
                + Counter1.ToString() + "|" + Counter2.ToString() + "|"
                + card_clone.ToString() + "|" + source_map.ToString() + "|";

            for (int i = 0; i < 10; i++)
            {
                text += presence[i].ToString() + "|";
            }
            for (int i = 0; i < 10; i++)
            {
                text += content[i].ToString() + "|";
            }
            if (modifiedmodified)
            {
                text += "1|";
            }
            else
            {
                text += "0|";
            }
            text += "\n";

            return text;
        }
        public void LoadCard(string Str)
        {
            string[] words = Str.Split(new char[] { '|' });

            number = Int32.Parse(words[0]);
            name = words[1];
            Variable = words[2];
            Counter1 = Int32.Parse(words[3]);
            Counter2 = Int32.Parse(words[4]);
            card_clone = Int32.Parse(words[5]);
            source_map = Int32.Parse(words[6]);

            for (int i = 0; i < 10; i++)
            {
                presence[i] = Int32.Parse(words[7 + i]);
            }

            for (int i = 0; i < 10; i++)
            {
                content[i] = Int32.Parse(words[17 + i]);
            }
            if (words[27] == "0")
            {
                modifiedmodified = false;
            }
            else
            {
                modifiedmodified = true;
            }

        }
    }
}
