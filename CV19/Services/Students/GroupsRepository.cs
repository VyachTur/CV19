using CV19.Models.Decanat;
using CV19.Services.Base;
using System;
using System.Linq;

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

        public Group Get(string groupName) => GetAll().FirstOrDefault(g => g.Name == groupName);
    }
}
