using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using WebApi.Brokers.Storages;
using WebApi.Models.Foundations.Students;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController: RESTFulController
    {
        private readonly IStorageBroker storageBroker;

        public StudentController(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;


        [HttpPost]
        public async ValueTask<ActionResult<IEnumerable<Student>>> PostRandomStudentsAsync()
        {
            try
            {
                List<Student> studentsToInsert = new List<Student>();

                for (int i = 0; i < 50; i++)
                {
                    var student = new Student
                    {
                        FullName = "Student" + i,  
                        Age = new Random().Next(18, 40) 
                    };

                    studentsToInsert.Add(student);
                }

                foreach (var student in studentsToInsert)
                {
                    await this.storageBroker.InsertStudentAsync(student);
                }

                return CreatedAtAction(nameof(PostRandomStudentsAsync), studentsToInsert);
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
