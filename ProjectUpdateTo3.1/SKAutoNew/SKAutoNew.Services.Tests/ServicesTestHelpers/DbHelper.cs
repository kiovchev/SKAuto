namespace SKAutoNew.Services.Tests.ServicesTestHelpers
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Data;
    using System;

    public static class DbHelper
    {
        public static SKAutoDbContext GetDb()
        {
            var options = new DbContextOptionsBuilder<SKAutoDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var db = new SKAutoDbContext(options);

            return db;
        }
    }
}
