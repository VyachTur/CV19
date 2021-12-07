using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services.Students
{
    internal class StudentsRepository : RepositoryInMemory<Student>
    {
        protected override void Update(Student source, Student destination)
        {
            destination.Name = source.Name;
            destination.Surname = source.Surname;
            destination.Patronimic = source.Patronimic;
            destination.Birthday = source.Birthday;
            destination.Rating = source.Rating;
        }
    }
}
