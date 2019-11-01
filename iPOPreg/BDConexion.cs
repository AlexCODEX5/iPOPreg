using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using System.Windows;

namespace iPOPreg
{
    public class BDConexion
    {
        //Cada data reader se genera por cada metodo de operacion de base de datos
        //Cada table se define por la funcion
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=bd_ipopreg_iiee;";
        string query= "SELECT * FROM 'docente'";
        string table = "docente";

        public string CadenaTable()
        {
            return table;
        }

        public void SetCadenaTable(string newTable)
        {
            table = newTable;
        }

        public string CadenaConexion()
        {
            return connectionString;
        }

        public void SetCadenaConexion(string datasource, string port, string username, string password, string database)
        {
            connectionString = $"datasource={datasource};port={port};username={username};password={password};database={database};";
        }

        public void GetConect(MySqlConnection bdConnect)
        {
            try
            {
                bdConnect.Open();
                MessageBox.Show("Conexion exitosa");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DisConect(MySqlConnection bdConnect)
        {
            try
            {
                bdConnect.Close();
                MessageBox.Show("Conexion Cerrada Correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "La conexion no se cerró Correctamente");
            }
        }

        public MySqlDataReader VerifyUser(MySqlConnection bdConnect, string table, string Usuario, string Contraseña)
        {
            query = $"SELECT * FROM `{table}` WHERE Usuario = '{Usuario}' AND CONTRASEÑA = '{Contraseña}'";
            MySqlCommand commandDatabase = new MySqlCommand(query, bdConnect);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            reader = commandDatabase.ExecuteReader();
            /*string[] row = { reader.GetString(0),  };
            return row ;*/
            return reader;
        }

        public MySqlDataReader UserList(MySqlConnection bdConnect, string table)
        {
            query = $"SELECT `Nombre`,`Apellidos`,`ESTADO` FROM `{table}` WHERE ESTADO = 'A'";
            MySqlCommand commandDatabase = new MySqlCommand(query, bdConnect);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            reader = commandDatabase.ExecuteReader();
            return reader;
        }
    }
}
