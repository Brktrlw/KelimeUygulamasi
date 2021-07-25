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
    public partial class SetNickNameForm : Form
    {
        public SetNickNameForm()
        {
            InitializeComponent();
        }
        ErrorMessageBox errorMessageBox = new ErrorMessageBox();
        private void btn_saveNickName_Click(object sender, EventArgs e)
        {
            if (txt_firstUserName.Text.Trim() == "")
            {
                errorMessageBox.ErrorMessage("Önce isminizi girmelisiniz");
            }
            else if (txt_firstUserName.Text.Trim().Replace(" ","").All(c => Char.IsLetter(c)) == false)
            {
                errorMessageBox.ErrorMessage("Özel karakter kullanmayınız.Sadece harfleri kullanınız.");
            }
            else 
            {
            SettingsManager.updateUserName(txt_firstUserName.Text.Trim());

                this.Close();
            }
        }
        public void showWindow() 
        {
            this.ShowDialog();
        }

        private void txt_firstUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
    }
}
