
using EventHub.Data;
using Microsoft.EntityFrameworkCore;

using var context = new EventHubContext();

context.Database.Migrate();

Console.WriteLine("Database created successfully!");
