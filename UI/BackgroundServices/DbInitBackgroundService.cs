using Microsoft.Data.Sqlite;

namespace UI.BackgroundServices;

public class DbInitBackgroundService(ILogger<DbInitBackgroundService> logger): BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var dbPath = Path.Combine(AppContext.BaseDirectory, "identifier.sqlite");

            if (!File.Exists(dbPath))
            {
                using var connection = new SqliteConnection($"Data Source={dbPath}");
                connection.Open();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "quartz_db_create.sql");
                var script = File.ReadAllText(path);

                var command = connection.CreateCommand();
                command.CommandText = script;
                command.ExecuteNonQuery();
            }

            return Task.CompletedTask;
        }
        catch (Exception e)
        {
            logger.LogError(e,"Quartz.net database create error!");
            throw;
        }
    }
}