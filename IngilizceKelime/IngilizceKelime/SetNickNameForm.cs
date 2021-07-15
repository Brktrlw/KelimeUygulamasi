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

    }
}
