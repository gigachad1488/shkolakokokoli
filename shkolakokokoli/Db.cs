using System;
using System.Collections.Generic;
using System.Data;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using shkolakokokoli.Models;

namespace shkolakokokoli;


public static class Db
{
    static bool s = true;
    static string const1 = "server = 10.10.1.24; uid=user_01;pwd=user01pro;database=pro1_6";
    static string const2 = "server = localhost; uid=user;pwd=qwerty228;database=world";

    private static MySqlConnection connection;
    private static MySqlConnection connection2;
    private static MySqlConnection connection3;
    private static MySqlConnection connection4;

    static Db()
    {
        if (s)
        {
            connection = new MySqlConnection(const1);
            connection2 = new MySqlConnection(const1);
            connection3 = new MySqlConnection(const1);
            connection4 = new MySqlConnection(const1);
        }
        else
        {
            connection = new MySqlConnection(const2);
            connection2 = new MySqlConnection(const2);
            connection3 = new MySqlConnection(const2);
            connection4 = new MySqlConnection(const2);
        }
        
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

    public static Client GetClientAt(int id)
    {
        if (connection2.State == ConnectionState.Closed)
        {
            connection2.Open();
        }

        MySqlCommand command = new MySqlCommand("select * from Client where id = @id", connection2);
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        Client client = new Client(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetDateTime(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));

        connection2.Close();

        return client;
    }

    public static void DeleteClient(Client client)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        
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
        command.Parameters.AddWithValue("@lal", client.languageLevel);
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

    public static Teacher GetTeacherAt(int id)
    {
        if (connection2.State == ConnectionState.Closed)
        {
            connection2.Open();
        }

        Teacher teacher = new Teacher();
        MySqlCommand command = new MySqlCommand("select * from Teacher where id = @id", connection2);
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        teacher.id = reader.GetInt32(0);
        teacher.firstName = reader.GetString(1);
        teacher.surName = reader.GetString(2);
        connection2.Close();
        return teacher;
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

    public static void DeleteTeacher(Teacher teacher)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }
        
        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        
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

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        
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

        List<Class> groups = new List<Class>();
        MySqlCommand command = new MySqlCommand("select * from Class", connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Class group = new Class();
            group.id = reader.GetInt32(0);
            group.name = reader.GetString(1);
            group.places = reader.GetInt32(2);
            group.course = GetCourseAt(reader.GetInt32(3));
            List<Client> clientsatclass = new List<Client>();
            connection4.Open();
            MySqlCommand ccommand = new MySqlCommand("select * from Client join Client_on_class on Client.id = Client_on_class.client_id join Class on Client_on_class.class_id = Class.id where class_id = @id", connection4);
            ccommand.Parameters.AddWithValue("@id", group.id);
            MySqlDataReader creader = ccommand.ExecuteReader();
            while (creader.Read())
            {
                Client c = GetClientAt(creader.GetInt32(0));
                clientsatclass.Add(c);
            }
            connection4.Close();
            group.clients = clientsatclass;
            groups.Add(group);
        }
        connection.Close();

        return groups;
    }


