using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace IngilizceKelime
{
    class whenOpeningManager
    {
        static Form1 form = Application.OpenForms.OfType<Form1>().FirstOrDefault();
        public static string veritabaniyolu = "Data source=Database.db";
        public static SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);

        public static void setUserNameOfLabel() 
        {
            baglanti.Open();
            string sqlCode = "SELECT UserName FROM Settings";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode,baglanti);
            string userName = Convert.ToString(cmd.ExecuteScalar());
            form.main_kullanici_name.Text = userName;
            baglanti.Close();
        }

        public static void setUserNameOfTxtBox()
        {
            baglanti.Open();
            string sqlCode = "SELECT UserName FROM Settings";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            string userName = Convert.ToString(cmd.ExecuteScalar());
            form.txt_userName.Text = userName;
            baglanti.Close();
        }
        public static void setNotifSoundSetting()
        {
            baglanti.Open();
            string sqlCode = "SELECT SoundNotification FROM Settings";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            string userName = Convert.ToString(cmd.ExecuteScalar());
            baglanti.Close();
            if (userName == "1")
            {
                form.swtich_isOpenNotifSound.Checked = true;
            }
            else 
            {
                form.swtich_isOpenNotifSound.Checked = false;
            }
        }

        public static void showTableTheme(int themeID)
        {
            baglanti.Open();
            string sqlCode = "SELECT TableTheme FROM Settings";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            int themeNo = Convert.ToInt32(cmd.ExecuteScalar());
            baglanti.Close();

            switch (themeID)
            {
                case 0:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Dark; break;
                case 1:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light; break;
                case 2:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Crimson; break;
                case 3:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.LimeGreen; break;
                case 4:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Yellow; break;
                case 5:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Orange; break;
                case 6:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Maroon; break;
                case 7:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Chocolate; break;
                case 8:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DodgerBlue; break;
                case 9:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Navy; break;
                case 10:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumSeaGreen; break;
                case 11:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Teal; break;
                case 12:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkSlateGray; break;
                case 13:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.ForestGreen; break;
                case 14:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.DarkViolet; break;
                case 15:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Purple; break;
                case 16:
                    form.try_table.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.MediumVioletRed; break;
                default:
                    break;
            }

        }


    }
}
