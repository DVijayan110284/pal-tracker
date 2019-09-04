using System.Collections.Generic;
using System.Linq;

namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
       private readonly IDictionary<long, TimeEntry> _timeEntrys = new Dictionary<long, TimeEntry>();
        
        public bool Contains(long id) => _timeEntrys.ContainsKey(id);

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var id = _timeEntrys.Count + 1;
            timeEntry.Id = id;
            _timeEntrys.Add(id, timeEntry);
            return timeEntry;
        }
        
         public TimeEntry Find(long id) => _timeEntrys[id];

        public IEnumerable<TimeEntry> List() => _timeEntrys.Values.ToList();

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            timeEntry.Id = id;

            _timeEntrys[id] = timeEntry;

            return timeEntry;
        }

        public void Delete(long id)
        {
            _timeEntrys.Remove(id);
        }
    }
}
