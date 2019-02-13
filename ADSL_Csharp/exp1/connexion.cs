using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace exp1
{
    public partial class connexion : Form
    {
        public connexion()
        {
            InitializeComponent();
        }
        public static string typeuser = "";
        public static string nomuser = ""; 
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DateTime datedebut = new DateTime(2017,10,11,0,0,0);
            DateTime datefin = new DateTime(2019,10,11,0,0,0);


            int result = DateTime.Compare(datefin, DateTime.Now);
            //MessageBox.Show( result.ToString());
            string MyConString = "SERVER=localhost;DATABASE=adsl;UID=root;password=";
            MySqlConnection connection = new MySqlConnection(MyConString);
            MySqlCommand command = connection.CreateCommand();
            connection.Open();
            command.CommandText="select * from user where login='" + textBox1.Text + "' and password='" + textBox2.Text + "'";
            MySqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                if (reader.GetString("type") == "administrateur")
                {
                    typeuser = "administrateur";
                    
                }

                else
                {
                    typeuser = "technicien";
                }

                nomuser = reader.GetString("nom_prenom");
                

                MDIParent1 mdi = new MDIParent1();
                if (result<0)
                {
                    MessageBox.Show("logiciel expirer");
                }
                else
                {
                    MessageBox.Show("Bienvenu Dans ADSL");
                    mdi.Show();
                    this.Hide();
                } 


            }
            else
            {

                MessageBox.Show("Login ou mot de passe est incorrecte");
            }




            connection.Close();

        }

        private void connexion_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
             
        }
    }
}