    public static void AddClass(Class group)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("INSERT INTO Class (name, places, course_id) VALUES (@n, @pl, @cr)", connection);
        command.Parameters.AddWithValue("@n", group.name);
        command.Parameters.AddWithValue("@pl", group.places);
        command.Parameters.AddWithValue("@cr", group.course.id);
        command.ExecuteNonQuery();
        command = new MySqlCommand("select last_insert_id() from Class", connection);
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        int clid = reader.GetInt32(0);
        reader.Close();
        MySqlCommand ccommand = new MySqlCommand("insert into Client_on_class (client_id, class_id) value (@cid, @clid)", connection);
        foreach (var item in group.clients)
        {
            ccommand.Parameters.AddWithValue("@clid", clid);
            ccommand.Parameters.AddWithValue("@cid", item.id);
            ccommand.ExecuteNonQuery();
            ccommand.Parameters.Clear();
        }
        connection.Close();
    }

    public static Class GetClassAt(int id)
    {
        connection4.Open();
        MySqlCommand command = new MySqlCommand("select * from Class where id = @id", connection4);
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();

        Class group = new Class();
        group.id = reader.GetInt32(0);
        group.name = reader.GetString(1);
        group.places = reader.GetInt32(2);
        group.course = GetCourseAt(reader.GetInt32(3));

        reader.Close();

        List<Client> clientsatclass = new List<Client>();
        MySqlCommand ccommand = new MySqlCommand("select * from Client join Client_on_class on Client.id = Client_on_class.client_id join Class on Client_on_class.class_id = Class.id where class_id = @id", connection4);
        ccommand.Parameters.AddWithValue("@id", group.id);
        MySqlDataReader creader = ccommand.ExecuteReader();
        while (creader.Read())
        {
            Client c = GetClientAt(creader.GetInt32(0));
            clientsatclass.Add(c);
        }
        connection4.Close();
        group.clients = clientsatclass;

        return group;
    }

    public static void ChangeClass(Class group)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("UPDATE Class SET name = @n, places = @pl, course_id = @cr WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", group.id);
        command.Parameters.AddWithValue("@n", group.name);
        command.Parameters.AddWithValue("@pl", group.places);
        command.Parameters.AddWithValue("@cr", group.course.id);
        command.ExecuteNonQuery();

        List<int> prevClientsId = new List<int>();
        command = new MySqlCommand("select * from Client_on_class where class_id = @id", connection);
        command.Parameters.AddWithValue("@id", group.id);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            prevClientsId.Add(reader.GetInt32(1));
        }

        reader.Close();

        command = new MySqlCommand("insert into Client_on_class (client_id, class_id) value (@cid, @clid)", connection);

        List<int> clientsToRemove = new List<int>();
        List<int> clientsToAdd = new List<int>();

        foreach (var student in group.clients)
        {
            if (prevClientsId.FindAll(x => x == student.id).Count <= 0)
            {
                command.Parameters.AddWithValue(@"clid", group.id);
                command.Parameters.AddWithValue("@cid", student.id);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                clientsToAdd.Add(student.id);
            }
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        
        command = new MySqlCommand("delete from Сlient_on_class where client_id = @cid and class_id = @clid", connection);

        foreach (var prevstudent in prevClientsId)
        {
            if (group.clients.FindAll(x => x.id == prevstudent).Count <= 0)
            {
                command.Parameters.AddWithValue(@"clid", group.id);
                command.Parameters.AddWithValue("@cid", prevstudent);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                clientsToRemove.Add(prevstudent);
            }
        }

        connection.Close();
    }

    public static void DeleteClass(Class group)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        MySqlCommand command = new MySqlCommand("DELETE FROM Client_on_class where class_id = @id", connection);
        command.Parameters.AddWithValue("@id", group.id);
        command.ExecuteNonQuery();

        command = new MySqlCommand("DELETE FROM Class WHERE id = @id", connection);
        //Console.WriteLine(group.id);
        command.Parameters.AddWithValue("@id", group.id);
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
            course.teacher = GetTeacherAt(tid);
            //Console.WriteLine("TECHER = " + course.teacher.firstName);

            int lid = reader.GetInt32(3);
            course.language = GetLanguageAt(lid);

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

    public static Course GetCourseAt(int id)
    {
        if (connection3.State == ConnectionState.Closed)
        {
            connection3.Open();
        }

        Course course = new Course();
        MySqlCommand commang = new MySqlCommand("select * from Course where id = @id", connection3);
        commang.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = commang.ExecuteReader();
        reader.Read();
        course.id = reader.GetInt32(0);
        course.name = reader.GetString(1);

        int tid = reader.GetInt32(2);
        course.teacher = GetTeacherAt(tid);
        //Console.WriteLine("TECHER = " + course.teacher.firstName);

        int lid = reader.GetInt32(3);
        course.language = GetLanguageAt(lid);

        course.price = reader.GetInt32(4);

        connection3.Close();
        return course;
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

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Course WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", course.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    #endregion

    #region Lessons

    public static List<Lesson> GetAllLessons()
    {
        connection.Open();

        List<Lesson> lessons = new List<Lesson>();
        MySqlCommand command = new MySqlCommand("select * from Lesson", connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Lesson lesson = new Lesson();
            lesson.id = reader.GetInt32(0);
            lesson.startTime = reader.GetDateTime(2);
            lesson.endTime = reader.GetDateTime(3);

            int cid = reader.GetInt32(1);
            lesson.group = GetClassAt(cid);
            lesson.attendances = GetGroupAttendanceAt(lesson.id);
            
            lessons.Add(lesson);
        }
        connection.Close();

        return lessons;
    }


    public static void AddLesson(Lesson lesson)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("insert into Lesson (class_id, time_start, time_end) VALUE (@clid, @times, @timee)", connection);
        command.Parameters.AddWithValue("@clid", lesson.group.id);
        command.Parameters.AddWithValue("@times", lesson.startTime);
        command.Parameters.AddWithValue("@timee", lesson.endTime);
        command.ExecuteNonQuery();
        
        command = new MySqlCommand("select LAST_INSERT_ID() from Lesson", connection);
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        int id = reader.GetInt32(0);
        reader.Close();
        
        command = new MySqlCommand("insert into Lessons_attendance (lesson_id, client_id, attendance) value (@lid, @cid, @at)", connection);
        foreach (var item in lesson.attendances)
        {
            command.Parameters.AddWithValue("@lid", id);
            command.Parameters.AddWithValue("@cid", item.client);
            command.Parameters.AddWithValue("@at", item.attendance);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        connection.Close();
    }

    /*
    public static Lesson GetCourseAt(int id)
    {
        if (connection3.State == ConnectionState.Closed)
        {
            connection3.Open();
        }

        Course course = new Course();
        MySqlCommand commang = new MySqlCommand("select * from Course where id = @id", connection3);
        commang.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = commang.ExecuteReader();
        reader.Read();
        course.id = reader.GetInt32(0);
        course.name = reader.GetString(1);

        int tid = reader.GetInt32(2);
        course.teacher = GetTeacherAt(tid);
        //Console.WriteLine("TECHER = " + course.teacher.firstName);

        int lid = reader.GetInt32(3);
        course.language = GetLanguageAt(lid);

        course.price = reader.GetInt32(4);

        connection3.Close();
        return course;
    }
    */

    public static void ChangeLesson(Lesson lesson)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand command = new MySqlCommand("update Lesson set class_id = @clid, time_start = @times, time_end = @timee where id = @id", connection);
        command.Parameters.AddWithValue("@clid", lesson.group.id);
        command.Parameters.AddWithValue("@times", lesson.startTime);
        command.Parameters.AddWithValue("@timee", lesson.endTime);
        command.Parameters.AddWithValue("@id", lesson.id);
        command.ExecuteNonQuery();
        
        command = new MySqlCommand("update Lessons_attendance set attendance = @at where lesson_id = @lid and client_id = @cid", connection);
        foreach (var item in lesson.attendances)
        {
            command.Parameters.AddWithValue("@lid", lesson.id);
            command.Parameters.AddWithValue("@cid", item.client);
            command.Parameters.AddWithValue("@at", item.attendance);
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        
        connection.Close();
    }

    public static void DeleteLesson(Lesson lesson)
    {
        if (connection.State == ConnectionState.Closed)
        {
            connection.Open();
        }

        MySqlCommand scommand = new MySqlCommand("set foreign_key_checks = 0", connection);
        scommand.ExecuteNonQuery();
        
        MySqlCommand command = new MySqlCommand("DELETE FROM Lesson WHERE id = @id", connection);
        command.Parameters.AddWithValue("@id", lesson.id);
        command.ExecuteNonQuery();

        command = new MySqlCommand("delete from Lessons_attendance where lesson_id = @id", connection);
        command.Parameters.AddWithValue("@id", lesson.id);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public static List<Attendance> GetGroupAttendanceAt(int id)
    {
        if (connection3.State == ConnectionState.Closed)
        {
            connection3.Open();
        }

        List<Attendance> attendances = new List<Attendance>();
        
        MySqlCommand command = new MySqlCommand("select * from Lessons_attendance where lesson_id = @id", connection3);
        command.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = command.ExecuteReader();
         while (reader.Read())
        {
            Attendance att = new Attendance();
            att.id = reader.GetInt32(0);
            att.lesson = reader.GetInt32(1);
            att.client = reader.GetInt32(2);
            att.attendance = reader.GetBoolean(3);
            attendances.Add(att);
        }
         connection3.Close();

         return attendances;
    }
    #endregion
}