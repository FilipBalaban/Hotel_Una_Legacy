using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.ViewModels
{
    public class Day
    {
        public int Date { get; set; }
        public string ForegroundColor { get; set; }
        public string FontWeight { get; set; }

        public Day(int date, string foregroundColor, string fontWeight)
        {
            Date = date;
            ForegroundColor = foregroundColor;
            FontWeight = fontWeight;
        }
    }
}
