using System;
using System.Collections.Generic;

namespace RoomBookingAPI.Models
{
    public partial class Amenity
    {
        public Amenity()
        {
            RoomAminity = new HashSet<RoomAminity>();
        }

        public int Id { get; set; }
        public string Label { get; set; }
        public int Amenityorder { get; set; }
        public bool IsActive { get; set; }

        public ICollection<RoomAminity> RoomAminity { get; set; }
    }
}
