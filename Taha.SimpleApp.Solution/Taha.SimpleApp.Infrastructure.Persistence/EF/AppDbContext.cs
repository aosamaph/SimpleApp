using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Taha.SimpleApp.Domain.Aggregates;
using Taha.SimpleApp.Domain.Entities;

namespace Taha.SimpleApp.Infrastructure.Persistence.EF
{
    public class AppDbContext : DbContext
    {
        private static bool _initialized = false;
        private static readonly object _lock = new();
        private readonly ILogger<AppDbContext> _logger;

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, ILogger<AppDbContext> logger) : base(options)
        {
            if (!_initialized)
            {
                lock (_lock)
                {
                    if (!_initialized)
                        Initialize();
                }
            }
            _logger = logger;
        }
        public void Initialize()
        {
            try
            {
                Database.EnsureCreated();
                SetInitialized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
            }
        }

        private static void SetInitialized()
        {
            _initialized = true;
        }
    }
}