namespace CSV_Import.Server.Controllers
{
    using CSV_Import.Server.Models;
    using CSV_Import.Server.Services;
    using Microsoft.AspNetCore.Mvc;
    
    [ApiController]
    [Route("[controller]")]
    public class CsvController : Controller
    {
        private readonly ILogger<CsvController> _logger;
        public PersonService PersonService { get; set; }

        public CsvController(ILogger<CsvController> logger, PersonService personService)
        {
            _logger = logger;
            PersonService = personService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersons()
        {
            return await PersonService.GetPersons();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsv([FromForm] IFormFile file)
        {
            var result = await PersonService.UploadCsv(file);

            if (result == null)
                return BadRequest();
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson(Person person)
        {
            var result = await PersonService.CreatePerson(person);

            return CreatedAtAction(nameof(GetPersons), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerson(int id, Person person)
        {
            var result = await PersonService.UpdatePerson(id, person);

            return result == true ? NoContent() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await PersonService.DeletePerson(id);

            return result == true ? NoContent() : BadRequest();
        }

    }
}
