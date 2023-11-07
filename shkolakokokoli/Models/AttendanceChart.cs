using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shkolakokokoli.Models
{
    public class AttendanceChart
    {
        public DateTime date;

        public int Date
        {
            get
            {
                return date.DayOfYear;
            }
        }

        public int attendancesCount { get; set; }

        public AttendanceChart(DateTime date, int attendancesCount)
        {
            this.date = date;
            this.attendancesCount = attendancesCount;
        }

        public AttendanceChart() { }
    }
}
