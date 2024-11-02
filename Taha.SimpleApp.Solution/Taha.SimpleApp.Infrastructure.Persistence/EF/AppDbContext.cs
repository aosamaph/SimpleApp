using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Taha.SimpleApp.Domain.Aggregates;
using Taha.SimpleApp.Domain.Entities;
using Taha.SimpleApp.Domain.ValueObjects;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired();

            modelBuilder.Entity<Category>()
                .HasData(
                    new Category("Sample Category") { Id = 1 }
                );

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(Product.DESCRIPTION_MAX_LENGTH);
                entity.Property(e => e.Image);
                
                entity.ComplexProperty(e => e.Price, sa =>
                {
                    sa.Property(p => p.Price).IsRequired().HasColumnName("Price");
                    sa.Property(p => p.Currency).IsRequired().HasColumnName("Currency");
                });
            });
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