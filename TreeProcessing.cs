using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace decision_making
{
    internal class TreeProcessing
    {
        public TreeView treeView1;
        public TreeView treeView2;
        public Helper helper;
        public System.Windows.Forms.Label label_number;

        public Card Choice(int Tree, Card card, List<Card> cards)
        {
            TreeNode node = treeView1.SelectedNode; ;
            if (Tree == 2)
            {
                node = treeView2.SelectedNode; helper.FactorNumber = -1;
            }
            if (node.Name != "_")
            {
                card = cards[Int32.Parse(node.Name) - 1];
               
                node.Text = "#" + card.number.ToString() + " " + card.name;
                if (Tree == 1)
                {
                    if (card.card_clone > 0)
                    {
                        helper.FactorNumber = card.number;
                    }
                    if (card.Counter1 != 0 && helper.FactorNumber != -1)
                    {
                        label_number.Text += " (" + cards[helper.FactorNumber - 1].name + ")";
                        node.Text += " (" + cards[helper.FactorNumber - 1].name + ")";
                        node.ToolTipText = helper.FactorNumber.ToString();
                    }
                    else
                    {
                        helper.FactorNumber = -1;
                    }
                }
                helper.Jump(card, cards);

                if (node.Nodes.Count == 0)
                {
                    node.Nodes.Add("Факторы");
                    node.Nodes[0].Name = "_";
                    node.Nodes.Add("Свойства");
                    node.Nodes[1].Name = "_";
                }
                if (card.Counter1 > 1)
                {
                    node.Nodes[0].Nodes.Clear();
                    for (int i = 1; i < card.Counter1; i++)
                    {
                        if (card.presence[i] < 0)
                        {
                            node.Nodes[0].Nodes.Add("-#" + card.presence[i].ToString() + " " + cards[card.presence[i] * -1 - 1].name);
                            node.Nodes[0].Nodes[i - 1].Name = card.presence[i].ToString();
                        }
                        else
                        {
                            node.Nodes[0].Nodes.Add("#" + card.presence[i].ToString() + " " + cards[card.presence[i] - 1].name);
                            node.Nodes[0].Nodes[i - 1].Name = card.presence[i].ToString();
                        }
                    }
                }
                if (card.Counter2 > 0)
                {
                    node.Nodes[1].Nodes.Clear();
                    for (int i = 0; i < card.Counter2; i++)
                    {
                        node.Nodes[1].Nodes.Add("#" + card.content[i].ToString() + " " + cards[card.content[i] - 1].name);
                        node.Nodes[1].Nodes[i].Name = card.content[i].ToString();
                        if (Tree == 1) { node.Nodes[1].Nodes[i].BackColor = cards[card.content[i] - 1].color; }
                        if (card.Counter1 != 0 && helper.FactorNumber != -1)
                        {
                            node.Nodes[1].Nodes[i].Text += " (" + cards[helper.FactorNumber - 1].name + ")";
                            if (Tree == 1) { node.Nodes[1].Nodes[i].ToolTipText = helper.FactorNumber.ToString(); }
                        }
                    }
                }
                node.Expand();
            }
            return card;
        }
        public Card MouseClick(int Tree, Card card, List<Card> cards)
        {
            TreeNode node = treeView1.SelectedNode;
            if (Tree == 2) { node = treeView2.SelectedNode; }

            if (node != null && node.Name != "_")
            {
                helper.FactorNumber = -1;
                if (node.ToolTipText.Length > 0 && Tree == 1)
                {
                    helper.FactorNumber = Int32.Parse(node.ToolTipText);
                }
                card = cards[Int32.Parse(node.Name) - 1];
                helper.Jump(card, cards);

                if (card.Counter1 != 0 && helper.FactorNumber > 0 && Tree == 1)//
                {
                    label_number.Text += " (" + cards[helper.FactorNumber - 1].name + ")";                    
                }
            }
            return card;
        }
    }
}
