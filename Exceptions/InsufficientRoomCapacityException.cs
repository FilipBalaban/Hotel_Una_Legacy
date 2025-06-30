using Hotel_Una_Legacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Exceptions
{
    public class InsufficientRoomCapacityException : Exception
    {
        public Room Room { get; }
        public int NumberOfGuests { get; }

        public InsufficientRoomCapacityException(Room room, int numberOfGuests)
        {
            Room = room;
            NumberOfGuests = numberOfGuests;
        }

        public InsufficientRoomCapacityException(string message, Room room, int numberOfGuests) : base(message)
        {
            Room = room;
            NumberOfGuests = numberOfGuests;
        }

        public InsufficientRoomCapacityException(string message, Exception innerException, Room room, int numberOfGuests) : base(message, innerException)
        {
            Room = room;
            NumberOfGuests = numberOfGuests;
        }

        protected InsufficientRoomCapacityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
