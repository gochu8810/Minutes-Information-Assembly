using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*
●Visual StudioとMySQLを接続するプログラムを作成するには
ソリューションエクスプローラー
	「参照」を右クリックー「参照の追加...」ー「アセンブリ」－「フレームワーク」を選択
	
	「MySql.Data」にチェックを入れて「OK」ボタンを押す
			…	表示されるフレームワークの数が多すぎるので
				検索ボックスに「mysql」と入力するとはやい
*/

// MySQLを使うため
using MySql.Data.MySqlClient;

namespace MySQLcomponents
{
    class MySQLConnect
    {
        // MySQLへの接続情報
        const string server = "35.200.36.4";        // MySQLサーバホスト名
        const string user = "fit_okutani";          // MySQLユーザ名
        const string pass = "zips0321";             // MySQLパスワード
        const string database = "fit_okutani";      // 接続するデータベース名
        

        public string Sstring{ get; set; }

        /*MySQLデータベースへ接続
        string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();*/


        //Select文を代入して、SQLで取得し2次配列のListで返す
        public List<List<string>> SelectSQL2(string SQLstring)
        {
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand selectCommand = new MySqlCommand(SQLstring, connection);
            MySqlDataReader results = selectCommand.ExecuteReader();

            var Datalist2 = new List<List<string>>();
            Datalist2.Add(new List<string>());

            // 1レコード毎にDatalist(List)に代入
            while (results.Read()) {
                int fc = results.FieldCount;
                List<string> dlist = new List<string>();
                
                for (int i = 0; i<=fc-1; i++)
                {
                  dlist.Add(results.GetString(i));
                }
                Datalist2.Add(dlist);
            }

            Datalist2.RemoveAt(0);
            results.Close();
            connection.Close();
            return Datalist2;
        }

        //Select文を代入して、SQLで取得し1次配列のListで返す
        public List<string> SelectSQL1(string SQLstring)
        {
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand selectCommand = new MySqlCommand(SQLstring, connection);
            MySqlDataReader results = selectCommand.ExecuteReader();

            var Datalist1 = new List<string>();

            // 1行ごとにMinutelistに代入
            while (results.Read()) {
                Datalist1.Add(results.GetString(0));
            }
            results.Close();
            connection.Close();
            return Datalist1;
        }


        //MySQLデータベースのInsert, Update,Deleteを行う
        public void SQLWrite(string SQLstring)
        {
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, database, user, pass);
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // トランザクションを開始します。
            MySqlTransaction transaction = connection.BeginTransaction();
            MySqlCommand Writecommand = new MySqlCommand(SQLstring, connection);

            try
            {
                Writecommand.Transaction = transaction;
                Writecommand.ExecuteNonQuery();
                //トランザクションをコミットします。
                transaction.Commit();
            }
            catch (System.Exception)
            {
                //トランザクションをロールバックします。
                transaction.Rollback();
                throw;
            }
            finally
            {
                connection.Close();
            }

        }

    }

    class SQLstringBuilder : MySQLConnect
    {
        public string TableName;
        public SQLstringBuilder(string TableName)
        {
            this.TableName = TableName;
        }

        string strSELECT()
        {
            MySQLConnect strSQL = new MySQLConnect();
            strSQL.SQLstring = "SELECT * FROM " + TableName;
            return strSQL.SQLstring;
        }

        string strWHERE(string ID, string targetID)
        {
            MySQLConnect strSQL = new MySQLConnect();
            strSQL.SQLstring = " WHERE " + ID + "=" + targetID;
            return strSQL.SQLstring;
        }

        string strAnd(string ID, string targetID)
        {
            MySQLConnect strSQL = new MySQLConnect();
            strSQL.SQLstring = " AND " + ID + "=" + targetID;
            return strSQL.SQLstring;
        }

        string strBETWEEN(string columnName, string targetlow , string targethigh)
        {
            MySQLConnect strSQL = new MySQLConnect();
            strSQL.SQLstring = " BETWEEN " + columnName + "=" + targetlow + " TO " + targethigh;
            return strSQL.SQLstring;
        }

        string Writestring()
        {
            MySQLConnect strSQL = new MySQLConnect();
            strSQL.
            string columns = "";
            string setvalues = setArray;
            strSQL.SQLstring = "INSERT INTO " + TableName + "(" + columns + ")" VALUE (" + setvalues + ")";
            return strSQL.SQLstring;
        }



    }
}