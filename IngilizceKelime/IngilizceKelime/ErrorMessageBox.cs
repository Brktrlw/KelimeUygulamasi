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
    public partial class ErrorMessageBox : Form
    {
        public ErrorMessageBox()
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
        public void ErrorMessage(string Message)
        {
            int isTrueOrFalse = DatabaseManager.isTrueNotif();
            if (isTrueOrFalse == 1) 
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"sound.wav");
                player.Play();
            }
            bunifuLabel1.Text = Message;
            
            this.Show();
        }
        public void ErrorMessageDialog(string Message,string sentence="") 
        {
            int isTrueOrFalse = DatabaseManager.isTrueNotif();
            if (isTrueOrFalse == 1)
            {
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"sound.wav");
                player.Play();
            }
            bunifuLabel1.Text = Message;
            bunifuLabel2.Text = sentence;
            this.ShowDialog();
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Hide();
        }
    }
}
