using RoomBookingAPI.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomBookingAPI.Models.DataManager
{
    public class RoomManager : IDataRepository<Room>
    {
        readonly RoomBookingDBContext _roomContext;
        public RoomManager(RoomBookingDBContext context)
        {
            _roomContext = context;
        }
        public IEnumerable<Room> GetAll()
        {
            return _roomContext.Room.ToList();
        }
        public Room Get(long id)
        {
            return _roomContext.Room
                  .FirstOrDefault(e => e.Id == id);
        }
        public void Add(Room entity)
        {
            _roomContext.Room.Add(entity);
            _roomContext.SaveChanges();
        }
        public void Update(Room room, Room entity)
        {
            room.RoomNumber = entity.RoomNumber;
            room.AdultsCapacity = entity.AdultsCapacity;
            room.ChildrenCapacity = entity.ChildrenCapacity;
            room.Price = entity.Price;
            _roomContext.SaveChanges();
        }
        public void Delete(Room room)
        {
            _roomContext.Room.Remove(room);
            _roomContext.SaveChanges();
        }
    }
}
