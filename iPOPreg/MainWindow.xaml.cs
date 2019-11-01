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
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace iPOPreg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Salir_Login_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimizar_Login_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        //Se copia la base de datos de docentes y se le puede cambiar por responsables
        //Cada cadena se obtiene por el data set por defecto que genera el Login
        private void Verificar_Login_Click(object sender, RoutedEventArgs e)
        {
            BDConexion verifyUser = new BDConexion();
            MySqlConnection verifyUserCon = new MySqlConnection(verifyUser.CadenaConexion());//Conexion

            try
            {
                verifyUserCon.Open();
                MessageBox.Show("La base de datos se conecto correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            if (verifyUserCon.State == ConnectionState.Open)
            {
                MySqlDataReader reader;
                verifyUser.SetCadenaTable("docente");
                reader = verifyUser.VerifyUser(verifyUserCon, verifyUser.CadenaTable(), User_Login.Text, Psw_Login.Password);
                if (reader.HasRows)
                {
                    MessageBox.Show("Login Correcto", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Verificar_Login.Content = "Acceso";
                    Panel_control panelControl = new Panel_control();
                    panelControl.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos, verifique si esta registrado");
                }
            }

            verifyUserCon.Close();
            MySqlConnection.ClearPool(verifyUserCon);
        }

        private void Panel_Login_Loaded(object sender, RoutedEventArgs e)
        {
            User_Login.Focus();
            BDConexion verifyUser = new BDConexion();
            verifyUser.SetCadenaConexion("127.0.0.1", "3306", "root", "", "bd_ipopreg_iiee");
            verifyUser.SetCadenaTable("docente");
            MySqlConnection UserList = new MySqlConnection(verifyUser.CadenaConexion());
            try
            {
                UserList.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UserList.Close();
        }
        

        private void RefreshList_Login_Click(object sender, RoutedEventArgs e)
        {
            BDConexion verifyUser = new BDConexion();
            MySqlConnection UserList = new MySqlConnection(verifyUser.CadenaConexion());
            try
            {
                UserList.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UserList.Close();
        }
    }
}
