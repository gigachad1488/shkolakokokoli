using System.Collections.Generic;
using DynamicData;

namespace shkolakokokoli.Models;

public class Class
{
    public int id { get; set; }
    public string name { get; set; }
    
    public Course course;
    
    public string Course
    {
        get
        {
            return course.ToString();
        }
    }
    
    public int places { get; set; }

    public List<Client> clients = new List<Client>();
    
    public Class()
    { }

    public Class(int id, string name, int places, Course course, params Client[] clients)
    {
        this.id = id;
        this.name = name;
        this.places = places;
        this.course = course;
        this.clients.AddRange(clients);
    }
    
    public override string ToString()
    {
        return name;
    }
}