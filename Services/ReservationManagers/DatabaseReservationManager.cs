using Hotel_Una_Legacy.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Services.ReservationManagers
{
    public class DatabaseReservationManager : IReservationManager
    {
        private readonly SqlConnection _sqlConnection;
        public DatabaseReservationManager(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }
        public async Task AddReservation(Reservation reservation)
        {
            string query = "INSERT INTO Reservation(room_number, first_name, last_name, check_in, check_out, number_of_guests) VALUES(@RoomNum, @FirstName, @LastName, @CheckIn, @CheckOut, @NumOfGuests)";

            SqlCommand sqlCommand = new SqlCommand(query, _sqlConnection);
            await _sqlConnection.OpenAsync();
            sqlCommand.Parameters.AddWithValue("@RoomNum", reservation.RoomNum);
            sqlCommand.Parameters.AddWithValue("@FirstName", reservation.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", reservation.LastName);
            sqlCommand.Parameters.AddWithValue("@CheckIn", reservation.StartDate);
            sqlCommand.Parameters.AddWithValue("@CheckOut", reservation.EndDate);
            sqlCommand.Parameters.AddWithValue("@NumOfGuests", reservation.NumberOfGuests);
            await sqlCommand.ExecuteScalarAsync();
        }
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            string query = "SELECT * FROM Reservation";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, _sqlConnection);
            using (sqlDataAdapter)
            {
                DataTable reservationDT = new DataTable();
                sqlDataAdapter.Fill(reservationDT);
                foreach (DataRow row in reservationDT.Rows)
                {
                    int id = (int)row["id"];
                    int roomNum = (int)row["room_number"];
                    string firstName = row["first_name"].ToString();
                    string lastName = row["last_name"].ToString();
                    DateTime reservationDate = (DateTime)row["reservation_date"];
                    DateTime checkIn = (DateTime)row["check_in"];
                    DateTime checkOut = (DateTime)row["check_out"];
                    int numberOfGuests = (int)row["number_of_guests"];
                    reservations.Add(new Reservation(id, roomNum, firstName, lastName, reservationDate, checkIn, checkOut, numberOfGuests));
                }
            }
            return reservations;
        }
        public async Task RemoveReservation(Reservation reservation)
        {
            
        }
        public async Task UpdateReservation(Reservation newReservation)
        {
            
        }
    }
}
