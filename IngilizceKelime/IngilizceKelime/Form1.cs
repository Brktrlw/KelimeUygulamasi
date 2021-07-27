using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using WECPOFLogic;

namespace IngilizceKelime
{
    public partial class Form1 : Form
    {
        public static SuccessMessageBox successMessageBox = new SuccessMessageBox();
        public static ErrorMessageBox errorMessageBox = new ErrorMessageBox();

        NotifyIcon MyIcon = new NotifyIcon();
        public Form1()
        {
            InitializeComponent();
            TrayMenuContext();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            
            table_words2.AllowUserToAddRows = false;
            table_words1.AllowUserToAddRows = false;
            table_words3.AllowUserToAddRows = false;
            table_words4.AllowUserToAddRows = false;
            cbox_liste1.SelectedIndex = 0;
            cbox_liste2.SelectedIndex = 0;
            try_table.Rows.Add("Hello", "Merhaba");
            bool checkName=SettingsManager.checkNickName();
            MyIcon.MouseDoubleClick += new MouseEventHandler(MyIcon_MouseDoubleClick);
            if (checkName == false) 
            {
                SetNickNameForm window=new SetNickNameForm();
                window.showWindow();

            }
            MyIcon.Icon = new Icon(@"icon.ico");
            UpdateAll();
          

        }


