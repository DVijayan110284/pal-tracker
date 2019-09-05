using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private ITimeEntryRepository _iTimeEntryRepository;
        private readonly IOperationCounter<TimeEntry> _operationCounter;

        public TimeEntryController(ITimeEntryRepository iTimeEntryRepository, IOperationCounter<TimeEntry> operationCounter)
        {
            _iTimeEntryRepository = iTimeEntryRepository;
            _operationCounter = operationCounter;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
           _operationCounter.Increment(TrackedOperation.Create);
            var createdTimeEntry = _iTimeEntryRepository.Create(timeEntry);

            return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
             _operationCounter.Increment(TrackedOperation.Read);
            return _iTimeEntryRepository.Contains(id) ? (IActionResult) Ok(_iTimeEntryRepository.Find(id)) : NotFound();
        }

        [HttpGet]
        public IActionResult List()
        {
            _operationCounter.Increment(TrackedOperation.Read);
            return Ok(_iTimeEntryRepository.List());
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
             _operationCounter.Increment(TrackedOperation.Read);
            return _iTimeEntryRepository.Contains(id) ? (IActionResult) Ok(_iTimeEntryRepository.Update(id, timeEntry)) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
             _operationCounter.Increment(TrackedOperation.Read);
            if (!_iTimeEntryRepository.Contains(id))
            {
                return NotFound();
            }

            _iTimeEntryRepository.Delete(id);

            return NoContent();
        }
    }
}