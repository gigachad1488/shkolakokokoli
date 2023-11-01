using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using shkolakokokoli.Models;

namespace shkolakokokoli;


public static class Db
{
    private static MySqlConnection connection;

    static Db()
    {
        connection = new MySqlConnection("server=10.10.1.24;uid=user_01;pwd=user01pro;database=pro1_6");
    }

    #region Clients
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
        MySqlCommand command = new MySqlCommand("INSERT INTO Client (firstname, surname, phone, birthday, last_language, language_level, language_needs) VALUES (@fn, @sn, @ph, @dt, @ll, @lal, @ln)", connection);
        command.Parameters.AddWithValue("@fn", client.firstName);
        command.Parameters.AddWithValue("@sn", client.surName);
        command.Parameters.AddWithValue("@ph", client.phone);
        command.Parameters.AddWithValue("@dt", client.birthday);
        command.Parameters.AddWithValue("@ll", client.lastLanguage);
        command.Parameters.AddWithValue("@lal", client.languageLevel);
        command.Parameters.AddWithValue("@ln", client.languageNeeds);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void DeleteClient(Client client)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("DELETE FROM Client WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", client.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void ChangeClient(Client client)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("UPDATE Client SET firstname = @fn, surname = @sn, phone = @ph, birthday = @dt, last_language = @ll, language_level = @lal, language_needs = @ln WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", client.id);
        command.Parameters.AddWithValue("@fn", client.firstName);
        command.Parameters.AddWithValue("@sn", client.surName);
        command.Parameters.AddWithValue("@ph", client.phone);
        command.Parameters.AddWithValue("@dt", client.birthday);
        command.Parameters.AddWithValue("@ll", client.lastLanguage);
        command.Parameters.AddWithValue("@lal",client.languageLevel);
        command.Parameters.AddWithValue("@ln", client.languageNeeds);
        command.ExecuteNonQuery();
        connection.Close();
    }
    #endregion

    #region Teachers

    public static List<Teacher> GetAllTeachers()
    {
        List<Teacher> teachers = new List<Teacher>();
        connection.Open();
        MySqlCommand commang = new MySqlCommand("select * from Teacher", connection);
        MySqlDataReader reader = commang.ExecuteReader();
        while (reader.Read())
        {
            teachers.Add(new Teacher(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
        }
        connection.Close();
        
        return teachers;
    }
    
    
    public static void AddTeacher(Teacher teacher)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("INSERT INTO Teacher (firstname, surname) VALUES (@fn, @sn)", connection);
        command.Parameters.AddWithValue("@fn", teacher.firstName);
        command.Parameters.AddWithValue("@sn", teacher.surName);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void ChangeTeacher(Teacher teacher)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("UPDATE Teacher SET firstname = @fn, surname = @sn WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", teacher.id);
        command.Parameters.AddWithValue("@fn", teacher.firstName);
        command.Parameters.AddWithValue("@sn", teacher.surName);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void DeleteTeacher(Teacher teacher)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("DELETE FROM Teacher WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", teacher.id);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    #endregion

    #region Language

    public static List<Language> GetAllLanguages()
    {
        List<Language> languages = new List<Language>();
        connection.Open();
        MySqlCommand commang = new MySqlCommand("select * from Language", connection);
        MySqlDataReader reader = commang.ExecuteReader();
        while (reader.Read())
        {
            languages.Add(new Language(reader.GetInt32(0), reader.GetString(1)));
        }
        connection.Close();
        
        return languages;
    }
    
    
    public static void AddLanguage(Language language)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("INSERT INTO Language (name) VALUES (@fn)", connection);
        command.Parameters.AddWithValue("@fn", language.name);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void ChangeLanguage(Language language)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("UPDATE Language SET name = @fn WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", language.id);
        command.Parameters.AddWithValue("@fn", language.name);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void DeleteLanguage(Language language)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("DELETE FROM Language WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", language.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion

    #region Classes

    public static List<Class> GetAllClasses()
    {
        List<Class> classes = new List<Class>();
        connection.Open();
        MySqlCommand commang = new MySqlCommand("select * from Language", connection);
        MySqlDataReader reader = commang.ExecuteReader();
        while (reader.Read())
        {
            //languages.Add(new Language(reader.GetInt32(0), reader.GetString(1)));
        }
        connection.Close();
        
        return classes;
    }
    
    
    public static void AddClass(Class language)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("INSERT INTO Language (name) VALUES (@fn)", connection);
        command.Parameters.AddWithValue("@fn", language.name);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void ChangeClass(Class language)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("UPDATE Language SET name = @fn WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", language.id);
        command.Parameters.AddWithValue("@fn", language.name);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void DeleteClass(Class language)
    {
        connection.Open();
        MySqlCommand command = new MySqlCommand("DELETE FROM Language WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", language.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion
}