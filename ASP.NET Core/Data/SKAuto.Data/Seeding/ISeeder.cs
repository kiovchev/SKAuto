namespace SKAuto.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(SKAutoDbContext dbContext, IServiceProvider serviceProvider);
    }
}
