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
using System.Windows.Threading;
using System.Data.SqlClient;

namespace idle
{
    /// <summary>
    /// Логика взаимодействия для game.xaml
    /// </summary>
    public partial class game : Window
    {
        static string connectionSring = @"Data Source=localhost;Initial Catalog=play_BD;Integrated Security=True";//указываем путь к базе
        SqlConnection sqlConnection = new SqlConnection(connectionSring);//присваиваем имя

        public string id1;
        
        private int _ticksCount = 0;//задаём тик 
        private const int EarningsInterval1 = 1; 
        private const int EarningsInterval2 = 3; //задаём интервал
        private EarningsManager _earningsManager1;//вызываем перменные для счёта
        private EarningsManager _earningsManager2;
        private EarningsManager _earningsManager3;
        private EarningsManager _earningsManager4;
        private EarningsManager _earningsManager5;
        private EarningsManager _earningsManager6;
        private EarningsManager _earningsManager7;
        double intia;
        DispatcherTimer gameArc = new DispatcherTimer();//создаём таймер

        public game()
        {
            InitializeComponent();
            _earningsManager1 = new EarningsManager(1.0);//представляем переменные как коэффициент
            _earningsManager2 = new EarningsManager(1.0);
            _earningsManager3 = new EarningsManager(1.0);
            _earningsManager4 = new EarningsManager(1.0);
            _earningsManager5 = new EarningsManager(1.0);
            _earningsManager6 = new EarningsManager(1.0);
            _earningsManager7 = new EarningsManager(1.0);
            
            

        }

        private bool _building2Purchased = false;//проверка для покупки
        private bool _building3Purchased = false;
        private bool _building4Purchased = false;
        private bool _building5Purchased = false;
        private bool _building6Purchased = false;
        private bool _building7Purchased = false;

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _ticksCount++;//прибавляем тик

            double earnings1 = _earningsManager1.GetEarnings(0.1); //задаём коэф заработка
            double earnings2 = _building2Purchased ? _earningsManager2.GetEarnings(0.2) : 0.0;
            double earnings3 = _building3Purchased ? _earningsManager3.GetEarnings(0.3) : 0.0;
            double earnings4 = _building4Purchased ? _earningsManager4.GetEarnings(0.4) : 0.0;
            double earnings5 = _building5Purchased ? _earningsManager2.GetEarnings(0.5) : 0.0;
            double earnings6 = _building6Purchased ? _earningsManager3.GetEarnings(0.6) : 0.0;
            double earnings7 = _building7Purchased ? _earningsManager4.GetEarnings(0.6) : 0.0;
            update();//метод загрузки в базу
            

            double totalEarnings =  intia + earnings1 + earnings2 + earnings3 + earnings4 + earnings5 + earnings6 + earnings7;//счёт общего заработка
            lb_money.Content = totalEarnings.ToString("0.0");//вывод с ограничением
          

        }

