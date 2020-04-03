using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySQLcomponents;

namespace Minutes_Information_Assembly
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySQLConnect data = new MySQLConnect();
            //data.SQLstring = "SHOW COLUMNS FROM MinuteTable";
            data.SQLstring = "SELECT* FROM MinuteTable WHERE MinuteNo=1";
            List<List<string>> dataList = new List<List<string>>();
            //List<string> dataList = new List<string>();
            dataList = data.SelectSQL2(data.SQLstring);
            
            MessageBox.Show(string.Join(",", dataList));
           // data.SQLWrite();
        }
    }
   
    class TEST
    {
        String message;
        TEST(String message)
        {
            this.message = message;

        }
        void Write()
        {
            MessageBox.Show("WriteString");
        }
    }



}
