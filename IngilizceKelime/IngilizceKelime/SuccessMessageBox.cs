using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IngilizceKelime
{
    public partial class SuccessMessageBox : Form
    {
        public SuccessMessageBox()
        {
            InitializeComponent();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        
        public void SuccessMessage(string Message) 
        {
            int isTrueOrFalse = DatabaseManager.isTrueNotif();
            if (isTrueOrFalse == 1)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"sound.wav");
                player.Play();
            }
                txt_mesaj.Text = Message;
                this.Show();

        }
        public void SuccessMessageDialog(string Message)
        {
            int isTrueOrFalse = DatabaseManager.isTrueNotif();
            if (isTrueOrFalse == 1)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"sound.wav");
                player.Play();
            }
            txt_mesaj.Text = Message;
            this.ShowDialog();

        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Hide();
        }
    }
}
