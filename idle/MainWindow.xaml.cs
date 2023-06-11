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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;

namespace idle
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        authorization att = new authorization();
        string hashedPassword;
        SqlConnection sqlConnection;
        game game = new game();
        public static string reit;

        public MainWindow()
        {
            InitializeComponent();

            

        }

        void con()
        {
            hashing();
            string connectionString = @"Data Source=localhost;Initial Catalog=play_BD;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            SqlDataReader sqlDataReader = null;
            SqlCommand command = new SqlCommand("Select Id From [Table_1] WHERE Login = @Login AND Pass = @Pass", sqlConnection);

            reit = tb1.Text;

            bool success = false;
            try
            {
                command.Parameters.AddWithValue("@Login", tb1.Text);
                command.Parameters.AddWithValue("@Pass", hashedPassword);
                sqlConnection.Open();

                using (sqlDataReader = command.ExecuteReader())
                {
                    success = sqlDataReader.Read();
                }
                if (success)
                {
                    authorization.id = Convert.ToInt32(command.ExecuteScalar().ToString());
                    game.Owner = this;
                    game.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
            finally
            {
                sqlConnection.Close();
            }



        }

        void hashing()
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(tb2.Password.ToString());
            byte[] hash = sha256.ComputeHash(bytes);
            hashedPassword = Convert.ToBase64String(hash);
        }

       


        private void logchanged(object sender, RoutedEventArgs e)
        {
            if (tb1.Text.Length > 0)
            {
                log.Visibility = Visibility.Collapsed;
            }
            else
            {
                log.Visibility = Visibility.Visible;
            }
        }


        private void passchanged(object sender, RoutedEventArgs e)
        {
            if (tb2.Password.Length > 0)
            {
                pass.Visibility = Visibility.Collapsed;
            }
            else
            {
                pass.Visibility = Visibility.Visible;
            }
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            con();
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            hashing();
            string connectionString = @"Data Source=localhost;Initial Catalog=play_BD;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);


            SqlCommand command = new SqlCommand("INSERT INTO [Table_1] (Pass, Login, Ur_d_1, Ur_d_2, Ur_d_3, Ur_d_4, Ur_d_5, Ur_d_6, Ur_d_7, Money)VALUES(@Pass, @Login, @Ur_d_1, @Ur_d_2, @Ur_d_3, @Ur_d_4, @Ur_d_5, @Ur_d_6, @Ur_d_7, @Money)", sqlConnection);
            sqlConnection.Open();
            command.Parameters.AddWithValue("Pass", hashedPassword);

            command.Parameters.AddWithValue("Login", tb1.Text);

            command.Parameters.AddWithValue("Ur_d_1", 1);
            command.Parameters.AddWithValue("Ur_d_2", 0);
            command.Parameters.AddWithValue("Ur_d_3", 0);
            command.Parameters.AddWithValue("Ur_d_4", 0);
            command.Parameters.AddWithValue("Ur_d_5", 0);
            command.Parameters.AddWithValue("Ur_d_6", 0);
            command.Parameters.AddWithValue("Ur_d_7", 0);
            command.Parameters.AddWithValue("Money", 0);

            command.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}
