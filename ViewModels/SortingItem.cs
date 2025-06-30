using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.ViewModels
{
    public class SortingItem
    {
        public string Name { get; }
        public string Value { get; }

        public SortingItem(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
