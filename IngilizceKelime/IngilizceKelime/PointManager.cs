using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IngilizceKelime
{
    class PointManager
    {
        public static string veritabaniyolu = "Data source=Database.db";
        public static SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);
        public static void UpdateTrue(string ID) 
        {
            baglanti.Open();
            string checkCountOfTrue = $"SELECT Doğrular FROM Kelimeler where ID='{ID}'";
            SQLiteCommand cmd = new SQLiteCommand(checkCountOfTrue,baglanti);
            int CountOfTrue = Convert.ToInt32(cmd.ExecuteScalar());
            CountOfTrue += 1;
            string upgradeTrue = $"UPDATE Kelimeler set Doğrular='{CountOfTrue}' where ID='{ID}'";
            SQLiteCommand cmd2 = new SQLiteCommand(upgradeTrue, baglanti);
            cmd2.ExecuteNonQuery();
            baglanti.Close();
        }
        public static void UpdateFalse(string ID) 
        {
            baglanti.Open();
            string checkCountOfTrue = $"SELECT Yanlışlar FROM Kelimeler where ID='{ID}'";
            SQLiteCommand cmd = new SQLiteCommand(checkCountOfTrue, baglanti);
            int CountOfTrue = Convert.ToInt32(cmd.ExecuteScalar());
            CountOfTrue += 1;
            string upgradeTrue = $"UPDATE Kelimeler set Yanlışlar='{CountOfTrue}' where ID='{ID}'";
            SQLiteCommand cmd2 = new SQLiteCommand(upgradeTrue, baglanti);
            cmd2.ExecuteNonQuery();
            baglanti.Close();
        }
    }
}
