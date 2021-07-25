using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace IngilizceKelime
{
    class SettingsManager
    {
        static Form1 form = Application.OpenForms.OfType<Form1>().FirstOrDefault();
        public static string veritabaniyolu = "Data source=Database.db";
        public static SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);

        public static void updateNotifSound(bool isTrue)
        {
            baglanti.Open();
            string sqlCode;
            if (isTrue == true)
            {
                sqlCode = "UPDATE Settings set SoundNotification='1'";
            }
            else {
                sqlCode = "UPDATE Settings set SoundNotification='0'";
            }
            SQLiteCommand cmd = new SQLiteCommand(sqlCode,baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }
        public static void updateUserName(string userName) 
        {
            baglanti.Open();
            string sqlCode = $"UPDATE Settings set UserName='{userName}'";
            string sqlCode2 = $"UPDATE Settings set CheckUserName='1'";

            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            SQLiteCommand cmd2 = new SQLiteCommand(sqlCode2, baglanti);
            cmd2.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }

        public static void updateTableTheme(int themeID) 
        {
            baglanti.Open();
            string sqlCode = $"UPDATE Settings SET TableTheme='{themeID}'";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }

        

        public static bool checkNickName() 
        {
            baglanti.Open();
            string sqlCode = "SELECT CheckUserName FROM Settings";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteScalar();
            string name = Convert.ToString(cmd.ExecuteScalar());
            baglanti.Close();
            if (name == "0")
            {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
