using NTR.Common.DataAccess.Test._TestObjects;
using NTR.Common.DataAccess.Context;
using NTR.Common.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NTR.Common.DataAccess.UnitTest._TestObjects
{
    public class InMemoryContext : EntityContextBase<InMemoryContext>
    {
        public InMemoryContext(DbContextOptions<InMemoryContext> options)
            : base(options)
        {
        }

        public DbSet<Foo> Foos { get; set; }
        public DbSet<ParentTable> parents { get; set; }
        public DbSet<ChildTable> childs { get; set; }



        private readonly MethodInfo SetGlobalQueryMethod = typeof(InMemoryContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");
        private IList<Type> _entityTypeCache;
        private IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch (FileNotFoundException)
                {
                    // TODO
                    //throw ex;
                }
            }
            var dllDatas = assemblies.Where(x => x.FullName.Contains("NTR.DataAccess.Test")).ToList();

            return dllDatas;
        }

        private IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null)
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(EntityBase) && !t.FullName.Contains("NTR.Datas.EntityBases")
                                select t.AsType()).ToList();

            return _entityTypeCache;
        }


        public void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            foreach (var type in GetEntityTypes())
            {
                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { modelBuilder });

            }

            base.OnModelCreating(modelBuilder);
        }

        public void SetGlobalQuery<T>(ModelBuilder builder) where T : EntityBase
        {
            try
            {
                builder.Entity<T>().HasQueryFilter(e => !e.is_deleted);
            }
            catch (Exception ex)
            {
                // TODO Handle exception
                throw ex;
            }
        }


        public static InMemoryContext Create()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<InMemoryContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            builder.UseInMemoryDatabase("demo")
#pragma warning restore CS0618 // Type or member is obsolete
                   .UseInternalServiceProvider(serviceProvider);

            return new InMemoryContext(builder.Options);
        }
    }
}
