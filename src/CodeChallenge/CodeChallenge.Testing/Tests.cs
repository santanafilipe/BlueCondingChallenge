using CodeChallenge.API.Data;
using CodeChallenge.API.Models;
using CodeChallenge.API.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace CodeChallenge.Testing
{
    public class Tests
    {
        private Mock<CodeChallengeContext> _contextMock;
        private ShortUrlRepository _repository;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<CodeChallengeContext>();
            _repository = new ShortUrlRepository(_contextMock.Object);

            _contextMock.Setup(c => c.Entry(It.IsAny<ShortUrls>())).Returns(Mock.Of<EntityEntry<ShortUrls>>());
        }

        [Test]
        public void Add_EntityIsAdded()
        {
            // Arrange
            var entity = new ShortUrls();

            // Act
            _repository.Add(entity);

            // Assert
            _contextMock.Verify(c => c.Add(entity), Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }



        [Test]
        public void GetAll_ReturnsAllEntities()
        {
            // Arrange
            var entities = GetMockData(); // Sample data for testing
            _contextMock.Setup(c => c.Set<ShortUrls>()).Returns(MockDbSet(entities));

            // Act
            var result = _repository.GetAll();

            // Assert
            Assert.AreEqual(entities.Count, result.Count());
        }

        [Test]
        public void Update_EntityIsUpdated()
        {
            // Arrange
            var entity = new ShortUrls
            {
                Id = 1,
                ShortUrl = "aB645",
                Url = "https://www.google.com/",
                Clicked = 4
            };

            // Act
            _repository.Update(entity);

            // Assert
            _contextMock.VerifySet(c => c.Entry(entity).State = EntityState.Modified, Times.Once);
            _contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        // Helper method to generate sample data
        private List<ShortUrls> GetMockData()
        {
            var list = new List<ShortUrls>();
            list.Add(new ShortUrls
            {
                Id = 1,
                ShortUrl = "aB",
                Url = "https://www.google.com/",
                Clicked = 4
            });

            list.Add(new ShortUrls
            {
                Id = 2,
                ShortUrl = "aC",
                Url = "https://www.linkedin.com/",
                Clicked = 2
            });

            return list;
        }

        // Helper method to create a mock DbSet
        private DbSet<T> MockDbSet<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());

            return dbSetMock.Object;
        }
    }
}