        // +++++++++++++++++++++++++++++++++++++++++++++++++++         BUTON FONKSİYONLARI        ++++++++++++++++++++++++++++++++++++++++++++++++++++++ //
        private void btnKelimeEkle_Click(object sender, EventArgs e)
        {
            //kelime eklediğimiz fonksiyon
            if (txt_ingKelimeEkle.Text == "" || txt_trKelimeEkle.Text == "")
            {
                errorMessageBox.ErrorMessage("Lütfen gerekli alanları giriniz.");
            }
            else if (cbox_groups2.Text == null) 
            {
                errorMessageBox.ErrorMessage("Lütfen grup seçiniz.Kelime grubu yoksa grup ekleyiniz.");
            }

            else if (txt_ingKelimeEkle.Text.Trim().Replace(" ","").All(c => Char.IsLetter(c))==false || txt_trKelimeEkle.Text.Trim().Replace(" ","").All(c => Char.IsLetter(c)) == false)
            {
                errorMessageBox.ErrorMessage("Özel karakter kullanmayınız.Sadece harfleri kullanınız.");
            }
            else
            {
                DatabaseManager.addWord(txt_ingKelimeEkle.Text, txt_trKelimeEkle.Text, cbox_groups2.Text);
                successMessageBox.SuccessMessage("Kelime başarıyla eklenmiştir.");
                txt_ingKelimeEkle.Text = String.Empty;
                txt_trKelimeEkle.Text = String.Empty;
                UpdateAll();
            }
            
        }
        private void btn_bilgileriGetir_Click(object sender, EventArgs e)
        {
            //tablodan verileri textboxlara getirdiğimiz fonksiyon
            if (table_words2.SelectedRows.Count == 0)
            {
                errorMessageBox.ErrorMessage("Lütfen önce tablodan kelime seçiniz.");
            }
            else
            {
                private_TXT_ID.Text = table_words2.SelectedRows[0].Cells[0].Value + string.Empty;
                txt_ingKelime.Text = table_words2.SelectedRows[0].Cells[1].Value + string.Empty;
                txt_trKelime.Text = table_words2.SelectedRows[0].Cells[2].Value + string.Empty;
                cbox_gruops.Text = table_words2.SelectedRows[0].Cells[5].Value + string.Empty;
                txt_ingKelime.ReadOnly = false;
                txt_trKelime.ReadOnly = false;
            }
        }
        private void btn_updateWord_Click(object sender, EventArgs e)
        {
            //kelime güncellediğimiz fonksiyon
            if (private_TXT_ID.Text == "")
            {
                errorMessageBox.ErrorMessage("Tablodan kelime seçip bilgileri getir butonuna basınız.");
            }
            else
            {
                if (txt_ingKelime.Text == "" | txt_trKelime.Text == "")
                {
                    errorMessageBox.ErrorMessage("Güncellemek için boş bırakılan yerleri doldurmalısınız.");
                }
                else if (txt_ingKelime.Text.Trim().Replace(" ","").All(c => Char.IsLetter(c)) == false || txt_trKelime.Text.Trim().Replace(" ", "").All(c => Char.IsLetter(c)) == false)
                {
                    errorMessageBox.ErrorMessage("Özel karakter kullanmayınız.Sadece harfleri kullanınız.");
                }
                else
                {
                    DatabaseManager.updateWord(txt_ingKelime.Text, txt_trKelime.Text, private_TXT_ID.Text,cbox_gruops.Text);
                    successMessageBox.SuccessMessage("Güncelleme işlemi başarıyla gerçekleştirilmiştir.");
                    UpdateAll();
                    txt_ingKelime.ReadOnly = true;
                    txt_trKelime.ReadOnly = true;
                    private_TXT_ID.Text = String.Empty;
                    txt_ingKelime.Text = String.Empty;
                    txt_trKelime.Text = String.Empty;
                    cbox_gruops.Text = null;
                }
            }
        }
        private void btn_clearTextBox_Click(object sender, EventArgs e)
        {
            //textboxları bosalttıgımız fonksiyon
            txt_ingKelime.ReadOnly = true;
            txt_trKelime.ReadOnly = true;
            cbox_gruops.Text = null;
            private_TXT_ID.Text = String.Empty;
            txt_ingKelime.Text = String.Empty;
            txt_trKelime.Text = String.Empty;
        }
        private void btn_removeWord_Click(object sender, EventArgs e)
        {
            //kelime sildiğimiz fonksiyon
            if (table_words3.SelectedRows.Count == 0)
            {
                errorMessageBox.ErrorMessage("Silmek istediğiniz kelimeyi tablodan seçiniz");
            }
            else { 
            string wordForMessageBox = table_words3.SelectedRows[0].Cells[1].Value + string.Empty;
            string wordID = table_words3.SelectedRows[0].Cells[0].Value + string.Empty;
            wordForMessageBox += " Kelimesini silmek istediğinizden emin misiniz?";
                if (MetroFramework.MetroMessageBox.Show(this, "", wordForMessageBox, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DatabaseManager.removeWord(wordID);
                    successMessageBox.SuccessMessage("Kelime başarıyla silinmiştir.");
                    UpdateAll();
                }
            }
        }
        private void btn_Start_Click(object sender, EventArgs e)
        {
            //Öğreticiyi başlattığımız fonksiyon
            if (cbox_educType.Text == null)
            {
                errorMessageBox.ErrorMessage("Lütfen öğretici modunu seçiniz.");
            }
            else if (cbox_minutes.Text == null) 
            {
                errorMessageBox.ErrorMessage("Lütfen dakikayı seçiniz.");
            }
            else
            {
                if (cbox_educType.Text == "Tüm grup kelimelerini sor")

                {
                    int countOfWords = DatabaseManager.sqlRowsCount();

                    if (switch_btn.Checked == false) // TÜM GRUP KELİMELERİNİ SOR || TÜM KELİMELERİ SOR
                    {
                        
                        if (countOfWords == 0)
                        {
                            errorMessageBox.ErrorMessage("Kayıtlı hiçbir kelime bulunmamaktadır.");
                        }
                        
                        else { 
                            askQuestion ask = new askQuestion();
                            ask.Baslat(Convert.ToInt32(cbox_minutes.Text) * 60 * 1000);
                            showNotif();
                            successMessageBox.SuccessMessageDialog("Programı kapatmak için sağ alttan ikona sağ tıklayınız.");

                        }
                    }
                    else
                    {
                        // TÜM GRUP KELİMELERİNİ SOR || YALNIZCA BİLİNMEYEN KELİMELERİ SOR
                        int countOfWords2 = DatabaseManager.sqlRowCountWithCode("SELECT COUNT(*) FROM Kelimeler WHERE Durum='Öğrenilmedi'");
                        if (countOfWords2 == 0)
                        {
                            errorMessageBox.ErrorMessage("Seçtiğiniz kriterlerde kelime bulunmamaktadır.");
                        }
                        else { 
                            askQuestion ask = new askQuestion();
                            ask.Baslat2(Convert.ToInt32(cbox_minutes.Text) * 60 * 1000);
                            showNotif();
                            successMessageBox.SuccessMessageDialog("Programı kapatmak için sağ alttan ikona sağ tıklayınız.");

                        }
                    }
                }
                else
                {                              // TÜM GRUP KELİMELERİNİ SORMA || TÜM KELİMELERİ SOR
                    int countOfWords =DatabaseManager.checkNumberOfWordsSQL(cbox_educType.Text);

                    if (switch_btn.Checked == false)
                    {
                        if (countOfWords == 0)
                        {
                            errorMessageBox.ErrorMessage("Kayıtlı hiçbir kelime bulunmamaktadır.");
                        }
                        else {
                            askQuestion ask = new askQuestion();
                            ask.Baslat3(Convert.ToInt32(cbox_minutes.Text) * 60 * 1000, cbox_educType.Text);
                            showNotif();
                            successMessageBox.SuccessMessageDialog("Programı kapatmak için sağ alttan ikona sağ tıklayınız.");

                        }
                    }
                    else
                    {                          // TÜM GRUP KELİMELERİNİ SORMA || YALNIZCA BİLİNMEYEN KELİMELERİ SOR
                        int countOfWords2 = DatabaseManager.checkNumberOfWordsSQL2(cbox_educType.Text);
                        if (countOfWords2 == 0)
                        {
                            errorMessageBox.ErrorMessage("Seçtiğiniz kriterlerde kelime bulunmamaktadır.");
                        }
                        else { 
                            askQuestion ask = new askQuestion();
                            ask.Baslat4(Convert.ToInt32(cbox_minutes.Text) * 60 * 1000, cbox_educType.Text);
                            showNotif();
                            successMessageBox.SuccessMessageDialog("Programı kapatmak için sağ alttan ikona sağ tıklayınız.");
                        }
                    }
                }
            }

        }
        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            //kelimenin değerlerini sıfırladığımız fonksiyon
            if (table_words3.SelectedRows.Count == 0)
            {
                errorMessageBox.ErrorMessage("Sıfırlamak istediğiniz kelimeyi tablodan seçiniz");
            }
            else
            {
                string wordForMessageBox = table_words3.SelectedRows[0].Cells[1].Value + string.Empty;
                string wordID = table_words3.SelectedRows[0].Cells[0].Value + string.Empty;
                wordForMessageBox += " Kelimesini sıfırlamak istediğinizden emin misiniz? Doğrularınız yanlışlarınız ve kelime oranınız 0 olacaktır.";
                if (MetroFramework.MetroMessageBox.Show(this, "", wordForMessageBox, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DatabaseManager.resetWord(wordID);
                    successMessageBox.SuccessMessage("Kelime başarıyla sıfırlanmıştır.");
                    UpdateAll();
                }
            }
        }
        private void btn_addGroup_Click(object sender, EventArgs e)
        {
            //yeni kelime grubu eklerken calıstırdıgımız fonksiyon
            addGroupFunc();
        }
        private void btn_addGroup2_Click(object sender, EventArgs e)
        {
            //yeni kelime grubu eklerken calıstırdıgımız fonksiyon
            addGroupFunc();
        }
        private void btn_setRight_Click(object sender, EventArgs e)
        {
            //kelimenin durumunu öğrenildi olarak değiştirdiğimiz fonksiyon
            if (table_words4.SelectedRows.Count == 0)
            {
                errorMessageBox.ErrorMessage("Lütfen tablodan bir kelime seçiniz.");
            }
            else { 
            string wordID = table_words4.SelectedRows[0].Cells[0].Value + string.Empty;
            DatabaseManager.updateWordsState(wordID, "Öğrenildi");
            successMessageBox.SuccessMessage("Kelime başarıyla güncellenmiştir");
            UpdateAll();
            }
        }
        private void btn_setWrong_Click(object sender, EventArgs e)
        {
            //kelimenin durumunu öğrenilmedi olarak değiştirdiğimiz fonksiyon
            if (table_words4.SelectedRows.Count == 0)
            {
                errorMessageBox.ErrorMessage("Lütfen tablodan bir kelime seçiniz.");
            }
            else { 
                string wordID = table_words4.SelectedRows[0].Cells[0].Value + string.Empty;
                DatabaseManager.updateWordsState(wordID, "Öğrenilmedi");
                successMessageBox.SuccessMessage("Kelime başarıyla güncellenmiştir");
                UpdateAll();
            }
        }
        private void btn_saveSettings_Click(object sender, EventArgs e)
        {
            //ayarları kayıt ederken calıstırdıgımız fonksiyon
            if (txt_userName.Text.Trim().Replace(" ","").All(c => Char.IsLetter(c)) == false)
            {
                txt_userName.Text = String.Empty;
                errorMessageBox.ErrorMessage("Özel karakter kullanmayınız.Sadece harfleri kullanınız.");

            }
            else { 

                SettingsManager.updateUserName(txt_userName.Text);
                SettingsManager.updateTableTheme(cbox_tableThemes.SelectedIndex);
                if (swtich_isOpenNotifSound.Checked == true)
                {
                    SettingsManager.updateNotifSound(true);
                }

                else
                {
                    SettingsManager.updateNotifSound(false);
                }
                successMessageBox.SuccessMessage("Ayarlar başarıyla kaydedilmiştir");
            }
            UpdateAll();


         }
        private void btn_addGroup3_Click(object sender, EventArgs e)
        {
            //yeni kelime grubu eklerken calıstırdıgımız fonksiyon
            addGroupFunc();
        }
        private void btn_getInfoOfGroup_Click(object sender, EventArgs e)
        {
            //tablodan textboxlara bilgi çektiğimiz fonksiyon
            if (table_Groups.SelectedRows.Count == 0)
            {
                errorMessageBox.ErrorMessage("Lütfen önce tablodan grup seçiniz.");
            }
            else
            {
                txt_nameOfGroups.Text = table_Groups.SelectedRows[0].Cells[1].Value + string.Empty;
                PRIVATE_txt_groupID.Text = table_Groups.SelectedRows[0].Cells[0].Value + string.Empty;
                txt_nameOfGroups.ReadOnly = false;
            }
        }
        private void btn_updateGroups_Click(object sender, EventArgs e)    
        {
            //Kelime grubunun ismini güncellediğimiz fonksiyon
            if (txt_nameOfGroups.ReadOnly == true)
            {
                errorMessageBox.ErrorMessage("Güncellemek için tablodan kelime grubu seçmelisiniz.");
            }
            else if (txt_nameOfGroups.Text.Trim() == "")
            {
                errorMessageBox.ErrorMessage("Lütfen güncellemek için isim yazınız.");
            }
            else if (txt_nameOfGroups.Text.Trim().Replace(" ","").All(c => Char.IsLetter(c)) == false)
            {
                errorMessageBox.ErrorMessage("Özel karakter kullanmayınız.Sadece harfleri kullanınız.");
            }
            else
            {
                bool checkDatabase = DatabaseManager.updateNameOfGroup(txt_nameOfGroups.Text.Trim(), PRIVATE_txt_groupID.Text);
                if (checkDatabase == false)
                {
                    errorMessageBox.ErrorMessage("Aynı isimde başka kelime grubu zaten kayıtlıdır");
                }
                else
                {
                    successMessageBox.SuccessMessage("Güncelleme işlemi başarıyla gerçekleştirilmiştir");
                    UpdateAll();
                    PRIVATE_txt_groupID.Text = String.Empty;
                    txt_nameOfGroups.Text = String.Empty;
                    txt_nameOfGroups.ReadOnly = true;
                    cbox_groups2.Text = null;
                }

            }
        }
        private void close_btn_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
        private void btn_removeGroup_Click(object sender, EventArgs e)
        {
            if (PRIVATE_txt_groupID.Text == "")
            {
                errorMessageBox.ErrorMessage("Lütfen tablodan bir kelime grubu seçiniz");
            }
            else
            {
                string message = "Eğer bu kelime grubunu silerseniz bu kelime grubuna ait tüm kelimeler de silinecektir.Onaylıyor musunuz?";
                if (MetroFramework.MetroMessageBox.Show(this, "", message, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DatabaseManager.removeGroupName(PRIVATE_txt_groupID.Text);
                    UpdateAll();
                    PRIVATE_txt_groupID.Text = string.Empty;
                    txt_nameOfGroups.Text = string.Empty;
                    txt_nameOfGroups.ReadOnly = true;
                    successMessageBox.SuccessMessage("Grup ve kelimeler başarıyla silinmiştir");
                    cbox_groups2.Text = null;
                }
            }

        }
        private void btn_clearTxts_Click(object sender, EventArgs e)
        {
            PRIVATE_txt_groupID.Text = string.Empty;
            txt_nameOfGroups.Text = string.Empty;
            txt_nameOfGroups.ReadOnly = true;
        }
        private void minimize_btn_Click(object sender, EventArgs e)
        {
 
            this.WindowState = FormWindowState.Minimized;
        }
        // --------------------------------------------------         BUTON FONKSİYONLARI        ------------------------------------------------------- //

        // +++++++++++++++++++++++++++++++++++++++++++++++++++ TEXT CHANGE FONKSİYONLARI ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //
        private void txt_wordSearch_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (radio_ingAra1.Checked == true)
                    (table_words1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Kelime LIKE '{0}%'", txt_wordSearch.Text);

                else if (radio_trAra1.Checked == true)
                    (table_words1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Anlamı LIKE '{0}%'", txt_wordSearch.Text);

                else
                { 
                    radio_ingAra1.Checked = true;
                    (table_words1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Kelime LIKE '{0}%'", txt_wordSearch.Text);
                }
            }
            catch 
            {
                txt_wordSearch.Text = String.Empty;
                errorMessageBox.ErrorMessage("Bir hata oluştu.Özel karakter kullanmayınız.");
            }
        }
        private void txt_wordSearch2_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (radio_ingAra2.Checked == true)

                    (table_words2.DataSource as DataTable).DefaultView.RowFilter = string.Format("Kelime LIKE '{0}%'", txt_wordSearch2.Text);

                else if (radio_trAra2.Checked == true)

                    (table_words2.DataSource as DataTable).DefaultView.RowFilter = string.Format("Anlamı LIKE '{0}%'", txt_wordSearch2.Text);

                else
                {
                    radio_ingAra2.Checked = true;
                    (table_words2.DataSource as DataTable).DefaultView.RowFilter = string.Format("Kelime LIKE '{0}%'", txt_wordSearch2.Text);
                }
            }
            catch 
            {
                txt_wordSearch2.Text = String.Empty;
                errorMessageBox.ErrorMessage("Bir hata oluştu.Özel karakter kullanmayınız.");
            }
        }
        private void txt_wordSearch3_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (radio_ingAra3.Checked == true)

                    (table_words3.DataSource as DataTable).DefaultView.RowFilter = string.Format("Kelime LIKE '{0}%'", txt_wordSearch3.Text);

                else if (radio_trAra3.Checked == true)

                    (table_words3.DataSource as DataTable).DefaultView.RowFilter = string.Format("Anlamı LIKE '{0}%'", txt_wordSearch3.Text);

                else
                { 
                    radio_ingAra3.Checked = true;
                    (table_words3.DataSource as DataTable).DefaultView.RowFilter = string.Format("Kelime LIKE '{0}%'", txt_wordSearch3.Text);
                }
            }
            catch { 
                txt_wordSearch3.Text = String.Empty;
                errorMessageBox.ErrorMessage("Bir hata oluştu.Özel karakter kullanmayınız.");
            }
        }
            private void txt_wordSearch4_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (radio_ingAra4.Checked == true)
                
