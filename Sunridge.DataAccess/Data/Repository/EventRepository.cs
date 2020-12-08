using Sunridge.DataAccess.Data.Repository.IRepository;
using Sunridge.Models;
using System.Linq;

namespace Sunridge.DataAccess.Data.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _db;

        //Inherits the base options listed in services in startup.cs
        public EventRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Event eventObj)
        {
            //Gets the first object from the table that has the same id as the one passed in
            var objFromDb = _db.Event.FirstOrDefault(e => e.Id == eventObj.Id);

            objFromDb.Title = eventObj.Title;

            if(eventObj.Description != null)
            {
                objFromDb.Description = eventObj.Description;
            }

            if (eventObj.Location != null)
            {
                objFromDb.Location = eventObj.Location;
            }

            if (eventObj.Image != null)
            {
                objFromDb.Image = eventObj.Image;
            }

            objFromDb.Start = eventObj.Start;

            if (eventObj.End != null)
            {
                objFromDb.End = eventObj.End;
            }

            objFromDb.AllDay = eventObj.AllDay;

            _db.SaveChanges();
        }
    }
}
