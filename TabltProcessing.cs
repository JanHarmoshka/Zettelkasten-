using System.Windows.Forms;

namespace decision_making
{
    internal class TabltProcessing
    {
        public DataGridView dataGridView1;
        public void AddNote(Card card)
        {
            dataGridView1.Columns.Add(card.name, card.name);
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = card.name;
            dataGridView1.RowHeadersWidth = 250;
            dataGridView1.AllowUserToAddRows = false;
        }
    }
}
