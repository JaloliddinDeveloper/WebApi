using WebApi.Brokers.Storages;
using WebApi.Models.Foundations.Students;

namespace WebApi.Services.Foundations.Students
{
    public class StudentService: IStudentService
    {
        private readonly IStorageBroker storageBroker;

        public StudentService(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;

        public async ValueTask<Student> RemoveStudentByIdAsync(int  studentId)
        {
            Student maybeStudent = await this.storageBroker.SelectStudentByIdAsync(studentId);

            return await this.storageBroker.DeleteStudentAsync(maybeStudent);
        }   
    }
}
