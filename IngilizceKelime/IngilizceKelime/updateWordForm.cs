using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace IngilizceKelime
{
    public partial class updateWordForm : Form
    {
        static Form1 form = Application.OpenForms.OfType<Form1>().FirstOrDefault();
        public static string veritabaniyolu = "Data source=Database.db";
        public static SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);
        public updateWordForm()
        {
            InitializeComponent();
        }
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Hide();
        }

        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_updateWord_Click(object sender, EventArgs e)
        {

            if (txt_ingKelime2.Text == "" | txt_trKelime2.Text == "")
            {
                Form1.errorMessageBox.ErrorMessage("Güncellemek için boş bırakılan yerleri doldurmalısınız.");
            }
            else if (txt_ingKelime2.Text.Trim().Replace(" ", "").All(c => Char.IsLetter(c)) == false || txt_trKelime2.Text.Trim().Replace(" ", "").All(c => Char.IsLetter(c)) == false)
            {
                Form1.errorMessageBox.ErrorMessage("Özel karakter kullanmayınız.Sadece harfleri kullanınız.");
            }
            else
            {
                DatabaseManager.updateWord(txt_ingKelime2.Text, txt_trKelime2.Text, llb_wordOfID.Text, cbox_gruops2.Text,txt_eng_cumle.Text);
                Form1.successMessageBox.SuccessMessage("Güncelleme işlemi başarıyla gerçekleştirilmiştir.");
                //Form1.UpdateAll();
                UpdateManager.updateTableAfterProcses(form.cbox_liste0.Text, form.btn_bilgileriGetir, form.cbox_liste0);
                
            }

        }

        private void btn_addGroup2_Click(object sender, EventArgs e)
        {
            AddGroupForm addGroupForm = new AddGroupForm();
            addGroupForm.Show();
        }

        private void updateWordForm_Load(object sender, EventArgs e)
        {
            cbox_gruops2.Items.Clear();

            string sqlCode = "SELECT Türİsmi FROM Türler";

            baglanti.Open();
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string turAdi = Convert.ToString(dr[0]);
                cbox_gruops2.Items.Add(turAdi);
            }
            baglanti.Close();
        }
    }

}
