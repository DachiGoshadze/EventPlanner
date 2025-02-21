using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Domain.Data
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<User> Users => Set<User>(); 
        public DbSet<AuthenticationCodes> AuthCodes => Set<AuthenticationCodes>(); 
        public DbSet<Event> Events => Set<Event>();
        public DbSet<EventsParticipantsQueue> eventsParticipantsQueue => Set<EventsParticipantsQueue>();
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }
    }
}
