using WebApi.Models.Foundations.Students;

namespace WebApi.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Student> InsertStudentAsync(Student student);
        ValueTask<IQueryable<Student>> SelectAllStudentsAsync();
        ValueTask<Student> SelectStudentByIdAsync(int studentId);
        ValueTask<Student> UpdateStudentAsync(Student student);
        ValueTask<Student> DeleteStudentAsync(Student student);
    }
}
