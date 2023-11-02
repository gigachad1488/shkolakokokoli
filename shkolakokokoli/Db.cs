using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using shkolakokokoli.Models;

namespace shkolakokokoli;


public static class Db
{
    private static MySqlConnection connection;
    private static MySqlConnection connection2;

    static Db()
    {
        connection = new MySqlConnection("server=10.10.1.24;uid=user_01;pwd=user01pro;database=pro1_6");
        connection2 = new MySqlConnection("server=10.10.1.24;uid=user_01;pwd=user01pro;database=pro1_6");
    }

    #region Clients
    public static List<Client> GetAllClients()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        List<Client> clients = new List<Client>();
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
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
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
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Client WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", client.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static void ChangeClient(Client client)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
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
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        List<Teacher> teachers = new List<Teacher>();
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
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("INSERT INTO Teacher (firstname, surname) VALUES (@fn, @sn)", connection);
        command.Parameters.AddWithValue("@fn", teacher.firstName);
        command.Parameters.AddWithValue("@sn", teacher.surName);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void ChangeTeacher(Teacher teacher)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("UPDATE Teacher SET firstname = @fn, surname = @sn WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", teacher.id);
        command.Parameters.AddWithValue("@fn", teacher.firstName);
        command.Parameters.AddWithValue("@sn", teacher.surName);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static Teacher GetTeacherAt(int id)
    {
        if (connection2.State == ConnectionState.Closed)
        {
            connection2.Open();
        }

        Teacher teacher = new Teacher();
        MySqlCommand commang = new MySqlCommand("select * from Teacher where id = @id", connection2);
        commang.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = commang.ExecuteReader();
        reader.Read();
        teacher.id = reader.GetInt32(0);
        teacher.firstName = reader.GetString(1);
        teacher.surName = reader.GetString(2);
        connection2.Close();
        return teacher;
    }
    
    public static void DeleteTeacher(Teacher teacher)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Teacher WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", teacher.id);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    #endregion

    #region Language

    public static List<Language> GetAllLanguages()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        List<Language> languages = new List<Language>();
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
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("INSERT INTO Language (name) VALUES (@fn)", connection);
        command.Parameters.AddWithValue("@fn", language.name);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static Language GetLanguageAt(int id)
    {
        if (connection2.State == ConnectionState.Closed)
        {
            connection2.Open();
        }
        
        Language language = new Language();
        MySqlCommand commang = new MySqlCommand("select * from Language where id = @id", connection2);
        commang.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = commang.ExecuteReader();
        reader.Read();
        language.id = reader.GetInt32(0);
        language.name = reader.GetString(1);
        connection2.Close();
        return language;
    }
    
    public static void ChangeLanguage(Language language)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("UPDATE Language SET name = @fn WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", language.id);
        command.Parameters.AddWithValue("@fn", language.name);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void DeleteLanguage(Language language)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Language WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", language.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion

    #region Classes

    public static List<Class> GetAllClasses()
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        List<Class> classes = new List<Class>();
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
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("INSERT INTO Language (name) VALUES (@fn)", connection);
        command.Parameters.AddWithValue("@fn", language.name);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void ChangeClass(Class language)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("UPDATE Language SET name = @fn WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", language.id);
        command.Parameters.AddWithValue("@fn", language.name);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void DeleteClass(Class language)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Language WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", language.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion

    #region Courses

    public static List<Course> GetAllCourses()
    {       
        connection.Open();  
        
        List<Course> courses = new List<Course>();
        MySqlCommand command = new MySqlCommand("select * from Course", connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Course course = new Course();
            course.id = reader.GetInt32(0);
            course.name = reader.GetString(1);
            
            int tid = reader.GetInt32(2);
            //course.teacher = GetTeacherAt(tid);
            Console.WriteLine("TECHER = " + course.teacher.firstName);

            int lid = reader.GetInt32(3);
            //course.language = GetLanguageAt(lid);

            course.price = reader.GetInt32(4);
            
            courses.Add(course);
        }
        connection.Close();
        
        return courses;
    }
    
    
    public static void AddCourse(Course course)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("INSERT INTO Course (name, teacher_id, language_id, price) VALUES (@n, @ti, @li, @pr)", connection);
        command.Parameters.AddWithValue("@n", course.name);
        command.Parameters.AddWithValue("@ti", course.teacher.id);
        command.Parameters.AddWithValue("@li", course.language.id);
        command.Parameters.AddWithValue("@pr", course.price);
        Console.Write(course.name);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void ChangeCourse(Course course)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("UPDATE Course SET name = @n, teacher_id = @ti, language_id = @li, price = @pr WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", course.id);
        command.Parameters.AddWithValue("@n", course.name);
        command.Parameters.AddWithValue("@ti", course.teacher.id);
        command.Parameters.AddWithValue("@li", course.language.id);
        command.Parameters.AddWithValue("@pr", course.price);
        command.ExecuteNonQuery();
        connection.Close();
    }
    
    public static void DeleteCourse(Course course)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Course WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", course.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion
}