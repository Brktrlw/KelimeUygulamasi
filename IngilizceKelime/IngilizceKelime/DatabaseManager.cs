
using System.Linq;
using System.Windows.Forms;
using System.Data.SQLite;
using System;


namespace IngilizceKelime
{
    class DatabaseManager
    {
        static Form1 form = Application.OpenForms.OfType<Form1>().FirstOrDefault();
        
        
        public static string veritabaniyolu = "Data source=Database.db";
        public static SQLiteConnection baglanti = new SQLiteConnection(veritabaniyolu);
        
        public static void updateWord(string Key,string Value,string wordID,string TurIsmi,string ingSentence="")    //veritabanındaki kelimeleri güncelleyen method
        {  
            baglanti.Open();
            string sqlCode2 = $"SELECT TurID FROM Türler where Türİsmi='{TurIsmi}'";
            SQLiteCommand cmd2 = new SQLiteCommand(sqlCode2, baglanti);
            string TurID = Convert.ToString(cmd2.ExecuteScalar());
            string sqlCode = $"UPDATE Kelimeler set Kelime='{Key}', Anlamı='{Value}',Tür='{TurID}',[İngilizce Cümle]='{ingSentence}' where ID='{wordID}'";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }
        public static void updateWordsState(string ID,string State)
        {
            baglanti.Open();
            string sqlCode = $"UPDATE Kelimeler set Durum='{State}' where ID='{ID}'";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }
        public static void addWord(string wordKey,string wordValue,string wordType,string ingSentence="")         //veritabanına yeni kelime ekleyen method
        {
            baglanti.Open();
            string typeCheck = $"SELECT TurID FROM Türler where Türİsmi='{wordType}'";
            SQLiteCommand cmd2 = new SQLiteCommand(typeCheck, baglanti);
            string ID = Convert.ToString(cmd2.ExecuteScalar());
            string sqlCode = $"INSERT INTO Kelimeler ('Kelime','Anlamı','Yanlışlar','Doğrular','Tür','Durum','İngilizce Cümle') VALUES ('{wordKey.Trim()}','{wordValue.Trim()}','0','0','{ID}','Öğrenilmedi','{ingSentence}')";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }
        public static void removeWord(string wordID)                     //veritabanından kelime silen method
        {
            baglanti.Open();
            string sqlCode = $"DELETE FROM Kelimeler where ID={wordID}";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }
        public static int sqlRowsCount() 
        {
            baglanti.Open();
            string sqlCode = $"SELECT COUNT(*) FROM Kelimeler";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            int toplamKelime = Convert.ToInt32(cmd.ExecuteScalar());
            baglanti.Close();
            return toplamKelime;
        }
        public static void addGroup(string GroupName) 
        {
            baglanti.Open();
            string sqlCode = $"INSERT INTO Türler ('Türİsmi') VALUES ('{GroupName}')";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            int toplamKelime = Convert.ToInt32(cmd.ExecuteScalar());
            baglanti.Close();
        }
        public static string checkTurName(string turIsmi) 
        {
            baglanti.Open();
            string sqlCode = $"SELECT Türİsmi FROM Türler where Türİsmi='{turIsmi}'";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            string turCheck = Convert.ToString(cmd.ExecuteScalar());
            baglanti.Close();
            return turCheck;
        }
        public static int checkNumberOfWordsSQL(string TypeName)
        {
            baglanti.Open();

            string sqlCode2 = $"SELECT TurID FROM Türler where Türİsmi='{TypeName}'";
            SQLiteCommand cmd2 = new SQLiteCommand(sqlCode2, baglanti);
            string Turid = Convert.ToString(cmd2.ExecuteScalar());

            string sqlCode = $"SELECT COUNT(*) FROM Kelimeler where Tür='{Turid}'";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            int countOfWord = Convert.ToInt32(cmd.ExecuteScalar());
            baglanti.Close();
            return countOfWord;
        }
        public static int checkNumberOfWordsSQL2(string TypeName)
        {
            baglanti.Open();

            string sqlCode2 = $"SELECT TurID FROM Türler where Türİsmi='{TypeName}'";
            SQLiteCommand cmd2 = new SQLiteCommand(sqlCode2, baglanti);
            string Turid = Convert.ToString(cmd2.ExecuteScalar());

            string sqlCode = $"SELECT COUNT(*) FROM Kelimeler where Tür='{Turid}' and Durum='Öğrenilmedi'";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            int countOfWord = Convert.ToInt32(cmd.ExecuteScalar());
            baglanti.Close();
            return countOfWord;
        }

        public static void resetWord(string wordID) 
        {
            baglanti.Open();
            string sqlCode = $"UPDATE Kelimeler set Doğrular='0',Yanlışlar='0',Durum='Öğrenilmedi' where ID='{wordID}'";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode,baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }
        public static int isTrueNotif() 
        {
            baglanti.Open();
            string sqlCode = "SELECT SoundNotification FROM Settings";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            int isTrueOrFalse=Convert.ToInt32(cmd.ExecuteScalar());
            baglanti.Close();
            return isTrueOrFalse;
        }

        public static bool updateNameOfGroup(string nameOfGroup,string IdOfGroup) 
        {
            baglanti.Open();
            string checkNameOfGroup = $"SELECT Türİsmi FROM Türler where Türİsmi='{nameOfGroup}'";
            SQLiteCommand cmd2 = new SQLiteCommand(checkNameOfGroup, baglanti);
            string nameGroup = Convert.ToString(cmd2.ExecuteScalar());
            if (nameGroup == nameOfGroup)
            {
                baglanti.Close();
                return false;
            }
            else 
            {
                string sqlCode = $"UPDATE Türler set Türİsmi='{nameOfGroup}' where TurID='{IdOfGroup}'";
                SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                return true;
            }
          
        }

        public static void removeGroupName(string IdOfGroup) 
        {
            baglanti.Open();
            string sqlCode = $"DELETE FROM Türler where TurID={IdOfGroup}";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteNonQuery();
            string sqlCode2 = $"DELETE FROM Kelimeler where Tür='{IdOfGroup}'";
            SQLiteCommand cmd2 = new SQLiteCommand(sqlCode2, baglanti);
            cmd2.ExecuteNonQuery();
            baglanti.Close();
        }

        public static void removeAllWords() 
        {
            baglanti.Open();
            string sqlCode = "DELETE FROM Kelimeler";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteNonQuery();

            string sqlCode3 = "DELETE FROM Türler";
            SQLiteCommand cmd3 = new SQLiteCommand(sqlCode3, baglanti);
            cmd3.ExecuteNonQuery();

            string sqlCode2 = "UPDATE sqlite_sequence set seq='0'";
            SQLiteCommand cmd2 = new SQLiteCommand(sqlCode2, baglanti);
            cmd2.ExecuteNonQuery();
            baglanti.Close();
        }
        public static void resetAllWords() 
        {
            baglanti.Open();
            string sqlCode = "UPDATE Kelimeler set Doğrular='0',Yanlışlar='0',Durum='Öğrenilmedi'";
            SQLiteCommand cmd = new SQLiteCommand(sqlCode, baglanti);
            cmd.ExecuteNonQuery();
            baglanti.Close();
        }

        public static int sqlRowCountWithCode(string SQLcode) 
        {
            baglanti.Open();
            SQLiteCommand cmd = new SQLiteCommand(SQLcode, baglanti);
            int toplamKelime = Convert.ToInt32(cmd.ExecuteScalar());
            baglanti.Close();
            return toplamKelime;

        }
    }
    
}