                    (table_words4.DataSource as DataTable).DefaultView.RowFilter = string.Format("Kelime LIKE '{0}%'", txt_wordSearch4.Text);
                
                else if (radio_trAra4.Checked == true)
                
                    (table_words4.DataSource as DataTable).DefaultView.RowFilter = string.Format("Anlamı LIKE '{0}%'", txt_wordSearch4.Text);
                
                else
                {
                    radio_ingAra4.Checked = true;
                    (table_words4.DataSource as DataTable).DefaultView.RowFilter = string.Format("Kelime LIKE '{0}%'", txt_wordSearch4.Text);
                }
            }
            catch 
            {
                txt_wordSearch4.Text = String.Empty;
                errorMessageBox.ErrorMessage("Bir hata oluştu.Özel karakter kullanmayınız.");
            }
        }

        // -------------------------------------------------- TEXT CHANGE FONKSİYONLARI --------------------------------------------------------------- //

        // +++++++++++++++++++++++++++++++++++++++++++++++++++ Önemli FONKSİYONLAR ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //
        public void UpdateAll() 
        { //tüm güncelleme işlemlerinin çalıştırıldığı fonksiyon
            UpdateManager.updateHomePage();
            UpdateManager.updateTables();
            UpdateManager.updateTools();
            UpdateManager.openThemeSetting();
            whenOpeningManager.setUserNameOfLabel();
            whenOpeningManager.setUserNameOfTxtBox();
            whenOpeningManager.setNotifSoundSetting();
        }
        public void addGroupFunc()
        {   //kelime grubu eklenirken çalıstırılan formun methodur
            AddGroupForm addGroupForm = new AddGroupForm();
            addGroupForm.Show();
        }
        public void MyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            MyIcon.Visible = false;
        }
        public void MenuTest1_Click(object sender, EventArgs e)
        {
            successMessageBox.SuccessMessageDialog("Program kapatılıyor,iyi günler dileriz");
            System.Environment.Exit(0);
        }
        private void TrayMenuContext()
        {
            this.MyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this.MyIcon.ContextMenuStrip.Items.Add("Programı Kapat", null, this.MenuTest1_Click);
        }
        public void showNotif() 
        {
            Hide();
            MyIcon.Visible = true;
            MyIcon.Text = "Program İsmi";
            MyIcon.BalloonTipTitle = "Program Arkaplanda Çalışıyor";
            MyIcon.BalloonTipText = "Programı kapatmak için sağ alttan programın ikonuna sağ tıklayıp kapatabilirsiniz.";
            MyIcon.BalloonTipIcon = ToolTipIcon.Info;
            MyIcon.ShowBalloonTip(30000);
        }

        // ------------------------------------------------------- Önemli FONKSİYONLAR -------------------------------------------------------------- //

        // +++++++++++++++++++++++++++++++++++++++++++++++++++ Menü Butonları ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ //

        private void btn_AnaSayfa_Click(object sender, EventArgs e)
        {
            TabPages.SelectedIndex = 0;
            m_load_panel.Location = new Point(3, 6);
            UpdateManager.updateTables();

        }
        private void btn_KelimeEkle_Click(object sender, EventArgs e)
        {
            TabPages.SelectedIndex = 1;
            m_load_panel.Location = new Point(3, 54);
            UpdateManager.updateTables();


        }
        private void btn_KelimeGuncelle_Click(object sender, EventArgs e)
        {
            TabPages.SelectedIndex = 2;
            m_load_panel.Location = new Point(3, 103);
            UpdateManager.updateTables();


        }
        private void btn_removeWords_Click(object sender, EventArgs e)
        {
            TabPages.SelectedIndex = 3;
            m_load_panel.Location = new Point(3, 152);
            cbox_liste1.Text = "Tüm Kelimeler";
            UpdateManager.updateTables();

        }
        private void btn_allWords_Click(object sender, EventArgs e)
        {
            TabPages.SelectedIndex = 4;
            m_load_panel.Location = new Point(3, 201);
            cbox_liste2.Text = "Tüm Kelimeler";
            UpdateManager.updateTables();

        }
        private void btn_Settings_Click(object sender, EventArgs e)
        {
            TabPages.SelectedIndex = 5;
            m_load_panel.Location = new Point(3, 299);
            UpdateManager.updateTables();

        }
        private void btn_wordGroups_Click(object sender, EventArgs e)
        {
            TabPages.SelectedIndex = 6;
            m_load_panel.Location = new Point(3, 250);
            UpdateManager.updateTables();

        }

        // ------------------------------------------------------- Menü Butonları -------------------------------------------------------------- //

        private void cbox_liste2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbox_liste2.Text == "Tüm kelimeler")
            {
                UpdateManager.updateTables();
            }
            else
            {
                UpdateManager.updateSourceOfTable(cbox_liste2.Text, table_words4);
            }
        }

        private void cbox_liste1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbox_liste1.Text == "Tüm kelimeler")
            {
                UpdateManager.updateTables();
            }
            else
            {
                UpdateManager.updateSourceOfTable(cbox_liste1.Text, table_words3);
     
            }
        }

        private void cbox_tableThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            whenOpeningManager.showTableTheme(cbox_tableThemes.SelectedIndex);
        }

        private void btn_deleteAllWords_Click(object sender, EventArgs e)
        {
            string message = $"TÜM KELİMELERİ SİLMEK İSTEDİĞİNİZDEN EMİN MİSİNİZ?";
            if (MetroFramework.MetroMessageBox.Show(this, "", message, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DatabaseManager.removeAllWords();
                successMessageBox.SuccessMessage("Tüm kelimeler başarıyla silinmiştir.");
                UpdateAll();
                cbox_groups2.Text = null;
            }
        }

        private void btn_resetAllWords_Click(object sender, EventArgs e)
        {
            string message = $"TÜM KELİMELERİ SIFIRLAMAK İSTEDİĞİNİZDEN EMİN MİSİNİZ?";
            if (MetroFramework.MetroMessageBox.Show(this, "", message, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DatabaseManager.resetAllWords();
                successMessageBox.SuccessMessage("Tüm kelimeler başarıyla sıfırlanmıştır.");
                UpdateAll();
            }
        }

        private void bunifuLabel29_Click(object sender, EventArgs e)
        {
            radio_ingAra1.Checked = true;
            radio_trAra1.Checked = false;
        }

        private void bunifuLabel22_Click(object sender, EventArgs e)
        {
            radio_ingAra1.Checked = false;
            radio_trAra1.Checked = true;
        }

        private void bunifuLabel35_Click(object sender, EventArgs e)
        {
            radio_ingAra2.Checked = true;
            radio_trAra2.Checked = false;
        }

        private void bunifuLabel26_Click(object sender, EventArgs e)
        {
            radio_trAra2.Checked = true;
            radio_ingAra2.Checked = false;
        }

        private void bunifuLabel39_Click(object sender, EventArgs e)
        {
            radio_ingAra3.Checked = true;
            radio_trAra3.Checked = false;
        }

        private void bunifuLabel12_Click(object sender, EventArgs e)
        {
            radio_ingAra3.Checked = false;
            radio_trAra3.Checked = true;
        }

        private void bunifuLabel16_Click(object sender, EventArgs e)
        {
            radio_trAra4.Checked = false;
            radio_ingAra4.Checked = true;
        }

        private void bunifuLabel15_Click(object sender, EventArgs e)
        {
            radio_ingAra4.Checked = false;
            radio_trAra4.Checked = true;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_ingKelimeEkle.Text = String.Empty;
            txt_trKelimeEkle.Text = String.Empty;
            cbox_groups2.Text = null;
        }
        
        private void btn_menu_berkay_Click(object sender, EventArgs e)
        {
            DeveloperForm frm = new DeveloperForm();
            frm.ShowDialog();
        }

  

        private void txt_ingKelimeEkle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_trKelimeEkle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_ingKelime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_trKelime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void bunifuTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_wordSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_wordSearch2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_wordSearch3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_wordSearch4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_userName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_nameOfGroups_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }


    }
}
