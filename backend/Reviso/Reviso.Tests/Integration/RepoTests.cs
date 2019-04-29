using Reviso.Application;
using Reviso.Data;
using Reviso.Domain.Dtos;
using Reviso.Domain.Entities;
using Reviso.Domain.Factories;
using System;
using System.Linq;
using Xunit;

namespace Reviso.Tests.Integration
{
    public class RepoTests
    {
        [Fact]
        public void Can_Add_Project_Aggregate_To_Repositoy()
        {
            //arrange
            var dto = new CreateEditProjectDto
            {
                Customer = "Test Customer",
                BaseRate = 100,
                Start = DateTime.Now.Date,
                Project = "Test Project",
                VatRate = 0.22m
            };

            //act
            PersistTestProject(dto);

            //assert
            var repo = new Repository<Project>(new RevisoContext());
            var projects = repo.GetAllIncluding(p => p.Contract.Customer).ToList();

            var persistedProject = projects
                .Where(p => p.Name.Equals(dto.Project))
                .FirstOrDefault();

            var actual = persistedProject.Name;
            var expected = dto.Project;

            Assert.Equal(expected, actual);

            //cleanup
            DeleteProject(persistedProject, repo);
        }

        [Fact]
        public void Can_Delete_Project_Aggregate()
        {
            //arrange
            var dto = new CreateEditProjectDto
            {
                Customer = "Test Customer",
                BaseRate = 100,
                Start = DateTime.Now.Date,
                Project = "Test Project",
                VatRate = 0.22m
            };

            PersistTestProject(dto);

            using (var repo = new Repository<Project>(new RevisoContext()))
            {
                var projects = repo.GetAllIncluding(p => p.TimeRegistrations, p => p.Contract.Customer);

                var persistedProject = projects
                        .Where(p => p.Name.Equals("Test Project"))
                        .FirstOrDefault();

                //act
                repo.Delete(persistedProject);
            }

            //assert
            using (var repo = new Repository<Project>(new RevisoContext()))
            {
                var shouldBeNull = repo.GetAllIncluding(p => p.Contract.Customer)
                                    .Where(p => p.Name.Equals("Test Project"))
                                    .FirstOrDefault();

                Assert.Null(shouldBeNull);
            }
        }

        private void DeleteProject(Project persistedProject, Repository<Project> repo)
        {
            repo.Delete(persistedProject);
        }

        private Project PersistTestProject(CreateEditProjectDto dto)
        {
            using (var repo = new Repository<Project>(new RevisoContext()))
            {
                var project = ProjectFactory.Create(dto);

                repo.Add(project);

                return project;
            }
        }
    }
}
