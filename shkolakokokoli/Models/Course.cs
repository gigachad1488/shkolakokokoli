using System.Diagnostics;

namespace shkolakokokoli.Models;

public class Course
{
    public int id { get; set; }
    public string name { get; set; }

    public Teacher teacher;
    public Language language;

    public string Teacher
    {
        get
        {
            return teacher.ToString();
        }
    }

    public string Language
    {
        get
        {
            return language.ToString();
        }
    }

    public Course()
    {}
    public Course(int id, string name, Teacher teacher, Language language, int price)
    {
        this.id = id;
        this.name = name;
        this.teacher = teacher;
        this.language = language;
    }

    public override string ToString()
    {
        return name;
    }
}