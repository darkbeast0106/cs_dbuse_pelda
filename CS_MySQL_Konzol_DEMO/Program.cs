﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_MySQL_Konzol_DEMO
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connstring = "Server=localhost;Database=cs_pelda_db;Uid=root;Password=;";
                var conn = new MySqlConnection(connstring);
                conn.Open();
                string sql = "SELECT * FROM felhasznalok ORDER BY user LIMIT 2";

                var command = conn.CreateCommand();
                command.CommandText = sql;
                //MySqlCommand command = new MySqlCommand(sql, conn); 
                /*
                 * Mindkét megoldás ugyan azt eredményezi.
                 */

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = (int)reader["id"];
                    string user = (string)reader["user"];
                    string pass = (string)reader["pwd"];

                    Console.WriteLine("{0}. Felhasználónév: {1}  Jelszó: {2}", id, user, pass);
                }

                reader.Close();

                command.CommandText = "SELECT MIN(id) FROM felhasznalok";
                int min = (int)command.ExecuteScalar();
                command.CommandText = "SELECT MAX(id) FROM felhasznalok";
                int max = (int)command.ExecuteScalar();

                Random rnd = new Random();

                int rid = rnd.Next(min, max + 1);

                command.CommandText = "SELECT * FROM felhasznalok WHERE id = @id";
                command.Parameters.AddWithValue("@id", rid);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string user = (string)reader["user"];
                    string pass = (string)reader["pwd"];

                    Console.WriteLine("Felhasználónév: {0}  Jelszó: {1}",user, pass);
                }
                reader.Close();

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: "+ex.Message);
            }

            Console.ReadLine();
        }
    }
}
