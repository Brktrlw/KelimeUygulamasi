﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace IngilizceKelime
{
    public partial class askQuestion : Form
    {
        SuccessMessageBox successMessageBox = new SuccessMessageBox();
        ErrorMessageBox errorMessageBox = new ErrorMessageBox();


        public askQuestion()
        {
            InitializeComponent();
        }
        int i = 0;
        public void Baslat(int Minute,string askType)
        {

            Thread th = new Thread(t =>
            {
                Random random = new Random();
                askQuestion ask = new askQuestion();
                ask.StartPosition = FormStartPosition.Manual;
                ask.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - ask.Width, Screen.PrimaryScreen.WorkingArea.Height - ask.Height);
                int toplamKelime = DatabaseManager.sqlRowsCount();


                string veritabaniyolu = "Data source=Database.db";
                SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu); 

                string sql = "SELECT ID,Kelime,Anlamı,[İngilizce Cümle] FROM Kelimeler";
                baglanti.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, baglanti);
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string wordID = Convert.ToString(dr[0]);
                    string kelime = Convert.ToString(dr[1]);
                    string anlam = Convert.ToString(dr[2]);
                    string sentence = Convert.ToString(dr[3]);
                    ask.listBox1.Items.Add(kelime);
                    ask.listBox2.Items.Add(anlam);
                    ask.listBox3.Items.Add(wordID);
                    ask.listBox_sencente.Items.Add(sentence);
                }
                baglanti.Close();

            while (i < ask.listBox1.Items.Count)
            {
                    ask.txt_trKelime.Text = String.Empty;
                    ask.txt_ingKelimeEkle.Text = String.Empty;
                    ask.bunifuGradientPanel5.Visible = true;
                    ask.bunifuGradientPanel2.Visible = false;
                    ask.bunifuButton1.Visible = false;
                    ask.btnKelimeEkle.Visible=true;
                    ask.lbl_ipucu.Text = "İpucu";

                    ask.Private_Index.Text= Convert.ToString(random.Next(ask.listBox1.Items.Count)); //rastgele seçtiğimiz index sayısı
                    int sayi;
                    if (askType == "Sadece İngilizcesini Sor")
                    {   //sadece ing halini soruyor
                        sayi = 0;
                    }
                    else if (askType == "Sadece Türkçesini Sor")
                    {   //sadece tr halini soruyor
                        sayi = 1;
                    }
                    else 
                    {  //ing tr karışık
                        sayi = random.Next(2);  //0 türkçe 1 ingilizce 2 karışık
                    }
                    
                    ask.lbl_1_2.Text = Convert.ToString(sayi);
                    if (sayi == 1)
                    {            //ingilizce kelimeyi türkçe yazdığımız blok
                        ask.lbl_eng.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_TR.Text = Convert.ToString(ask.listBox2.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_ENG.Text= Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.lbl_sentence.Text = Convert.ToString(ask.listBox_sencente.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.txt_trKelime.Visible = true;
                        ask.txt_ingKelimeEkle.Visible = false;
                        ask.bunifuLabel2.Text = "Kelimesinin Türkçesini Yazınız";
                    }       
                    else {      //türkçe kelimeyi ingilizce yazdığımız blok
                        ask.lbl_eng.Text = Convert.ToString(ask.listBox2.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_TR.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_ENG.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.lbl_sentence.Text = Convert.ToString(ask.listBox_sencente.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.txt_trKelime.Visible = false;
                        ask.txt_ingKelimeEkle.Visible = true;
                        ask.bunifuLabel2.Text = "Kelimesinin ingilizcesini Yazınız";

                    }


                    Thread.Sleep(2000); //Buraya Minute gelecek

                    ask.ShowDialog();
                    
                            
                }
                MessageBox.Show("Tüm kelimeler tamamlanmıştır,tebrikler", "Öğretici Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    Application.Exit();
            })
            {

            };
            th.Start();

        } // TÜM GRUP KELİMELERİNİ SOR || TÜM KELİMELERİ SOR

        public void Baslat2(int Minute,string askType) 
        {
            Thread th = new Thread(t =>
            {
                Random random = new Random();
                askQuestion ask = new askQuestion();
                ask.StartPosition = FormStartPosition.Manual;
                ask.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - ask.Width, Screen.PrimaryScreen.WorkingArea.Height - ask.Height);
                int toplamKelime = DatabaseManager.sqlRowsCount();


                string veritabaniyolu = "Data source=Database.db";
                SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);

                string sql = "SELECT ID,Kelime,Anlamı,[İngilizce Cümle] FROM Kelimeler where Durum='Öğrenilmedi'";
                baglanti.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, baglanti);
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string wordID = Convert.ToString(dr[0]);
                    string kelime = Convert.ToString(dr[1]);
                    string anlam = Convert.ToString(dr[2]);
                    string sentence = Convert.ToString(dr[3]);
                    ask.listBox1.Items.Add(kelime);
                    ask.listBox2.Items.Add(anlam);
                    ask.listBox3.Items.Add(wordID);
                    ask.listBox_sencente.Items.Add(sentence);
                }
                baglanti.Close();

                while (i < ask.listBox1.Items.Count)
                {
                    ask.txt_trKelime.Text = String.Empty;
                    ask.txt_ingKelimeEkle.Text = String.Empty;
                    ask.bunifuGradientPanel5.Visible = true;
                    ask.bunifuGradientPanel2.Visible = false;
                    ask.bunifuButton1.Visible = false;
                    ask.btnKelimeEkle.Visible = true;
                    ask.lbl_ipucu.Text = "İpucu";

                    ask.Private_Index.Text = Convert.ToString(random.Next(ask.listBox1.Items.Count)); //rastgele seçtiğimiz index sayısı

                    int sayi;
                    if (askType == "Sadece İngilizcesini Sor")
                    {   //sadece ing halini soruyor
                        sayi = 0;
                    }
                    else if (askType == "Sadece Türkçesini Sor")
                    {   //sadece tr halini soruyor
                        sayi = 1;
                    }
                    else
                    {  //ing tr karışık
                        sayi = random.Next(2);  //0 türkçe 1 ingilizce 2 karışık
                    }

                    ask.lbl_1_2.Text = Convert.ToString(sayi);
                    if (sayi == 1)
                    {            //ingilizce kelimeyi türkçe yazdığımız blok
                        ask.lbl_eng.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_TR.Text = Convert.ToString(ask.listBox2.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_ENG.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.lbl_sentence.Text = Convert.ToString(ask.listBox_sencente.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.txt_trKelime.Visible = true;
                        ask.txt_ingKelimeEkle.Visible = false;
                        ask.bunifuLabel2.Text = "Kelimesinin Türkçesini Yazınız";
                    }
                    else
                    {      //türkçe kelimeyi ingilzice yazdığımız blok
                        ask.lbl_eng.Text = Convert.ToString(ask.listBox2.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_TR.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_ENG.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.lbl_sentence.Text = Convert.ToString(ask.listBox_sencente.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.txt_trKelime.Visible = false;
                        ask.txt_ingKelimeEkle.Visible = true;
                        ask.bunifuLabel2.Text = "Kelimesinin ingilizcesini Yazınız";

                    }


                    Thread.Sleep(2000); //Buraya Minute gelecek

                    ask.ShowDialog();

                }
                MessageBox.Show("Tüm kelimeler tamamlanmıştır,tebrikler", "Öğretici Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Application.Exit();
            })
            {

            };
            th.Start();
        } // TÜM GRUP KELİMELERİNİ SOR || YALNIZCA BİLİNMEYEN KELİMELERİ SOR

        public void Baslat3(int Minute, string TurName,string askType) // TÜM GRUP KELİMELERİNİ SORMA || TÜM KELİMELERİ SOR 
        {
            
            Thread th = new Thread(t =>
            {
                Random random = new Random();
                askQuestion ask = new askQuestion();
                ask.StartPosition = FormStartPosition.Manual;
                ask.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - ask.Width, Screen.PrimaryScreen.WorkingArea.Height - ask.Height);
                int toplamKelime = DatabaseManager.sqlRowsCount();


                string veritabaniyolu = "Data source=Database.db";
                SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);
                baglanti.Open();
                string checkWordType = $"SELECT TurID FROM Türler WHERE Türİsmi='{TurName}'";
                SQLiteCommand cmd5 = new SQLiteCommand(checkWordType,baglanti);
                string TurID = Convert.ToString(cmd5.ExecuteScalar());

                string sql = $"SELECT ID,Kelime,Anlamı,[İngilizce Cümle] FROM Kelimeler where Tür='{TurID}'";
                
                SQLiteCommand cmd = new SQLiteCommand(sql, baglanti);
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string wordID = Convert.ToString(dr[0]);
                    string kelime = Convert.ToString(dr[1]);
                    string anlam = Convert.ToString(dr[2]);
                    string sentence = Convert.ToString(dr[3]);
                    ask.listBox1.Items.Add(kelime);
                    ask.listBox2.Items.Add(anlam);
                    ask.listBox3.Items.Add(wordID);
                    ask.listBox_sencente.Items.Add(sentence);
                }
                baglanti.Close();

                while (i < ask.listBox1.Items.Count)
                {
                    ask.txt_trKelime.Text = String.Empty;
                    ask.txt_ingKelimeEkle.Text = String.Empty;
                    ask.bunifuGradientPanel5.Visible = true;
                    ask.bunifuGradientPanel2.Visible = false;
                    ask.bunifuButton1.Visible = false;
                    ask.btnKelimeEkle.Visible = true;
                    ask.lbl_ipucu.Text = "İpucu";

                    ask.Private_Index.Text = Convert.ToString(random.Next(ask.listBox1.Items.Count)); //rastgele seçtiğimiz index sayısı
                    int sayi;
                    if (askType == "Sadece İngilizcesini Sor")
                    {   //sadece ing halini soruyor
                        sayi = 0;
                    }
                    else if (askType == "Sadece Türkçesini Sor")
                    {   //sadece tr halini soruyor
                        sayi = 1;
                    }
                    else
                    {  //ing tr karışık
                        sayi = random.Next(2);  //0 türkçe 1 ingilizce 2 karışık
                    }

           
                    ask.lbl_1_2.Text = Convert.ToString(sayi);
                    if (sayi == 1)
                    {            //ingilizce kelimeyi türkçe yazdığımız blok
                        ask.lbl_eng.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_TR.Text = Convert.ToString(ask.listBox2.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_ENG.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.lbl_sentence.Text = Convert.ToString(ask.listBox_sencente.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.txt_trKelime.Visible = true;
                        ask.txt_ingKelimeEkle.Visible = false;
                        ask.bunifuLabel2.Text = "Kelimesinin Türkçesini Yazınız";
                    }
                    else
                    {      //türkçe kelimeyi ingilzice yazdığımız blok
                        ask.lbl_eng.Text = Convert.ToString(ask.listBox2.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_TR.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_ENG.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.lbl_sentence.Text = Convert.ToString(ask.listBox_sencente.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.txt_trKelime.Visible = false;
                        ask.txt_ingKelimeEkle.Visible = true;
                        ask.bunifuLabel2.Text = "Kelimesinin ingilizcesini Yazınız";

                    }


                    Thread.Sleep(2000); //Buraya Minute gelecek

                    ask.ShowDialog();

                }
                MessageBox.Show("Tüm kelimeler tamamlanmıştır,tebrikler", "Öğretici Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Application.Exit();
            })
            {

            };
            th.Start();
        }

        public void Baslat4(int Minute, string TurName,string askType) // TÜM GRUP KELİMELERİNİ SORMA || YALNIZCA BİLİNMEYEN KELİMELERİ SOR
        {
            Thread th = new Thread(t =>
            {
                Random random = new Random();
                askQuestion ask = new askQuestion();
                ask.StartPosition = FormStartPosition.Manual;
                ask.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - ask.Width, Screen.PrimaryScreen.WorkingArea.Height - ask.Height);
                int toplamKelime = DatabaseManager.sqlRowsCount();


                string veritabaniyolu = "Data source=Database.db";
                SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);
                baglanti.Open();
                string checkWordType = $"SELECT TurID FROM Türler WHERE Türİsmi='{TurName}'";
                SQLiteCommand cmd5 = new SQLiteCommand(checkWordType, baglanti);
                string TurID = Convert.ToString(cmd5.ExecuteScalar());

                string sql = $"SELECT ID,Kelime,Anlamı,[İngilizce Cümle] FROM Kelimeler where Tür='{TurID}' AND Durum='Öğrenilmedi'";

                SQLiteCommand cmd = new SQLiteCommand(sql, baglanti);
                SQLiteDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string wordID = Convert.ToString(dr[0]);
                    string kelime = Convert.ToString(dr[1]);
                    string anlam = Convert.ToString(dr[2]);
                    string sentence = Convert.ToString(dr[3]);
                    ask.listBox1.Items.Add(kelime);
                    ask.listBox2.Items.Add(anlam);
                    ask.listBox3.Items.Add(wordID);
                    ask.listBox_sencente.Items.Add(sentence);
                }
                baglanti.Close();

                while (i < ask.listBox1.Items.Count)
                {
                    ask.txt_trKelime.Text = String.Empty;
                    ask.txt_ingKelimeEkle.Text = String.Empty;
                    ask.bunifuGradientPanel5.Visible = true;
                    ask.bunifuGradientPanel2.Visible = false;
                    ask.bunifuButton1.Visible = false;
                    ask.btnKelimeEkle.Visible = true;
                    ask.lbl_ipucu.Text = "İpucu";

                    ask.Private_Index.Text = Convert.ToString(random.Next(ask.listBox1.Items.Count)); //rastgele seçtiğimiz index sayısı

                    int sayi;
                    if (askType == "Sadece İngilizcesini Sor")
                    {   //sadece ing halini soruyor
                        sayi = 0;
                    }
                    else if (askType == "Sadece Türkçesini Sor")
                    {   //sadece tr halini soruyor
                        sayi = 1;
                    }
                    else
                    {  //ing tr karışık
                        sayi = random.Next(2);  //0 türkçe 1 ingilizce 2 karışık
                    }
   
                    ask.lbl_1_2.Text = Convert.ToString(sayi);
                    if (sayi == 1)
                    {            //ingilizce kelimeyi türkçe yazdığımız blok
                        ask.lbl_eng.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_TR.Text = Convert.ToString(ask.listBox2.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_ENG.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.lbl_sentence.Text = Convert.ToString(ask.listBox_sencente.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.txt_trKelime.Visible = true;
                        ask.txt_ingKelimeEkle.Visible = false;
                        ask.bunifuLabel2.Text = "Kelimesinin Türkçesini Yazınız";
                    }
                    else
                    {      //türkçe kelimeyi ingilzice yazdığımız blok
                        ask.lbl_eng.Text = Convert.ToString(ask.listBox2.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_TR.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.Private_ENG.Text = Convert.ToString(ask.listBox1.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.lbl_sentence.Text = Convert.ToString(ask.listBox_sencente.Items[Convert.ToInt32(ask.Private_Index.Text)].ToString());
                        ask.txt_trKelime.Visible = false;
                        ask.txt_ingKelimeEkle.Visible = true;
                        ask.bunifuLabel2.Text = "Kelimesinin ingilizcesini Yazınız";

                    }


                    Thread.Sleep(2000); //Buraya Minute gelecek

                    ask.ShowDialog();

                }
                MessageBox.Show("Tüm kelimeler tamamlanmıştır,tebrikler", "Öğretici Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Application.Exit();
            })
            {

            };
            th.Start();
        }

    
        public void btnKelimeEkle_Click(object sender, EventArgs e)
        {

            if (lbl_1_2.Text == "1")        //ingilizce kelimeyi türkçe yazdığımız blok
            {
                
                if (Private_TR.Text.Trim().ToLower().Replace("ı","i").Replace("'", "").Replace(" ","").Replace("’", "") == txt_trKelime.Text.Trim().ToLower().Replace("ı", "i").Replace("'", "").Replace(" ", "").Replace("’", ""))    //Doğruysa calısan blok
                {

                    this.Close();


                    MetroFramework.MetroMessageBox.Show(this, "Tebrikler \n"+lbl_sentence.Text, "Cevabınız Doğru", MessageBoxButtons.OK,MessageBoxIcon.Asterisk,160);

                    txt_trKelime.Text = "";
            
                    listBox1.Items.RemoveAt(Convert.ToInt32(Private_Index.Text));
                    listBox2.Items.RemoveAt(Convert.ToInt32(Private_Index.Text));
                    
                    string ID=listBox3.Items[Convert.ToInt32(Private_Index.Text)].ToString();
                    i++;
                    PointManager.UpdateTrue(ID);
                    listBox3.Items.RemoveAt(Convert.ToInt32(Private_Index.Text));
                    listBox_sencente.Items.RemoveAt(Convert.ToInt32(Private_Index.Text));
                }
                else
                {
                    this.Close();
                    errorMessageBox.ErrorMessageDialog("Cevabınız yanlış. Doğrusu: "+ Private_TR.Text,lbl_sentence.Text);
                    txt_trKelime.Text = "";
                    string ID = listBox3.Items[Convert.ToInt32(Private_Index.Text)].ToString();
                    PointManager.UpdateFalse(ID);
    

                }

            }
            else {
                if (Private_ENG.Text.Trim().ToLower().Replace("ı", "i").Replace("'", "").Replace(" ", "").Replace("’", "") == txt_ingKelimeEkle.Text.Trim().ToLower().Replace("ı", "i").Replace("'", "").Replace(" ", "").Replace("’", ""))    //Doğruysa calısan blok
                {
                    this.Close();
                    MetroFramework.MetroMessageBox.Show(this, "Tebrikler \n"+lbl_sentence.Text, "Cevabınız Doğru", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,160);

                    txt_trKelime.Text = "";
               
                    listBox1.Items.RemoveAt(Convert.ToInt32(Private_Index.Text));
                    listBox2.Items.RemoveAt(Convert.ToInt32(Private_Index.Text));
                    
                    string ID = listBox3.Items[Convert.ToInt32(Private_Index.Text)].ToString();
                    i++;
                    PointManager.UpdateTrue(ID);
                    listBox3.Items.RemoveAt(Convert.ToInt32(Private_Index.Text));
                    listBox_sencente.Items.RemoveAt(Convert.ToInt32(Private_Index.Text));

                }
                else {
                    this.Close();
                    errorMessageBox.ErrorMessageDialog("YANLIŞ, Doğrusu: " + Private_ENG.Text,lbl_sentence.Text);
                    txt_ingKelimeEkle.Text = "";
                    string ID = listBox3.Items[Convert.ToInt32(Private_Index.Text)].ToString();
                    PointManager.UpdateFalse(ID);
               

                }
            }
            
            

        }
        private void bunifuButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Öğreticiyi Sonlandırmak İstediğinizden Emin Misiniz,öğretici tamamen kapanacaktır.", "Emin misiniz?", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                System.Environment.Exit(0);
            }

        }

        private void bunifuPictureBox2_Click(object sender, EventArgs e)
        {
            Random rastgele = new Random();
            string mesaj = Private_TR.Text;
            string sMesaj = new string('*', mesaj.Length);

            List<char> datalist = new List<char>();
            List<string> harflerListesi = new List<string>();
            List<int> indexListesi = new List<int>();

            IDictionary<int, string> asilListe = new Dictionary<int, string>();



            datalist.AddRange(sMesaj);

            int mesajUzungulu = datalist.Count;
            int gosterilecekKarakterSayisi = mesajUzungulu / 2 - 1;
          

            int[] sayilar = new int[gosterilecekKarakterSayisi];
            int donguSayaci = 0;
            while (gosterilecekKarakterSayisi > 0)
            {

                int karakterIndexi = rastgele.Next(0, mesajUzungulu - 1);
                if (sayilar.Contains(karakterIndexi))
                    continue;
                else
                {
                    string karakter = mesaj.Substring(karakterIndexi, 1);
                    asilListe.Add(karakterIndexi, karakter);
                    sayilar[donguSayaci] = karakterIndexi;
                    donguSayaci++;
                    gosterilecekKarakterSayisi--;
                }

            }
            StringBuilder sb = new StringBuilder(sMesaj);


            foreach (KeyValuePair<int, string> kvp in asilListe)
            {
                
                sb[Convert.ToInt32(kvp.Key)] = Convert.ToChar(kvp.Value);
                sMesaj = sb.ToString();

            }
            bunifuLabel2.Text = sMesaj;
            bunifuLabel1.Visible = false;




        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {
            if (lbl_1_2.Text == "1")
            {
                txt_trKelime.Text = Private_TR.Text;
                bunifuGradientPanel5.Visible = false;
                bunifuGradientPanel2.Visible = true;
                bunifuButton1.Visible = true;
            }
            else {
                txt_ingKelimeEkle.Text = Private_ENG.Text;
                bunifuGradientPanel5.Visible = false;
                bunifuGradientPanel2.Visible = true;
                bunifuButton1.Visible = true;
            }

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            bunifuGradientPanel5.Visible = true;
            bunifuGradientPanel2.Visible = false;
            bunifuButton1.Visible = false;
            btnKelimeEkle.Visible = true;
            txt_trKelime.Text = String.Empty;
            txt_ingKelimeEkle.Text = String.Empty;
            this.Close();
        }

        private void txt_trKelime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        private void txt_ingKelimeEkle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

    }
}
