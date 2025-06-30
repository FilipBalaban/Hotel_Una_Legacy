using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Models
{
    public class Room
    {
        public int ID { get; }    
        public int RoomNum { get; }
        public int Capacity { get; }
        public Room(int id, int roomNum, int capacity)
        {
            ID = id;
            RoomNum = roomNum;
            Capacity = capacity;
        }
    }
}
