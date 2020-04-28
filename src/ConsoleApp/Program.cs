using ConsoleApp.EF;
using ConsoleApp.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RT.Comb;
using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

using static System.Threading.CancellationToken;

namespace ConsoleApp
{
    /// <summary>
    /// This is based on this Benchmark/Test
    /// http://microsoftprogrammers.jebarson.com/benchmarking-index-fragmentation-with-popular-sequential-guid-algorithms/
    /// </summary>
    static class Program
    {
        private static readonly UtcNoRepeatTimestampProvider _noDupeProvider = new UtcNoRepeatTimestampProvider();
        
        // If you want to run with SQL LocalDB, uncomment this line
        // private static string _connectionString = "Server=localhost,1433;Database=CombGuidBenchmark;User=sa;Password=Your_password123";
        
        // By default it will try to connect to SQL Server running on docker.
        // Make sure to run docker-compose up before starting the app
        private static string _connectionString = @"Server=(localdb)\mssqllocaldb;Database=CombGuidBenchmark;Trusted_Connection=True;MultipleActiveResultSets=true";

        private static readonly ICombProvider _sqlNoRepeatCombs = new SqlCombProvider(new SqlDateTimeStrategy(), customTimestampProvider: _noDupeProvider.GetTimestamp);

        static async Task Main(string[] args)
        {
            await MigrateDatabase();

            Console.WriteLine("Starting Option 1 - Identity");
            var watch = Stopwatch.StartNew();
            await InsertDataToTestTable<TableWithIdentity, int>();
            Console.WriteLine($"Identity - Inserted 1_000_000 rows in {watch.Elapsed.TotalSeconds} seconds");

            Console.WriteLine("Starting Option 2 - Normal C# Guid");
            watch = Stopwatch.StartNew();
            await InsertDataToTestTable<TableWithRegularGuid, Guid>(Guid.NewGuid);
            Console.WriteLine($"C# Guid - Inserted 1_000_000 rows in {watch.Elapsed.TotalSeconds} seconds");

            Console.WriteLine("Starting Option 3 - CombGuid");
            watch = Stopwatch.StartNew();
            await InsertDataToTestTable<TableWithCombGuid, Guid>(CombGuid.NewCombGuid);
            Console.WriteLine($"CombGuid - Inserted 1_000_000 rows in {watch.Elapsed.TotalSeconds} seconds");

            Console.WriteLine("Starting Option 4 - RT.Comb - UtcNoRepeat");
            watch = Stopwatch.StartNew();
            await InsertDataToTestTable<TableWithRTCombGuid, Guid>(_sqlNoRepeatCombs.Create);
            Console.WriteLine($"RT.Comb - UtcNoRepeat - Inserted 1_000_000 rows in {watch.Elapsed.TotalSeconds} seconds");

            Console.WriteLine("All done!");
            Console.ReadLine();
        }

        private static async Task InsertDataToTestTable<TEntity, TId>(
            Func<Guid>? guidGeneratorFunc = null, Func<Task>? delayStrategy = null)
            where TEntity : BaseBenchmarkEntity<TId>
            where TId : struct
        {
            var tableName = typeof(TEntity).Name;

            var numberOfRecords = 1_000_000;
            var numberOfSteps = numberOfRecords / 1000;

            // Split to batch of 1000 records.
            for (var stepIndex = 0; stepIndex < numberOfSteps; stepIndex++)
            {
                var commandText = new StringBuilder($"INSERT INTO {tableName} (");
                commandText.Append(guidGeneratorFunc == null ? string.Empty : " Id, ");
                commandText.Append("[Value] ) VALUES");

                Random rnd = new Random(stepIndex);

                for (var recordIndex = 0; recordIndex < numberOfRecords / numberOfSteps; recordIndex++)
                {
                    commandText.Append(recordIndex == 0 ? string.Empty : ",");
                    commandText.Append("(");
                    commandText.Append(guidGeneratorFunc == null ? string.Empty : $"'{guidGeneratorFunc.Invoke()}', ");
                    commandText.Append($"'{rnd.Next()}')");

                    if (delayStrategy != null)
                    {
                        await delayStrategy();
                    }
                }

                // Commit to the database.
                await using SqlConnection con = new SqlConnection(_connectionString);
                con.Open();
                await using SqlCommand cmd = new SqlCommand(commandText.ToString(), con)
                {
                    CommandType = CommandType.Text
                };
                await cmd.ExecuteNonQueryAsync(None);

                //Console.WriteLine($"{(stepIndex + 1) * 1000} records written to {tableName}");
            }
        }

        private static async Task MigrateDatabase()
        {
            var builder = new DbContextOptionsBuilder<CombGuidDbContext>();
            builder.UseSqlServer(_connectionString);

            await using var context = new CombGuidDbContext(builder.Options);

            // will be no-op if db is updated
            await context.Database.MigrateAsync(None);
        }
    }
}
