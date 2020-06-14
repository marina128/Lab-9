using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab9
{
    public partial class Form1 : Form
    {

        bool ErrFlag = false;
        List<uint> Simples;
        public Form1()
        {
            InitializeComponent();
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                interval x = new interval(UInt32.Parse(LeftIntBox.Text), UInt32.Parse(RightIntBox.Text));

                PrimaryInInterval arg;
                arg.Int = x;
                arg.ThreadNumber = byte.Parse(StreamBox.Text);

                backgroundWorker1.RunWorkerAsync(arg);

                buttonOk.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ErrFlag = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            backgroundWorker1.CancelAsync();
            buttonOk.Enabled = true;
        }

       
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {     
            PrimaryInInterval Arg = (PrimaryInInterval)e.Argument;
            SimpleNum z = new SimpleNum();
            e.Result = new List<uint>(z.SimpleArrThread(Arg.Int, Arg.ThreadNumber));          
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            Simples = (List<uint>) e.Result;
            if (ErrFlag)
            {
                dataGridView1.Rows.Clear();
                buttonOk.Enabled = true;
                return;
            }
            foreach (uint i in Simples)
            {
                dataGridView1.Rows.Add(i);
            }
            dataGridView1.Rows.Add("Count: " + Simples.Count);
            buttonOk.Enabled = true;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
