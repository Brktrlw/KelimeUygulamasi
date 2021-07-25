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
    public partial class AddGroupForm : Form
    {
        static Form1 form = Application.OpenForms.OfType<Form1>().FirstOrDefault();

        public AddGroupForm()
        {
            InitializeComponent();
        }
        ErrorMessageBox errorMessageBox = new ErrorMessageBox();
        SuccessMessageBox successMessageBox = new SuccessMessageBox();
        private void btn_addGroup_Click(object sender, EventArgs e)
        {
            if (txt_nameOfGroup.Text.Trim() == "")
            {
                errorMessageBox.ErrorMessage("Lütfen kelime grubu ismini giriniz.");
            }
            else if (txt_nameOfGroup.Text.Trim().Replace(" ","").All(c => Char.IsLetter(c)) == false)
            {
                errorMessageBox.ErrorMessage("Özel karakter kullanmayınız.Sadece harfleri kullanınız.");
            }
            else 
            {
                string V_Tur = DatabaseManager.checkTurName(txt_nameOfGroup.Text.Trim().ToLower());
                if (V_Tur == txt_nameOfGroup.Text.ToLower().Trim())
                {
                    errorMessageBox.ErrorMessage("Aynı isimde bir başka grup zaten bulunmaktadır.");
                }
                else { 
                DatabaseManager.addGroup(txt_nameOfGroup.Text.ToLower().Trim());
                successMessageBox.SuccessMessage("Grup başarıyla eklenmiştir.");
                form.UpdateAll();   
                }
            }
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Hide();
        }

        private void txt_nameOfGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
    }
}
