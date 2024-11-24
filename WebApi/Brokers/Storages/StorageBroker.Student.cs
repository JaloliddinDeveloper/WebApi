using Microsoft.EntityFrameworkCore;
using WebApi.Models.Foundations.Students;

namespace WebApi.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Student> Students { get; set; }

        public async ValueTask<Student> InsertStudentAsync(Student student) =>
            await InsertAsync(student);

        public async ValueTask<IQueryable<Student>> SelectAllStudentsAsync() =>
            await SelectAllAsync<Student>();

        public async ValueTask<Student> SelectStudentByIdAsync(Guid studentId) =>
            await SelectAsync<Student>(studentId);

        public async ValueTask<Student> UpdateStudentAsync(Student student) =>
            await UpdateAsync(student);

        public async ValueTask<Student> DeleteStudentAsync(Student student) =>
            await DeleteAsync(student);


        public async Task<IQueryable<Student>> SelectStudentsOrderedByAgeAsync(bool ascending = true)
        {
            var query = ascending
                ? Students.OrderBy(student => student.Age)
                : Students.OrderByDescending(student => student.Age);

            return await Task.FromResult(query);
        }
    }
}
