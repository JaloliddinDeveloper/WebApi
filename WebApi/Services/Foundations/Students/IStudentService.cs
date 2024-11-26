using WebApi.Models.Foundations.Students;

namespace WebApi.Services.Foundations.Students
{
    public interface IStudentService
    {
        ValueTask<Student> RemoveStudentByIdAsync(int studentId);
    }
}
