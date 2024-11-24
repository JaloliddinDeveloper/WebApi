using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using WebApi.Brokers.Storages;
using WebApi.Models.Foundations.Students;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : RESTFulController
    {
        private readonly IStorageBroker storageBroker;

        public StudentController(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        [HttpPost]
        public async ValueTask<ActionResult<Student>> PostStudentAsync(Student student)
        {
            try
            {
                Student postedStudent = await this.storageBroker.InsertStudentAsync(student);
                return Created(postedStudent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Student>>> GetAllStudentsAsync()
        {
            try
            {
                IQueryable<Student> students =
                    await this.storageBroker.SelectAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<Student>> GetStudentById(Guid studentId)
        {
            try
            {
                var student = await this.storageBroker.SelectStudentByIdAsync(studentId);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Student>> PutStudentAsync(Student student)
        {
            try
            {
                Student modifiedStudent =
                    await this.storageBroker.UpdateStudentAsync(student);

                return Ok(modifiedStudent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
