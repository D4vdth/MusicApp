using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicApp.Database
{
    internal class MySqlCon
    {
        public MySqlConnection con;
        public string url;

        public MySqlCon()
        {
            this.con = new MySqlConnection();
            this.url = "server=localhost; port=3306; user=root; database=MusicApp; password=password";
        }


        public MySqlConnection getConection()
        {
            try
            {
                con.ConnectionString = url;
                con.Open();
                MessageBox.Show("The conection was successful!");
            }
            catch(MySqlException e) 
            {
                MessageBox.Show("Conection Error: "+e.Message);
            }

            return con; 
        }

        public void closeConection()
        {
            try
            {
                con.Close();
                MessageBox.Show("Desconection was successful!");

            }
            catch(MySqlException e)
            {
                MessageBox.Show("Desconection Error: " + e.Message);
            }
        }

    }
}
