using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ConsoleApp20 {
    internal class Program {
        static async Task Main(string[] args) {
            var Options = new DbContextOptionsBuilder<TestContext>()
                .UseSqlServer("Server=(local);Database=__TEST_DB;Integrated Security=True;Trust Server Certificate=True")
                ;

            var Context = new TestContext(Options.Options);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();

            var Item11 = new Frame<TestPacket1>() {
                Id = "I11",
                Original = {
                    FirstName = "First",
                },
            }.CopySourceToDest();

            var Item12 = new Frame<TestPacket1>() {
                Id = "I12",
                Original = {
                    FirstName = "First",
                },
            }.CopySourceToDest();

            var Item21 = new Frame<TestPacket2>() {
                Id = "I21",
                Original = { 
                    LastName = "Last",
                },
            }.CopySourceToDest();

            var Item22 = new Frame<TestPacket2>() {
                Id = "I22",
                Original = {
                    LastName = "Last",
                },
            }.CopySourceToDest();


            var Item31 = new Frame<TestPacket1>() {
                Id = "I31",
                Original = {
                    FirstName = "First",
                },
            }.CopySourceToDest();

            var Item32 = new Frame<TestPacket2>() {
                Id = "I32",
                Original = {
                    LastName = "Last",
                },
            }.CopySourceToDest();

            var Set1 = new Frame[] { Item11, Item12, };
            var Set2 = new Frame[] { Item21, Item22, };
            var Set3 = new Frame[] { Item31, Item32, };

            Context.BulkInsert(Set1);

            Context.AddRange(Set2);
            Context.SaveChanges();

            //Context.BulkInsert(Set3);

        }
    }


}