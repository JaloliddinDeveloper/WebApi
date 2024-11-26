using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using WebApi.Brokers.Storages;
using WebApi.Models.Foundations.Students;
using WebApi.Services.Foundations.Students;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : RESTFulController
    {
        private readonly IStorageBroker storageBroker;
        private readonly IStudentService studentService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public StudentController(
            IStorageBroker storageBroker,
            IStudentService studentService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.storageBroker = storageBroker;
            this.studentService = studentService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async ValueTask<ActionResult<Student>> PostStudent([FromForm] Student Student, IFormFile picture)
        {
            if (picture != null)
            {
                string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }

                Student.PictureUrl = $"images/{fileName}";
            }

            await this.storageBroker.InsertStudentAsync(Student);

            return Created(Student);
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
        public async Task<ActionResult<Student>> GetStudentById(int studentId)
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

        [HttpDelete("{studentId}")]
        public async ValueTask<ActionResult<Student>> DeleteStudentByIdAsync(int studentId)
        {
            try
            {
                return await this.studentService.RemoveStudentByIdAsync(studentId);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
