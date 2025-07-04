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
            _sqlConnection.Close();
        }
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            string query = "SELECT * FROM Reservation";

            using (var command = new SqlCommand(query, _sqlConnection))
            {
                if (_sqlConnection.State != ConnectionState.Open)
                    await _sqlConnection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        int roomNum = reader.GetInt32(reader.GetOrdinal("room_number"));
                        string firstName = reader["first_name"].ToString();
                        string lastName = reader["last_name"].ToString();
                        DateTime reservationDate = reader.GetDateTime(reader.GetOrdinal("reservation_date"));
                        DateTime checkIn = reader.GetDateTime(reader.GetOrdinal("check_in"));
                        DateTime checkOut = reader.GetDateTime(reader.GetOrdinal("check_out"));
                        int numberOfGuests = reader.GetInt32(reader.GetOrdinal("number_of_guests"));

                        reservations.Add(new Reservation(id, roomNum, firstName, lastName, reservationDate, checkIn, checkOut, numberOfGuests));
                    }
                }
            }
            _sqlConnection.Close();
            return reservations;
        }
        public async Task RemoveReservation(Reservation reservation)
        {
            string query = "DELETE FROM Reservation WHERE id = @ReservationID";
            SqlCommand sqlCommand = new SqlCommand(query, _sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ReservationID", reservation.ID);
            await _sqlConnection.OpenAsync();
            await sqlCommand.ExecuteScalarAsync();
            _sqlConnection.Close();
        }
        public async Task UpdateReservation(Reservation newReservation)
        {
            string query = "UPDATE Reservation SET room_number = @RoomNum, first_name = @FirstName, last_name = @LastName, check_in = @CheckIn, check_out = @CheckOut, number_of_guests = @NumberOfGuests WHERE id = @ReservationID";
            SqlCommand sqlCommand = new SqlCommand(query, _sqlConnection);
            await _sqlConnection.OpenAsync();
            sqlCommand.Parameters.AddWithValue("@ReservationID", newReservation.ID);
            sqlCommand.Parameters.AddWithValue("@RoomNum", newReservation.RoomNum);
            sqlCommand.Parameters.AddWithValue("@FirstName", newReservation.FirstName);
            sqlCommand.Parameters.AddWithValue("@LastName", newReservation.LastName);
            sqlCommand.Parameters.AddWithValue("@CheckIn", newReservation.StartDate);
            sqlCommand.Parameters.AddWithValue("@CheckOut", newReservation.EndDate);
            sqlCommand.Parameters.AddWithValue("@NumberOfGuests", newReservation.NumberOfGuests);

            await sqlCommand.ExecuteScalarAsync();
            _sqlConnection.Close();
        }
    }
}
