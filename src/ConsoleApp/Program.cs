using ConsoleApp.EF;
using ConsoleApp.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

using static System.Threading.CancellationToken;

namespace ConsoleApp
{
    /// <summary>
    /// This is based on this Benchmark/Test
    /// http://microsoftprogrammers.jebarson.com/benchmarking-index-fragmentation-with-popular-sequential-guid-algorithms/
    /// I wanted to test an alternate version of the famous "NHibernate" CombGuid generation and see if there's any
    /// improvements in index fragmentation <see cref="CombGuid"/>
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            await MigrateDatabase();

            DateTime startTime = DateTime.Now;

            Console.WriteLine($"Starting Option 1");
            await InsertDataToTestTable<TableWithRegularGuid>(Guid.NewGuid);
            Console.WriteLine($"Completed in {(DateTime.Now - startTime).TotalSeconds}s");

            Console.WriteLine($"Starting Option 2");
            startTime = DateTime.Now;
            await InsertDataToTestTable<TableWithNewSequentialIdAsDefault>();
            Console.WriteLine($"Completed in {(DateTime.Now - startTime).TotalSeconds}s");

            Console.WriteLine($"Starting Option 3");
            startTime = DateTime.Now;
            await InsertDataToTestTable<TableWithExtendedUuidCreateSequential>(SQLGuidUtil.NewSequentialId);
            Console.WriteLine($"Completed in {(DateTime.Now - startTime).TotalSeconds}s");

            Console.WriteLine($"Starting Option 4");
            startTime = DateTime.Now;
            await InsertDataToTestTable<TableWithSpanCustomGuidComb>(CombGuid.NewCombGuid);
            Console.WriteLine($"Completed in {(DateTime.Now - startTime).TotalSeconds}s");

            Console.WriteLine($"Starting Option 5");
            startTime = DateTime.Now;
            await InsertDataToTestTable<TableWithCustomGuidInSql>();
            Console.WriteLine($"Completed in {(DateTime.Now - startTime).TotalSeconds}s");

            Console.ReadLine();
        }

        private static async Task InsertDataToTestTable<TEntity>(Func<Guid> guidGeneratorFunc = null)
            where TEntity : BaseBenchmarkEntity
        {
            var tableName = typeof(TEntity).Name;

            int numberOfRecords = 1000000;
            int numberOfSteps = numberOfRecords / 1000;

            // Split to batch of 1000 records.
            for (int stepIndex = 0; stepIndex < numberOfSteps; stepIndex++)
            {
                var commandText = new StringBuilder($"INSERT INTO {tableName} (");
                commandText.Append(guidGeneratorFunc == null ? string.Empty : " Id, AnotherId, ");
                commandText.Append("[Value] ) VALUES");

                Random rnd = new Random(stepIndex);

                for (int recordIndex = 0; recordIndex < numberOfRecords / numberOfSteps; recordIndex++)
                {
                    commandText.Append((recordIndex == 0) ? string.Empty : ",");
                    commandText.Append("(");
                    commandText.Append(guidGeneratorFunc == null ? string.Empty : $"'{guidGeneratorFunc.Invoke()}', '{guidGeneratorFunc.Invoke()}' , ");
                    commandText.Append($"'{rnd.Next()}')");
                }

                // Commit to the database.
                using (SqlConnection con = new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=CombGuidBenchmark;Trusted_Connection=True;MultipleActiveResultSets=true"))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(commandText.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        await cmd.ExecuteNonQueryAsync(None);
                    }
                }

                Console.WriteLine($"{(stepIndex + 1) * 1000} records written to {tableName}");
            }
        }

        private static async Task MigrateDatabase()
        {
            using var context = new CombGuidDbContext();

            // will be no-op if db is updated
            await context.Database.MigrateAsync(None);
        }
    }
}
