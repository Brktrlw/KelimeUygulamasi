using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System;
namespace IngilizceKelime
{
    class UpdateManager
    {

        static Form1 form = Application.OpenForms.OfType<Form1>().FirstOrDefault();
        public static string veritabaniyolu = "Data source=Database.db";
        public static SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);

        public static void updateTables()           //Tüm kelime tablolarının güncellendiği method
        {       
            baglanti.Open();
            string sqlWordTable = "SELECT ID,Kelime,Anlamı,Doğrular,Yanlışlar,Türİsmi,Durum FROM Kelimeler INNER JOIN Türler ON Kelimeler.Tür=Türler.TurID";                              
            SQLiteDataAdapter da = new SQLiteDataAdapter(sqlWordTable, baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            form.table_words1.DataSource = dt;
            form.table_words2.DataSource = dt;
            form.table_words3.DataSource = dt;
            form.table_words4.DataSource = dt;

            string sqlWordTableOfGroups = "SELECT * FROM Türler";
            SQLiteDataAdapter da2 = new SQLiteDataAdapter(sqlWordTableOfGroups, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            form.table_Groups.DataSource = dt2;

            baglanti.Close();
        }
        public static void updateHomePage() 
        {
            baglanti.Open();
            try
            {
                string sqlToplamKelime = "SELECT COUNT(*) FROM Kelimeler";
                SQLiteCommand cmd = new SQLiteCommand(sqlToplamKelime, baglanti);
                int toplamKelime = Convert.ToInt32(cmd.ExecuteScalar());
                form.lbl_toplamKelime.Text = Convert.ToString(toplamKelime);

                string sqlToplamDogru = "SELECT sum(Doğrular) FROM Kelimeler";
                SQLiteCommand cmd2 = new SQLiteCommand(sqlToplamDogru, baglanti);
                int toplamDogru = Convert.ToInt32(cmd2.ExecuteScalar());
                form.lbl_toplamDogru.Text = Convert.ToString(toplamDogru);

                string sqlToplamYanlis = "SELECT sum(Yanlışlar) FROM Kelimeler";
                SQLiteCommand cmd3 = new SQLiteCommand(sqlToplamYanlis, baglanti);
                int toplamYanlis = Convert.ToInt32(cmd3.ExecuteScalar());
                form.lbl_toplamYanlis.Text = Convert.ToString(toplamDogru);
            }
            catch { }
            baglanti.Close();
        }

        public static void updateTools() 
        {
            form.cbox_gruops.Items.Clear();
            form.cbox_groups2.Items.Clear();
            form.cbox_educType.Items.Clear();

            form.cbox_liste1.Items.Clear();
            form.cbox_liste2.Items.Clear();
            string text = "Tüm kelimeler";
            form.cbox_liste1.Items.Add(text);
            form.cbox_liste2.Items.Add(text);

            form.cbox_educType.Items.Add("Tüm grup kelimelerini sor");

            string sqlCode = "SELECT Türİsmi FROM Türler";
            baglanti.Open();
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string turAdi = Convert.ToString(dr[0]);
                form.cbox_gruops.Items.Add(turAdi);
                form.cbox_groups2.Items.Add(turAdi);
                form.cbox_educType.Items.Add(turAdi);
                form.cbox_liste1.Items.Add(turAdi);
                form.cbox_liste2.Items.Add(turAdi);
            }


            baglanti.Close();
        }

        public static void updateSourceOfTable(string textTable,Bunifu.UI.WinForms.BunifuDataGridView nameOfdatagrid) 
        {
            if (textTable == "Tüm kelimeler")
            {

            }
            else { 
                
                string sqlCode = $"SELECT ID,Kelime,Anlamı,Doğrular,Yanlışlar,Türİsmi,Durum FROM Kelimeler INNER JOIN Türler ON Kelimeler.Tür=Türler.TurID where Türİsmi='{textTable}'";
                SQLiteDataAdapter da = new SQLiteDataAdapter(sqlCode, baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                nameOfdatagrid.DataSource = dt;
                baglanti.Close();
            }
        }

        public static void openThemeSetting()
        {
            baglanti.Open();
            string sqlCode = "SELECT TableTheme FROM Settings";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            int themeNo=Convert.ToInt32(cmd.ExecuteScalar());
            form.cbox_tableThemes.SelectedIndex = themeNo;
            baglanti.Close();
            switch (themeNo)
            {
                case 0:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Dark;
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Dark;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Dark;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Dark;
                    form.table_Groups.Theme= Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Dark;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Dark;break;
                case 1:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light; break;
                case 2:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Crimson;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Crimson;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Crimson;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Crimson;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Crimson;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Crimson; break;
                case 3:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.LimeGreen;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.LimeGreen;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.LimeGreen;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.LimeGreen;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.LimeGreen;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.LimeGreen; break;
                case 4:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Yellow;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Yellow;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Yellow;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Yellow;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Yellow;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Yellow; break;
                case 5:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Orange;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Orange;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Orange;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Orange;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Orange;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Orange; break;
                case 6:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Maroon;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Maroon;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Maroon;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Maroon;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Maroon;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Maroon; break;
                case 7:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Chocolate;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Chocolate;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Chocolate;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Chocolate;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Chocolate;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Chocolate; break;
                case 8:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DodgerBlue;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DodgerBlue;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DodgerBlue;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DodgerBlue;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DodgerBlue;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DodgerBlue; break;
                case 9:
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Navy;
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Navy;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Navy;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Navy;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Navy;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Navy; break;
                case 10:
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumSeaGreen;
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumSeaGreen;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumSeaGreen;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumSeaGreen;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumSeaGreen;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumSeaGreen; break;
                case 11:
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Teal;
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Teal;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Teal;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Teal;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Teal;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Teal; break;
                case 12:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkSlateGray;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkSlateGray;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkSlateGray;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkSlateGray;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkSlateGray;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkSlateGray; break;
                case 13:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.ForestGreen;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.ForestGreen;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.ForestGreen;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.ForestGreen;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.ForestGreen;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.ForestGreen; break;
                case 14:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkViolet;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkViolet;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkViolet;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkViolet;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkViolet;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkViolet; break;
                case 15:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Purple;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Purple;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Purple;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Purple;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Purple;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Purple; break;
                case 16:
                    form.table_words1.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumVioletRed;
                    form.table_Groups.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumVioletRed;
                    form.table_words2.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumVioletRed;
                    form.table_words3.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumVioletRed;
                    form.table_words4.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumVioletRed;
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumVioletRed; break;
                default:
                    break;
            }
        }


    }

}
