﻿using NTR.Common.DataAccess.Repositories;
using NTR.Common.DataAccess.UnitTest._TestObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace NTR.Common.DataAccess.UnitTest.Repositories
{
    public class GenericEntityRepositoryTests
    {
        private InMemoryContext _context;
        private IRepository<Foo> _fooRepository;

        public GenericEntityRepositoryTests()
        {
            _fooRepository = new GenericEntityRepository<Foo>(null);
            _context = InMemoryContext.Create();
            ((IRepositoryInjection)_fooRepository).SetContext(_context);
        }

        private void AddEntitiesToContext(int count = 10)
        {
            // Add entities to context
            for (var i = 1; i <= count; i++)
            {
                var foo = new Foo()
                {
                    id = i
                };

                _context.Foos.Add(foo);
            }

            _context.SaveChanges();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void GetAllDoesNotReturnNull(int totalEntities)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.GetAll();

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void GetAllReturnsCorrectNumberOfEntities(int totalEntities)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.GetAll();

            // Assert
            Assert.Equal(totalEntities, result.Count());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void GetAllReturnsAllEntitiesInContext(int totalEntities)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.GetAll();

            // Assert
            for (int i = 1; i <= totalEntities; i++)
                Assert.Equal(1, result.Count(x => x.id == i));
        }

        [Theory]
        [InlineData(10)]
        public void GetAllSortedByIdReturnsAllEntitiesInCorrectOrder(int totalEntities)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.GetAll(x => x.OrderBy(y => y.id));

            // Assert
            var i = 0;
            foreach (var entity in result)
            {
                i++;
                Assert.Equal(i, entity.id);
            }
        }

        [Theory]
        [InlineData(10)]
        public void GetAllReverseSortedByIdReturnsAllEntitiesInCorrectOrder(int totalEntities)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.GetAll(x => x.OrderByDescending(y => y.id));

            // Assert
            var i = totalEntities;
            foreach (var entity in result)
            {
                Assert.Equal(i, entity.id);
                i--;
            }
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(3, 4)]
        public void GetReturnsNullWhenEntityDoesNotExistInContext(int totalEntities, int id)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.Get(id);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(3, 3)]
        public void GetDoesNotReturnNullWhenEntityExistsInContext(int totalEntities, int id)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.Get(id);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(3, 3)]
        public void GetReturnsSingleEntityWithProvidedId(int totalEntities, int id)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.Get(id);

            // Assert
            Assert.Equal(id, result.id);
        }

        [Theory]
        [InlineData(10, 0, 5)]
        [InlineData(10, 5, 5)]
        [InlineData(10, 10, 5)]
        public void GetPageSortedByIdDoesNotReturnNull(int totalEntities, int rowIndex, int pageSize)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.GetPage(rowIndex, pageSize, x => x.OrderBy(y => y.id));

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(10, 0, 5)]
        [InlineData(10, 5, 5)]
        [InlineData(10, 10, 5)]
        [InlineData(8, 5, 5)]
        public void GetPageSortedByIdReturnsCorrectNumberOfEntities(int totalEntities, int rowIndex, int pageSize)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);
            var minId = rowIndex + 1;
            var maxId = Math.Min(totalEntities, minId + pageSize - 1);
            var expectedNumberOfEntities = Math.Max(0, maxId - minId + 1);

            // Act
            var result = _fooRepository.GetPage(rowIndex, pageSize, x => x.OrderBy(y => y.id));

            // Assert
            Assert.Equal(expectedNumberOfEntities, result.Count());
        }

        [Theory]
        [InlineData(10, 0, 5)]
        [InlineData(10, 5, 5)]
        [InlineData(10, 10, 5)]
        [InlineData(8, 5, 5)]
        public void GetPageSortedByIdReturnsEntitiesWithCorrectIds(int totalEntities, int rowIndex, int pageSize)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            var minId = rowIndex + 1;
            var maxId = Math.Min(totalEntities, minId + pageSize - 1);

            // Act
            var result = _fooRepository.GetPage(rowIndex, pageSize, x => x.OrderBy(y => y.id));

            // Assert
            for (int i = minId; i <= maxId; i++)
                Assert.Equal(1, result.Count(x => x.id == i));
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(10, 5)]
        [InlineData(10, 10)]
        public void AnyByIdReturnsTrueForExistingRecord(int totalEntities, int id)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.Any(x => x.id == id);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(10, 0)]
        [InlineData(10, 11)]
        public void AnyByIdReturnsFalseForNonExistingRecord(int totalEntities, int id)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.Any(x => x.id == id);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(10, 2)]
        public void Filters(int totalEntities, int id)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.Filters(x => x.id == id).ToList();

            // Assert
            Assert.True(1==result.Count);
        }

        [Theory]
        [InlineData(10, 1)]
        [InlineData(10, 2)]
        public void FilterDefaults(int totalEntities, int id)
        {
            // Arrange
            AddEntitiesToContext(totalEntities);

            // Act
            var result = _fooRepository.Filters(x => x.id == id).FirstOrDefault();

            // Assert
            Assert.True(!ReferenceEquals(result,null));
        }
    }
}
