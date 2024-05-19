using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace decision_making
{
    internal class Helper
    {
        public TextBox[] TextBoxPresence = new TextBox[10];
        public Button[] ButtonPresence = new Button[10];
        public TextBox[] TextBoxContent = new TextBox[10];
        public Button[] ButtonContent = new Button[10];
        public Button[] ButtonContentUp = new Button[10];

        public TextBox textBox_name;
        public TextBox textBox22;

        public Button button1;
        public Button button31;
        public Button button32;
        public Button button33;
        public Button button34;
        public Button button35;
        public Button button38;

        public RichTextBox richTextBox1;
        public RichTextBox richTextBox2;

        public System.Windows.Forms.Label label_number;
        public int number_card_clon = -1;
        public CreatingMap creatingMap;

        public Button buttonNew;
        private bool flag = false;

        public int FactorNumber = 0;

        public Card GoInOrder_Presence(int order, Card card, List<Card> cards)
        {
            int ReturnЕoСard = -1;

            if (order > 0)
            {
                ReturnЕoСard = card.number;
                button31.Visible = true;
                button31.Text = "Вернутся к # " + card.number.ToString();
            }
            card = cards[card.presence[order] - 1];
            Jump(card, cards);
            if (ReturnЕoСard != -1)
            {
                card.ReturnЕoСard = ReturnЕoСard;
            }
            if (order == 0)
            {
                if (card.card_clone > 0)
                {
                    FactorNumber = card.number;
                }
                if (card.Counter1 != 0 && FactorNumber > 0)
                {
                    label_number.Text += " (" + cards[FactorNumber - 1].name + ")";
                }
            }
            return card;
        }

        public Card GoInOrder_Content(int order, Card card, List<Card> cards)
        {
            if (flag || card.content_Temporary[order] - 1 > 0)
            {
                card = cards[card.content_Temporary[order] - 1];
            }
            else
            {
                card = cards[card.content[order] - 1];
            }
            Jump(card, cards);
            if (card.card_clone > 0)
            {
                FactorNumber = card.number;
            }
            if (card.Counter1 != 0 && FactorNumber > 0)
            {
                label_number.Text += " (" + cards[FactorNumber - 1].name + ")";
            }
            return card;
        }

        /// <summary>
        /// Переход между карточками
        /// </summary>
        /// <param name="card"></param>
        /// <param name="cards"></param>
        public void Jump(Card card, List<Card> cards)
        {
            interface_(card, cards);

            if (card.Counter1 > 0)// заполнение факторов влияния
            {
                for (int j = 0; j < card.Counter1; j++)
                {
                    if (card.presence[j] > 0)
                    {
                        Card card_second = cards[card.presence[j] - 1];
                        TextBoxPresence[j].Text = "# " + card.presence[j] + " " + card_second.name;
                        if (card_second.card_clone == 0)
                        {
                            if (j > 0 && card_second.Counter2 > 0)// перенос свойств факторов влияния
                            {
                                for (int i = 0; i < card_second.Counter2; i++)
                                {
                                    if (card.Counter2 < 10)
                                    {
                                        bool content = true;
                                        for (int k = 0; k < card.Counter2; k++)
                                        {
                                            if (card.content[k] == card_second.content[i])// Поиск дублей
                                            {
                                                content = false;
                                            }
                                        }
                                        if (content)
                                        {
                                            card.content[card.Counter2] = card_second.content[i];
                                            card.Counter2++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        TextBoxPresence[j].Text = "-# " + card.presence[j] * -1 + " " + cards[card.presence[j] * -1 - 1].name;
                        TextBoxPresence[j].BackColor = Color.Maroon;
                    }
                    ButtonPresence[j].Enabled = true;
                }
            }

            if (card.Counter2 > 0)// заполнение списка свойств
            {
                if (card.card_clone == 0 & FactorNumber == -1)//Если карточка структуры
                {
                    for (int j = 0; j < card.Counter2; j++)
                    {
                        TextBoxContent[j].Text = "# " + card.content[j] + " " + cards[card.content[j] - 1].name;
                        ButtonContent[j].Enabled = true;
                        if (j != 0) { ButtonContentUp[j].Enabled = true; }
                    }
                }
                else //Если карточка проекта
                {
                    List<bool> Counter = new List<bool>();
                    List<int> contents = new List<int>();
                    for (int j = 0; j < card.Counter2; j++) //Перебираю все свойства на целевой карте
                    {
                        if (cards[card.content[j] - 1].card_clone == 0) // Если карта следующая за целевой структурная
                        {
                            bool content_bool = true;
                            int[] content = cards[card.content[j] - 1].content;
                            for (int i = 0; i < content.Length; i++)// Поиск значения на карте следующая за целевой
                            {
                                bool presence_bool = false;
                                if (content[i] != 0 && cards[content[i] - 1].Counter1 > 0)
                                {
                                    for (int k = 0; k < 10; k++)
                                    {
                                        if (cards[content[i] - 1].presence[k] == FactorNumber) { presence_bool = true; }
                                    }
                                }
                                if (content[i] != 0 && presence_bool)//Если значения найдены
                                {
                                    contents.Add(content[i]);
                                    Counter.Add(true);
                                    content_bool = false;
                                }
                            }
                            for (int k = 0; k < 10; k++)// Поиск фактора блокирующего признак
                            {
                                if (cards[card.content[j] - 1].presence[k] == -FactorNumber) { content_bool = false; }
                            }
                            if (content_bool)
                            {
                                contents.Add(card.content[j]);
                                Counter.Add(false);
                            }
                        }
                        else // Если карта следующая за целевой карта проекта
                        {
                            contents.Add(card.content[j]);
                            Counter.Add(false);
                        }
                    }
                    for (int i = 0; i < contents.Count; i++)// раскрашиваю
                    {
                        if (i < 10)
                        {
                            TextBoxContent[i].Text = "# " + contents[i] + " " + cards[contents[i] - 1].name;
                            if (cards[contents[i] - 1].card_clone > 0)
                            {
                                TextBoxContent[i].BackColor = Color.LightSteelBlue;
                                cards[contents[i] - 1].color = Color.LightSteelBlue;
                            }
                            else
                            {
                                if (!Counter[i] && cards[contents[i] - 1].Counter2 == 0 && FactorNumber > 0)
                                {
                                    TextBoxContent[i].BackColor = Color.MistyRose;
                                    cards[contents[i] - 1].color = Color.MistyRose;
                                }
                            }
                            ButtonContent[i].Enabled = true;
                            card.content_Temporary[i] = contents[i];
                            if (i != 0)
                            {
                                ButtonContentUp[i].Enabled = true;
                                ButtonContentUp[i].Text = "Х";
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Загрузка исходного интерфейса карточки
        /// </summary>
        /// <param name="card"></param>
        /// <param name="cards"></param>
        private void interface_(Card card, List<Card> cards)
        {
            if (number_card_clon == -1 || number_card_clon == card.number)
            {
                button33.Visible = false;
                button35.Visible = false;
                button38.Visible = false;
            }
            else
            {
                if (card.Counter1 < 10)
                {
                    if (cards[number_card_clon - 1].card_clone != 0)
                    {
                        button38.Visible = true;
                    }
                    button33.Visible = true;
                }
                if (card.Counter2 < 10)
                {
                    button35.Visible = true;
                    button33.Visible = true;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                ButtonContent[i].Enabled = false;
                ButtonPresence[i].Enabled = false;
                TextBoxContent[i].Text = "";
                TextBoxContent[i].BackColor = Color.WhiteSmoke;
                TextBoxPresence[i].Text = "";
                TextBoxPresence[i].BackColor = Color.WhiteSmoke;
                if (i != 0)
                {
                    ButtonContentUp[i].Enabled = false;
                    ButtonContentUp[i].Text = "j";
                }
            }
            label_number.Text = "# " + card.number.ToString() + " " + card.name;
            textBox_name.Text = card.name;

            button33.Text = "Вставить структуру";
            if (number_card_clon > 0 && cards[number_card_clon - 1].card_clone > 0)
            {
                button33.Text = "Привязать к фактору";
            }
            buttonNew.Text = "Добавить уникальный признак";
            flag = true;
            if (card.card_clone == 0)
            {
                buttonNew.Text = "Добавить новое свойство";
                flag = false;
            }

            richTextBox1.Text = card.Description_text;
            richTextBox2.Text = card.End_description_text;
            textBox22.Text = card.Variable;
            textBox_name.Enabled = false;
            button32.Text = "Изменить";
        }
    }
}
