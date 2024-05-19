using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace decision_making
{
    public partial class Form1 : Form
    {
        Helper helper = new Helper();
        CreatingMap creatingMap = new CreatingMap();
        TreeProcessing treeProcessing = new TreeProcessing();
        TabltProcessing tabltProcessing = new TabltProcessing();

        List<Card> cards = new List<Card>();
        Card card = new Card();
        bool Structure_object;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, System.EventArgs e)
        {
            treeProcessing.helper = helper;

            helper.TextBoxPresence[0] = textBox11; helper.TextBoxPresence[1] = textBox12;
            helper.TextBoxPresence[2] = textBox13; helper.TextBoxPresence[3] = textBox14;
            helper.TextBoxPresence[4] = textBox15; helper.TextBoxPresence[5] = textBox16;
            helper.TextBoxPresence[6] = textBox17; helper.TextBoxPresence[7] = textBox18;
            helper.TextBoxPresence[8] = textBox19; helper.TextBoxPresence[9] = textBox20;

            helper.ButtonPresence[0] = button1; helper.ButtonPresence[1] = button2;
            helper.ButtonPresence[2] = button3; helper.ButtonPresence[3] = button4;
            helper.ButtonPresence[4] = button5; helper.ButtonPresence[5] = button6;
            helper.ButtonPresence[6] = button7; helper.ButtonPresence[7] = button8;
            helper.ButtonPresence[8] = button9; helper.ButtonPresence[9] = button10;

            helper.TextBoxContent[0] = textBox1; helper.TextBoxContent[1] = textBox2;
            helper.TextBoxContent[2] = textBox3; helper.TextBoxContent[3] = textBox4;
            helper.TextBoxContent[4] = textBox5; helper.TextBoxContent[5] = textBox6;
            helper.TextBoxContent[6] = textBox7; helper.TextBoxContent[7] = textBox8;
            helper.TextBoxContent[8] = textBox9; helper.TextBoxContent[9] = textBox10;

            helper.ButtonContent[0] = button21; helper.ButtonContent[1] = button20;
            helper.ButtonContent[2] = button19; helper.ButtonContent[3] = button18;
            helper.ButtonContent[4] = button17; helper.ButtonContent[5] = button16;
            helper.ButtonContent[6] = button15; helper.ButtonContent[7] = button14;
            helper.ButtonContent[8] = button13; helper.ButtonContent[9] = button12;

            helper.ButtonContentUp[1] = button30;
            helper.ButtonContentUp[2] = button29; helper.ButtonContentUp[3] = button28;
            helper.ButtonContentUp[4] = button27; helper.ButtonContentUp[5] = button26;
            helper.ButtonContentUp[6] = button25; helper.ButtonContentUp[7] = button24;
            helper.ButtonContentUp[8] = button23; helper.ButtonContentUp[9] = button22;

            helper.textBox_name = textBox_name;
            helper.textBox22 = textBox22;

            helper.button1 = button1;
            helper.button31 = button31;
            helper.button32 = button32;
            helper.button33 = button33;
            helper.button35 = button35;
            helper.button38 = button38;

            helper.richTextBox1 = richTextBox1;
            helper.richTextBox2 = richTextBox2;

            helper.label_number = label_number;
            helper.buttonNew = buttonNew;

            creatingMap.helper = helper;

            treeProcessing.treeView1 = treeView1;
            treeProcessing.treeView2 = treeView2;
            treeProcessing.label_number = label_number;

            creatingMap.tabltProcessing = tabltProcessing;
            tabltProcessing.dataGridView1 = dataGridView1;

            if (File.Exists(@"card/card.txt") && File.Exists(@"card/End description.txt") && File.Exists(@"card/Description.txt"))
            {
                using (StreamReader reader = new StreamReader(@"card/card.txt", Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Card new_card = new Card();
                        new_card.LoadCard(line);
                        creatingMap.Checklist += new_card.name + "|";
                        cards.Add(new_card);
                        tabltProcessing.AddNote(new_card);
                    }
                }

                int Capt = 0;
                using (StreamReader reader = new StreamReader(@"card/End description.txt", Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length > 2)
                        {
                            if (line[0].ToString() == "[")
                            {
                                cards[Capt].End_description_text += line.Remove(0, 1) + "\n";
                            }
                            else
                            {
                                cards[Capt].End_description_text += line;
                            }
                            if (line[line.Length - 1].ToString() == "]")
                            {
                                cards[Capt].End_description_text = cards[Capt].End_description_text.Remove(cards[Capt].End_description_text.Length - 2);
                                Capt++;
                            }
                        }
                        else
                        {
                            Capt++;
                        }
                    }
                }

                Capt = 0;
                using (StreamReader reader = new StreamReader(@"card/Description.txt", Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Length > 2)
                        {
                            if (line[0].ToString() == "[")
                            {
                                cards[Capt].Description_text += line.Remove(0, 1) + "\n";
                            }
                            else
                            {
                                cards[Capt].Description_text += line;
                            }
                            if (line[line.Length - 1].ToString() == "]")
                            {
                                cards[Capt].Description_text = cards[Capt].Description_text.Remove(cards[Capt].Description_text.Length - 2);
                                Capt++;
                            }
                        }
                        else
                        {
                            Capt++;
                        }
                    }
                }
            }

            if (cards.Count > 0)
            {
                card = cards[0];
                using (StreamReader reader = new StreamReader(@"card/Projects.txt", Encoding.UTF8))
                {
                    string line;
                    int card_number;
                    while ((line = reader.ReadLine()) != null)
                    {
                        card_number = Int32.Parse(line);                        
                        if (cards[card_number - 1].card_clone == 0)
                        {
                            treeView2.Nodes.Add("#" + line + " " + cards[card_number - 1].name);
                            treeView2.Nodes[treeView2.Nodes.Count - 1].Name = line;                            
                        }
                        else
                        {
                            treeView1.Nodes.Add("#" + line + " " + cards[card_number - 1].name);
                            treeView1.Nodes[treeView1.Nodes.Count - 1].Name = line;
                            treeView1.Nodes[treeView1.Nodes.Count - 1].ToolTipText = card_number.ToString();
                        }
                    }
                }
            }
            else
            {
                card.number = 1;
                card.name = "Стартовая";
                creatingMap.Checklist += "Стартовая|";
                card.CreateTextCard();
                cards.Add(card);

                treeView2.Nodes.Add("#1 Стартовая");
                treeView2.Nodes[0].Name = "1";
                tabltProcessing.AddNote(card);
            }

            label_number.Text = "# " + card.number.ToString() + " " + card.name;
            textBox_name.Text = card.name;
            richTextBox1.Text = card.Description_text;
            richTextBox2.Text = card.End_description_text;
            creatingMap.N = cards.Count;
            helper.creatingMap = creatingMap;

            if (File.Exists(@"card/Description.txt"))
            {
                for (int i = 0; i < card.Counter2; i++)
                {
                    if (i != 0)
                    {
                        helper.ButtonContentUp[i].Enabled = true;
                    }
                    helper.ButtonContent[i].Enabled = true;
                    helper.TextBoxContent[i].Text = "# " + card.content[i].ToString() + "." + cards[card.content[i] - 1].card_clone.ToString() + " " + cards[card.content[i] - 1].name;
                }

                for (int i = 0; i < card.Counter1; i++)
                {
                    helper.ButtonPresence[i].Enabled = true;
                    helper.TextBoxPresence[i].Text += "# " + card.presence[i].ToString() + " " + cards[card.presence[i]].name;
                }
            }
        }

        private void buttonNew_Click(object sender, System.EventArgs e)
        {
            if (card.Counter2 < 10)
            {
                buttonNew.Visible = false;
                textBox21.Visible = true;
                textBox21.Text = "";
                textBox21.Focus();
                button34.Visible = true;
            }
        }

        private void button1_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(0, card, cards); }
        private void button2_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(1, card, cards); }
        private void button3_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(2, card, cards); }
        private void button4_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(3, card, cards); }
        private void button5_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(4, card, cards); }
        private void button6_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(5, card, cards); }
        private void button7_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(6, card, cards); }
        private void button8_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(7, card, cards); }
        private void button9_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(8, card, cards); }
        private void button10_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Presence(9, card, cards); }

        private void button21_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(0, card, cards); }
        private void button20_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(1, card, cards); }
        private void button19_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(2, card, cards); }
        private void button18_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(3, card, cards); }
        private void button17_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(4, card, cards); }
        private void button16_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(5, card, cards); }
        private void button15_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(6, card, cards); }
        private void button14_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(7, card, cards); }
        private void button13_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(8, card, cards); }
        private void button12_Click(object sender, System.EventArgs e) { card = helper.GoInOrder_Content(9, card, cards); }

        private void button30_Click(object sender, EventArgs e) { contentUp(1); }
        private void button29_Click(object sender, EventArgs e) { contentUp(2); }
        private void button28_Click(object sender, EventArgs e) { contentUp(3); }
        private void button27_Click(object sender, EventArgs e) { contentUp(4); }
        private void button26_Click(object sender, EventArgs e) { contentUp(5); }
        private void button25_Click(object sender, EventArgs e) { contentUp(6); }
        private void button24_Click(object sender, EventArgs e) { contentUp(7); }
        private void button23_Click(object sender, EventArgs e) { contentUp(8); }
        private void button22_Click(object sender, EventArgs e) { contentUp(9); }

        private void contentUp(int order)
        {
            if (helper.ButtonContentUp[order].Text == "j")
            {
                (helper.TextBoxContent[order - 1].Text, helper.TextBoxContent[order].Text) = (helper.TextBoxContent[order].Text, helper.TextBoxContent[order - 1].Text);
                (card.content[order], card.content[order - 1]) = (card.content[order - 1], card.content[order]);
            }
            if (helper.ButtonContentUp[order].Text == "Х" && helper.FactorNumber > 0)
            {
                Card deletion_card = cards[card.content_Temporary[order] - 1];
                deletion_card.Counter1++;
                deletion_card.presence[deletion_card.Counter1 - 1] = -helper.FactorNumber;
                helper.Jump(card, cards);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Directory.Exists(@"card"))
            {
                Directory.CreateDirectory(@"card");
            }
            string texts = "";
            for (int i = 0; i < cards.Count; i++)
            {
                texts += cards[i].CreateTextCard();
            }
            File.WriteAllText(@"card/card.txt", texts, Encoding.UTF8);
            texts = "";
            for (int i = 0; i < cards.Count; i++)
            {
                texts += "[" + cards[i].Description_text + "]" + "\n";
            }
            File.WriteAllText(@"card/Description.txt", texts, Encoding.UTF8);
            texts = "";
            for (int i = 0; i < cards.Count; i++)
            {
                texts += "[" + cards[i].End_description_text + "]" + "\n";
            }
            File.WriteAllText(@"card/End description.txt", texts, Encoding.UTF8);
            texts = "";
            for (int i = 0; i < treeView2.Nodes.Count; i++)
            {
                texts += treeView2.Nodes[i].Name.ToString() + "\n";
            }
            for (int i = 0; i < treeView1.Nodes.Count; i++)
            {
                texts += treeView1.Nodes[i].Name.ToString() + "\n";
            }
            File.WriteAllText(@"card/Projects.txt", texts, Encoding.UTF8);
        }

        private void richTextBox1_Leave(object sender, System.EventArgs e)
        {
            card.Description_text = richTextBox1.Text;
        }

        private void richTextBox2_Leave(object sender, System.EventArgs e)
        {
            card.End_description_text = richTextBox2.Text;
        }

        private void textBox22_Leave(object sender, System.EventArgs e)
        {
            card.Variable = textBox22.Text;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            textBox_name.Enabled = false;
            button32.Text = "Изменить";
            card = cards[card.ReturnЕoСard - 1];
            label_number.Text = "# " + card.number.ToString();
            textBox_name.Text = card.name;

            for (int i = 0; i < 10; i++)
            {
                helper.ButtonContent[i].Enabled = false;
                helper.ButtonPresence[i].Enabled = false;
                helper.TextBoxContent[i].Text = "";
                helper.TextBoxPresence[i].Text = "";
                if (i != 0)
                {
                    helper.ButtonContentUp[i].Enabled = false;
                }
            }
            helper.Jump(card, cards);

            if (card.ReturnЕoСard == -1)
            {
                button31.Visible = false;
            }
            else
            {
                button31.Text = "Вернутся к # " + card.ReturnЕoСard.ToString();
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (button32.Text == "Изменить")
            {
                textBox_name.Enabled = true;
                textBox_name.Focus();
                button32.Text = "Сохранить";
            }
            else
            {
                textBox_name.Enabled = false;
                button32.Text = "Изменить";
                int pos = creatingMap.Checklist.IndexOf(textBox_name.Text + "|");
                if (pos == -1)
                {
                    card.name = textBox_name.Text;
                    creatingMap.Checklist = creatingMap.Checklist.Replace(card.name + "|", "");
                    creatingMap.Checklist += card.name + "|";
                }
                else
                {
                    string name_card = textBox_name.Text;
                    int number = 0;
                    for (int i = 0; i < cards.Count; i++)
                    {
                        if (cards[i].name == name_card) { number = cards[i].number; break; }
                    }
                    string text = "Данное имя имеет карточка #" + number.ToString() + ". Выбирете другое имя.";
                    MessageBox.Show(text);
                    textBox_name.Text = card.name;
                }
            }

        }

        private void button35_Click(object sender, EventArgs e)
        {
            helper.number_card_clon = -1;
            button33.Visible = false;
            button35.Visible = false;
            button38.Visible = false;
        }

        private void buttonCopyTo_Click(object sender, EventArgs e)
        {
            helper.number_card_clon = card.number;
            groupBox6.Text = "#" + card.number.ToString() + " " + card.name;
            button33.Visible = false;
            button38.Visible = false;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (card.Counter1 < 10)
            {
                creatingMap.InsertPresence(card, cards, helper.number_card_clon);
            }
        }
        private void button38_Click(object sender, EventArgs e)
        {
            if (card.Counter1 < 10)
            {
                creatingMap.InsertContent(card, cards, helper.number_card_clon);
            }
        }

        private void button34_Click(object sender, EventArgs e)//создание свойства
        {
            if (card.Counter2 < 10 && textBox21.Text != "")
            {
                if (buttonNew.Text == "Добавить новое свойство")
                {
                    creatingMap.NewMap(card, cards, textBox21.Text, 0);
                }
                else
                {
                    creatingMap.NewMap(card, cards, textBox21.Text, 1);
                }
                helper.Jump(card, cards);
            }
            buttonNew.Visible = true;
            buttonNew.Focus();
            textBox21.Visible = false;
            button34.Visible = false;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            button36.Visible = false;
            textBox23.Visible = true;
            textBox23.Text = "";
            textBox23.Focus();
            button37.Visible = true;
            card = cards[0];
            helper.Jump(card, cards);
            Structure_object = true;
        }
        private void button11_Click(object sender, EventArgs e)
        {
            button11.Visible = false;
            textBox23.Visible = true;
            textBox23.Text = "";
            textBox23.Focus();
            button37.Visible = true;
            card = cards[0];
            helper.Jump(card, cards);
            Structure_object = false;
        }
        private void button37_Click(object sender, EventArgs e)//создать объект или структуру
        {
            if (card.Counter2 < 10 && textBox23.Text != "")
            {
                if (Structure_object)
                {
                    creatingMap.NewMap(card, cards, textBox23.Text, 1);
                    treeView1.Nodes.Add("#" + card.content[card.Counter2 - 1].ToString() + " " + textBox23.Text);
                    treeView1.Nodes[treeView1.Nodes.Count - 1].Name = card.content[card.Counter2 - 1].ToString();
                    card.content[card.Counter2] = 0;
                    card.Counter2--;
                    helper.Jump(card, cards);
                }
                else
                {
                    creatingMap.NewMap(card, cards, textBox23.Text, 0);
                    treeView2.Nodes.Add("#" + card.content[card.Counter2 - 1].ToString() + " " + textBox23.Text);
                    treeView2.Nodes[treeView2.Nodes.Count - 1].Name = card.content[card.Counter2 - 1].ToString();
                    card.content[card.Counter2] = 0;
                    card.Counter2--;
                    helper.Jump(card, cards);
                }
            }
            button36.Visible = true;
            button11.Visible = true;
            textBox23.Visible = false;
            button37.Visible = false;
        }

        private void textBox21_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button34.PerformClick();
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            card = treeProcessing.Choice(1, card, cards);
        }
        private void treeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            card = treeProcessing.Choice(2, card, cards);
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            card = treeProcessing.MouseClick(1, card, cards);
        }
        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            card = treeProcessing.MouseClick(2, card, cards);
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            card = treeProcessing.MouseClick(1, card, cards);
        }
        private void treeView2_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            card = treeProcessing.MouseClick(2, card, cards);
        }
    }
}