using System;
using System.Collections.Generic;

namespace RoomBookingAPI.Models
{
    public partial class Room
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
            RoomAminity = new HashSet<RoomAminity>();
        }

        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public int AdultsCapacity { get; set; }
        public int? ChildrenCapacity { get; set; }
        public decimal Price { get; set; }

        public ICollection<Booking> Booking { get; set; }
        public ICollection<RoomAminity> RoomAminity { get; set; }
    }
}
