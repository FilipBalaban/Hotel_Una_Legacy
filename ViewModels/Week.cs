using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.ViewModels
{
    public class Week
    {
        public Day Monday { get; set; }
        public Day Tuesday { get; set; }
        public Day Wednesday { get; set; }
        public Day Thursday { get; set; }
        public Day Friday { get; set; }
        public Day Saturday { get; set; }
        public Day Sunday { get; set; }

        public void AssignDateToDay(int date, string day, string color="Black", string fontWeight="Normal")
        {
            switch (day)
            {
                case "Monday":
                    Monday = new Day(date, color, fontWeight);
                    break;
                case "Tuesday":
                    Tuesday = new Day(date, color, fontWeight);
                    break;
                case "Wednesday":
                    Wednesday = new Day(date, color, fontWeight);
                    break;
                case "Thursday":
                    Thursday = new Day(date, color, fontWeight);
                    break;
                case "Friday":
                    Friday = new Day(date, color, fontWeight);
                    break;
                case "Saturday":
                    Saturday = new Day(date, color, fontWeight);
                    break;
                case "Sunday":
                    Sunday = new Day(date, color, fontWeight);
                    break;
            }
        }
    }
}
