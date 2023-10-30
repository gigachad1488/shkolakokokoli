using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace shkolakokokoli;


public static class Db
{
    private static MySqlConnection connection;

    static Db()
    {
        connection = new MySqlConnection("server=10.10.1.24;uid=user_01;pwd=user01pro;database=pro1_6");
    }

    public static List<Client> GetAllClients()
    {
        List<Client> clients = new List<Client>();
        connection.Open();
        MySqlCommand commang = new MySqlCommand("select * from Client", connection);
        MySqlDataReader reader = commang.ExecuteReader();
        while (reader.Read())
        {
            clients.Add(new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetString(7)));
        }
        connection.Close();
        
        return clients;
    }

    public static void AddClient(Client client)
    {
        connection.Open();
        MySqlCommand commang = new MySqlCommand("INSERT INTO Client (firstname, surname, phone, birthday, last_language, language_level, language_needs) VALUES (@fn, @sn, @ph, @dt, @ll, @lal, @ln)", connection);
        commang.Parameters.AddWithValue("@fn", client.firstname);
        commang.Parameters.AddWithValue("@sn", client.surName);
        commang.Parameters.AddWithValue("@ph", client.phone);
        commang.Parameters.AddWithValue("@dt", client.birthday);
        commang.Parameters.AddWithValue("@ll", client.lastLanguage);
        commang.Parameters.AddWithValue("@lal", client.languageLevel);
        commang.Parameters.AddWithValue("@ln", client.languageNeeds);
        commang.ExecuteNonQuery();
        connection.Close();
    }
}