        void loading()
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=play_BD;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            SqlDataReader sqlDataReader = null;
            SqlCommand command = new SqlCommand("Select Ur_d_1, Ur_d_2, Ur_d_3, Ur_d_4, Ur_d_5, Ur_d_6, Ur_d_7, Money From [Table_1] WHERE Id=@Id", sqlConnection);
            bool success = false;
            try
            {
                command.Parameters.AddWithValue("@Id", authorization.id);

                sqlConnection.Open();

                id1 = Convert.ToString(authorization.id);

                using (sqlDataReader = command.ExecuteReader())
                {
                    success = sqlDataReader.Read();
                    Dom.lvldom.Add(Convert.ToInt32(sqlDataReader["Ur_d_1"]));
                    Dom.lvldom.Add(Convert.ToInt32(sqlDataReader["Ur_d_2"]));
                    Dom.lvldom.Add(Convert.ToInt32(sqlDataReader["Ur_d_3"]));
                    Dom.lvldom.Add(Convert.ToInt32(sqlDataReader["Ur_d_4"]));
                    Dom.lvldom.Add(Convert.ToInt32(sqlDataReader["Ur_d_5"]));
                    Dom.lvldom.Add(Convert.ToInt32(sqlDataReader["Ur_d_6"]));
                    Dom.lvldom.Add(Convert.ToInt32(sqlDataReader["Ur_d_7"]));
                    intia = Convert.ToDouble(sqlDataReader["Money"]);
                }
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        void update()
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=play_BD;Integrated Security=True";
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open(); // Open the connection

                string query = "UPDATE [Table_1] SET Ur_d_1 = @Ur_d_1, Ur_d_2 = @Ur_d_2, Ur_d_3 = @Ur_d_3, Ur_d_4 = @Ur_d_4, Ur_d_5 = @Ur_d_5, Ur_d_6 = @Ur_d_6, Ur_d_7 = @Ur_d_7, Money = @Money WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, sqlConnection))
                {
                    command.Parameters.AddWithValue("@Ur_d_1", lvl1.Content);
                    command.Parameters.AddWithValue("@Ur_d_2", lvl2.Content);
                    command.Parameters.AddWithValue("@Ur_d_3", lvl3.Content);
                    command.Parameters.AddWithValue("@Ur_d_4", lvl4.Content);
                    command.Parameters.AddWithValue("@Ur_d_5", lvl5.Content);
                    command.Parameters.AddWithValue("@Ur_d_6", lvl6.Content);
                    command.Parameters.AddWithValue("@Ur_d_7", lvl7.Content);
                    command.Parameters.AddWithValue("@Money", lb_money.Content);
                    command.Parameters.AddWithValue("@Id", authorization.id);

                    int rowsAffected = command.ExecuteNonQuery();
                }

               
            }
        }

        private void bt_ex_Click(object sender, RoutedEventArgs e)
        {
            Owner.Close();
        }

        private void bt1_Click(object sender, RoutedEventArgs e)
        {
            double level1 = double.Parse(lvl1.Content.ToString());
            double costOfImprovement1 = level1 * 1.0;


            if (level1 < 20.0 && _earningsManager1.CanBuyImprovement(costOfImprovement1))
            {
                _earningsManager1.SetLevel(level1 + 1.0);
                _earningsManager1.BuyImprovement(costOfImprovement1);
                lvl1.Content = (level1 + 1.0).ToString();
            }
            else
            {
                level1 = 20.0;
            }
        }

        private void bt2_Click(object sender, RoutedEventArgs e)
        {
            double level1 = double.Parse(lvl1.Content.ToString());
            double level2 = double.Parse(lvl2.Content.ToString());
            double costOfBuilding2 = level2 * 2.0; 

            if (level1 >= 5 && _earningsManager1.CanBuyImprovement(costOfBuilding2))
            {
                _earningsManager1.BuyImprovement(costOfBuilding2);
                level2++;
                lvl2.Content = level2.ToString();
                _building2Purchased = true; 
            }
            else
            {
                level2 = 20;
            }
        }

        private void bt3_Click(object sender, RoutedEventArgs e)
        {
            double level2 = double.Parse(lvl2.Content.ToString());
            double level3 = double.Parse(lvl3.Content.ToString());
            double costOfBuilding3 = level3 * 3.0;

            if (level2 >= 5 && _earningsManager1.CanBuyImprovement(costOfBuilding3))
            {
                _earningsManager1.BuyImprovement(costOfBuilding3);
                level3++;
                lvl3.Content = level3.ToString();
                _building2Purchased = true;
            }
            else
            {
                level3 = 20;
            }
        }

        private void bt4_Click(object sender, RoutedEventArgs e)
        {
            double level3 = double.Parse(lvl3.Content.ToString());
            double level4 = double.Parse(lvl4.Content.ToString());
            double costOfBuilding4 = level4 * 4.0;

            if (level3 >= 5 && _earningsManager1.CanBuyImprovement(costOfBuilding4))
            {
                _earningsManager1.BuyImprovement(costOfBuilding4);
                level4++;
                lvl4.Content = level4.ToString();
                _building2Purchased = true;
            }
            else
            {
                level4 = 20;
            }
        }

        private void bt5_Click(object sender, RoutedEventArgs e)
        {
            double level4 = double.Parse(lvl4.Content.ToString());
            double level5 = double.Parse(lvl5.Content.ToString());
            double costOfBuilding5 = level5 * 5.0;

            if (level4 >= 5 && _earningsManager1.CanBuyImprovement(costOfBuilding5))
            {
                _earningsManager1.BuyImprovement(costOfBuilding5);
                level5++;
                lvl5.Content = level5.ToString();
                _building2Purchased = true;
            }
            else
            {
                level5 = 20;
            }
        }

        private void bt6_Click(object sender, RoutedEventArgs e)
        {
            double level5 = double.Parse(lvl5.Content.ToString());
            double level6 = double.Parse(lvl6.Content.ToString());
            double costOfBuilding6 = level6 * 3.0;

            if (level5 >= 5 && _earningsManager1.CanBuyImprovement(costOfBuilding6))
            {
                _earningsManager1.BuyImprovement(costOfBuilding6);
                level6++;
                lvl6.Content = level6.ToString();
                _building2Purchased = true;
            }
            else
            {
                level6 = 20;
            }
        }

        private void bt7_Click(object sender, RoutedEventArgs e)
        {
            double level6 = double.Parse(lvl6.Content.ToString());
            double level7 = double.Parse(lvl7.Content.ToString());
            double costOfBuilding7 = level7 * 3.0;

            if (level6 >= 5 && _earningsManager1.CanBuyImprovement(costOfBuilding7))
            {
                _earningsManager1.BuyImprovement(costOfBuilding7);
                level7++;
                lvl7.Content = level7.ToString();
                _building2Purchased = true;
            }
            else
            {
                level7 = 20;
            }
        }

        void LoadDataToLabels()
        {
            if (Dom.lvldom.Count >= 7)
            {
                lvl1.Content = Dom.lvldom[0].ToString();
                lvl2.Content = Dom.lvldom[1].ToString();
                lvl3.Content = Dom.lvldom[2].ToString();
                lvl4.Content = Dom.lvldom[3].ToString();
                lvl5.Content = Dom.lvldom[4].ToString();
                lvl6.Content = Dom.lvldom[5].ToString();
                lvl7.Content = Dom.lvldom[6].ToString();
                
            }
        }


        private void Window_Activated(object sender, EventArgs e)
        {
            loading();
            gameArc.Tick += new EventHandler(dispatcherTimer_Tick);
            gameArc.Interval = new TimeSpan(0, 0, 1);
            gameArc.Start();
            LoadDataToLabels();
        }

        private void bt_rt_Click(object sender, RoutedEventArgs e)
        {
            Rating rayt = new Rating();
            rayt.Owner = this;
            rayt.Show();
            this.Hide();
        }

        private void bt_del_Click(object sender, RoutedEventArgs e)
        {
            gameArc.Stop();
            using (SqlConnection connection = new SqlConnection(connectionSring))//удаление
            {
                string idText = id1;
                
                connection.Open();
                string query = "DELETE FROM [Table_1] WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id1);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Close();
                        Owner.Show();
                        MessageBox.Show("Запись успешно удалена.");
                       
                       
                    }
                    else
                    {
                        MessageBox.Show("Запись не найдена.");
                        gameArc.Start();
                    }
                }
            }
        }
    }
}
