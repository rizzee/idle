using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace idle
{
    /// <summary>
    /// Логика взаимодействия для Rating.xaml
    /// </summary>
    public partial class Rating : Window
    {
        public Rating()
        {
            InitializeComponent();
            
        }

        static string connectionSring = @"Data Source=localhost;Initial Catalog=play_BD;Integrated Security=True";//указываем путь к базе
        SqlConnection sqlConnection = new SqlConnection(connectionSring);//присваиваем имя

        void rayt()
        {
            using (SqlConnection connection = new SqlConnection(connectionSring))
            {
                connection.Open();
                string query = "SELECT TOP 10 Money, Login FROM Table_1 ORDER BY Money DESC";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        lt1.Items.Clear();

                        while (reader.Read())
                        {
                            string moneyValue = reader.GetString(0); 
                            string loginValue = reader.GetString(1); 

                            string itemText = $"{moneyValue} : {loginValue}";
                            if (loginValue == MainWindow.reit)
                            { itemText += "- " + "это вы"; }

                            lt1.Items.Add(itemText);
                        }
                    }
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            rayt();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Show();
        }
    }
}
