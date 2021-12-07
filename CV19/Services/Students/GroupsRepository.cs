using CV19.Models.Decanat;
using CV19.Services.Base;

namespace CV19.Services.Students
{
    internal class GroupsRepository : RepositoryInMemory<Group>
    {
        public GroupsRepository() : base(TestData.Groups) { }

        protected override void Update(Group source, Group destination)
        {
            destination.Name = source.Name;
            destination.Description = source.Description;
        }
    }
}
