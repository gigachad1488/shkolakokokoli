using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shkolakokokoli.Models
{
    public class Lesson
    {
        public int id { get; set; }
        public Class group;

        public string Group
        {
            get
            {
                return group.ToString();
            }
        }

        public DateTime startTime { get;  set; }
        public DateTime endTime { get; set; }
    }
}
