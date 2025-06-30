using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Exceptions
{
    public class NonExistentRoomException : Exception
    {
        public int RoomNum { get; }
        public NonExistentRoomException(int roomNum)
        {
            RoomNum = roomNum;
        }

        public NonExistentRoomException(string message, int roomNum) : base(message)
        {
            RoomNum = roomNum;
        }

        public NonExistentRoomException(string message, Exception innerException, int roomNum) : base(message, innerException)
        {
            RoomNum = roomNum;
        }

        protected NonExistentRoomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
