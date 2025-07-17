using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Models
{
    public class Reservation
    {
        public int ID { get; }
        public int RoomNum { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime ReservationDate { get; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string Comment { get; set; }

        // From DB
        public Reservation(int iD, int roomNum, string firstName, string lastName, DateTime reservationDate, DateTime startDate, DateTime endDate, int numberOfGuests, string comment = null)
        {
            ID = iD;
            RoomNum = roomNum;
            FirstName = firstName;
            LastName = lastName;
            ReservationDate = reservationDate;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfGuests = numberOfGuests;
            if (comment != null)
            {
                Comment = comment;
            }
        }
        public Reservation(int iD, int roomNum, string firstName, string lastName, DateTime startDate, DateTime endDate, int numberOfGuests, string comment = null)
        {
            ID = iD;
            RoomNum = roomNum;
            FirstName = firstName;
            LastName = lastName;
            ReservationDate = DateTime.Now;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfGuests = numberOfGuests;
            if (comment != null)
            {
                Comment = comment;
            }
        }
        // From VM
        public Reservation(int roomNum, string firstName, string lastName, DateTime startDate, DateTime endDate, int numberOfGuests, string comment = null)
        {
            RoomNum = roomNum;
            FirstName = firstName;
            LastName = lastName;
            ReservationDate = DateTime.Now;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfGuests = numberOfGuests;
            if (comment != null)
            {
                Comment = comment;
            }
        }
        public bool CausesConflicts(Reservation reservation)
        {
            if(RoomNum != reservation.RoomNum || ID == reservation.ID)
            {
                return false;
            }
            return reservation.StartDate < EndDate && reservation.EndDate > StartDate;
        }
    }
}
