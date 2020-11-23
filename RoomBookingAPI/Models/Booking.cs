using System;
using System.Collections.Generic;

namespace RoomBookingAPI.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public string GuestLastName { get; set; }
        public string GuestFirstName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
        public int RoomId { get; set; }
        public int StatusId { get; set; }

        public Room Room { get; set; }
        public Status Status { get; set; }
    }
}
