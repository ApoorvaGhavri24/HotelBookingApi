using System;
using System.Collections.Generic;

namespace RoomBookingAPI.Models
{
    public partial class Status
    {
        public Status()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public int Statusorder { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Booking> Booking { get; set; }
    }
}
