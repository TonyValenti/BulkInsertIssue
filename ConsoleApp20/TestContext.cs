using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace ConsoleApp20 {
    public class TestContext : DbContext {

        protected DbContextOptions<TestContext> Options { get; private set; }

        public TestContext(DbContextOptions<TestContext> Options) : base(Options) {
            this.Options = Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            //ERROR:  IF THIS RUNS, WE'LL GET EMPTY COLUMNS FOR BULK INSERT.
            modelBuilder.Entity<Frame<TestPacket1>>()
                .ToTable("TestPacket1")
                .OwnsOne(x => x.Original, owned => {
                    owned.Property(p => p.FirstName).HasDefaultValue(string.Empty);
                })
                .OwnsOne(x => x.Final, owned => {
                    owned.Property(p => p.FirstName).HasDefaultValue(string.Empty);
                })
                ;
                

            //MapTable<TestPacket1>(modelBuilder, "TestPacket1");
            MapTable<TestPacket2>(modelBuilder, "TestPacket2");

            
            //modelBuilder.ForEachProperty<string>(x => x.HasDefaultValue(""), CanReadAndWrite);
            //modelBuilder.ForEachProperty2<string>(x => x.SetDefaultValue(""), CanReadAndWrite);

            base.OnModelCreating(modelBuilder);

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) {

            base.ConfigureConventions(configurationBuilder);
        }

        static bool CanReadAndWrite(PropertyInfo V) {
            return V.CanRead && V.CanWrite;
        }

        static EntityTypeBuilder<Frame<TPacket>> MapTable<TPacket>(ModelBuilder This, string Table)
            where TPacket : Packet, new() {
                var ret = This.Entity<Frame<TPacket>>()
                .ToTable(Table, x => x.HasTrigger("__NONE"))
                ;
            
            return ret;

        }

    }
}