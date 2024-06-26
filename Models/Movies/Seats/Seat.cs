﻿using System.ComponentModel.DataAnnotations;

namespace RMall_BE.Models.Movies.Seats
{
    public class Seat
    {
        public int Id { get; set; }
        public int Room_Id { get; set; }
        public int Seat_Type_Id { get; set; }
        public int Row_Number { get; set; }
        public int Seat_Number { get; set; }
        public Room Room { get; set; }
        public ICollection<SeatReservation> SeatReservations { get; set; }
        public SeatType SeatType { get; set; }
    }
}
