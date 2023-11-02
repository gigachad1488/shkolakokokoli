namespace shkolakokokoli.Models;

public class Course
{
    public int id { get; set; }
    public string name { get; set; }
    public Teacher teacher { get; set; }
    public Language language { get; set; }
    public int price { get; set; }

    public Course()
    {}
    public Course(int id, string name, Teacher teacher, Language language, int price)
    {
        this.id = id;
        this.name = name;
        this.teacher = teacher;
        this.language = language;
        this.price = price;
    }

    public override string ToString()
    {
        return name;
    }
}