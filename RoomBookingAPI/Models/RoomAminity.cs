using System;
using System.Collections.Generic;

namespace RoomBookingAPI.Models
{
    public partial class RoomAminity
    {
        public int Id { get; set; }
        public int AmenityId { get; set; }
        public int RoomId { get; set; }
        public decimal AmenityPrice { get; set; }

        public Amenity Amenity { get; set; }
        public Room Room { get; set; }
    }
}
