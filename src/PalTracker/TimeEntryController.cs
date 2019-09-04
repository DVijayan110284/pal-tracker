using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private ITimeEntryRepository _iTimeEntryRepository;
        public TimeEntryController(ITimeEntryRepository iTimeEntryRepository)
        {
            _iTimeEntryRepository = iTimeEntryRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            var createdTimeEntry = _iTimeEntryRepository.Create(timeEntry);

            return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            return _iTimeEntryRepository.Contains(id) ? (IActionResult) Ok(_iTimeEntryRepository.Find(id)) : NotFound();
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(_iTimeEntryRepository.List());
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TimeEntry timeEntry)
        {
            return _iTimeEntryRepository.Contains(id) ? (IActionResult) Ok(_iTimeEntryRepository.Update(id, timeEntry)) : NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!_iTimeEntryRepository.Contains(id))
            {
                return NotFound();
            }

            _iTimeEntryRepository.Delete(id);

            return NoContent();
        }
    }
}