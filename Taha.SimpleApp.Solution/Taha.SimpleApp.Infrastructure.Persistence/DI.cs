﻿using Microsoft.EntityFrameworkCore;
using Taha.SimpleApp.Application.Interfaces;
using Taha.SimpleApp.Domain.Aggregates;
using Taha.SimpleApp.Domain.Entities;
using Persistence = Taha.SimpleApp.Infrastructure.Persistence;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DI
    {
        public static IServiceCollection AddOnlyEFServices(this IServiceCollection services, string? connectionString) =>
            services.AddScoped<IRepository<Category, int>, Persistence.EF.CategoryRepository>()
                .AddScoped<IRepository<Product, int>, Persistence.EF.ProductRepository>()
                .AddDbContext<Persistence.EF.AppDbContext>(options =>
                    options.UseSqlServer(connectionString));

        public static IServiceCollection AddOnlyStaticListsServices(this IServiceCollection services) =>
            services.AddSingleton<IRepository<Category, int>, Persistence.StaticLists.CategoryRepository>()
                .AddSingleton<IRepository<Product, int>, Persistence.StaticLists.ProductRepository>();

    }
}
