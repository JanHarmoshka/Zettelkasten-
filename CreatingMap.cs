using System.Collections.Generic;
using System.Windows.Forms;

namespace decision_making
{
    internal class CreatingMap
    {
        public int N;
        public Helper helper;
        public string Checklist = "";
        public TabltProcessing tabltProcessing;

        public Card NewMap(Card card, List<Card> cards, string name_card, int new_clone)
        {
            if (name_card.Length > 0)
            {
                int pos = Checklist.IndexOf(name_card + "|");
                if (pos == -1)
                {
                    CreatingNewMap(name_card, card, cards, new_clone);
                }
                else
                {
                    int number = 0;
                    for (int i = 0; i < cards.Count; i++)
                    {
                        if (cards[i].name == name_card) { number = cards[i].number; break; }
                    }
                    string text = "данное имя имеет карточка #" + number.ToString() + ". Переименуйте карточку после создания";
                    MessageBox.Show(text);
                    name_card = "ヽ(￣～￣　)ノ";
                    CreatingNewMap(name_card, card, cards, 0);
                }
            }
            return card;
        }

        public void InsertPresence(Card card, List<Card> cards, int number_card_clon)
        {
            bool Check = true;
            for (int i = 0; i < 10; i++)
            {
                if (card.content[i] == number_card_clon || card.presence[i] == number_card_clon) { Check = false; }
            }
            if (Check)
            {
                card.Counter1++;
                card.presence[card.Counter1 - 1] = number_card_clon;
                helper.TextBoxPresence[card.Counter1 - 1].Text = "# " + card.presence[card.Counter1 - 1] + " " + cards[number_card_clon - 1].name;
                helper.ButtonPresence[card.Counter1 - 1].Enabled = true;
                helper.Jump(card, cards);
            }
            else
            {
                MessageBox.Show("Связь уже существует");
            }
        }

        public void InsertContent(Card card, List<Card> cards, int number_card_clon)
        {
            bool Check = true;
            for (int i = 0; i < 10; i++)
            {
                if (card.content[i] == number_card_clon || card.presence[i] == number_card_clon) { Check = false; }
            }
            if (Check)
            {
                card.Counter2++;
                card.content[card.Counter2 - 1] = number_card_clon;
                helper.TextBoxContent[card.Counter2 - 1].Text = "# " + card.content[card.Counter2 - 1] + " " + cards[number_card_clon - 1].name;
                helper.ButtonContent[card.Counter2 - 1].Enabled = true;
                helper.Jump(card, cards);
            }
            else
            {
                MessageBox.Show("Связь уже существует");
            }
        }

        /// <summary>
        /// Данная функция создаёт новую карточку
        /// </summary>
        /// <param name="name_card"></param>
        /// <param name="card"></param>
        /// <param name="cards"></param>
        /// <param name="new_clone"></param>
        public void CreatingNewMap(string name_card, Card card, List<Card> cards, int new_clone)
        {
            if (name_card.Length > 0)
            {
                N++;
                Card new_card = new Card
                {
                    number = N,
                    name = name_card
                };
                Checklist += name_card + "|";

                new_card.card_clone = new_clone;

                new_card.Counter1++;
                new_card.presence[new_card.Counter1 - 1] = card.number;

                card.Counter2++;
                card.content[card.Counter2 - 1] = N;
                card.content_Temporary[card.Counter2 - 1] = N;
                helper.TextBoxContent[card.Counter2 - 1].Text = "# " + N.ToString() + " " + name_card;
                helper.ButtonContent[card.Counter2 - 1].Enabled = true;

                if (card.Counter2 - 1 != 0)
                {
                    helper.ButtonContentUp[card.Counter2 - 1].Enabled = true;
                }
                new_card.CreateTextCard();
                cards.Add(new_card);
                tabltProcessing.AddNote(new_card);
            }
        }
    }
}
