namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Threading.Tasks;
    using Xunit;

    public class RecipientServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task CreateRecipientAsyncShouldReturnId()
        {
            var orderService = GetOrderService.Return(db);
            var recipientRepository = new Repository<Recipient>(db);

            var recipientService = new RecipientService(recipientRepository, orderService);

            var returnId = await recipientService.CreateRecipientAsync("Test", 
                                                                       "Testov",
                                                                       "TestTown", "Test12", "0000 000 000");

            var expected = 1;
            var actual = returnId;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteRecipientAsyncShouldReturnTrue()
        {
            var orderService = GetOrderService.Return(db);
            var recipientRepository = new Repository<Recipient>(db);

            var recipientService = new RecipientService(recipientRepository, orderService);

            var returnId = await recipientService.CreateRecipientAsync("Test",
                                                                       "Testov",
                                                                       "TestTown", "Test12", "0000 000 000");

            var isDeleted = await recipientService.DeleteRecipientAsync(new RecipientDeleteDtoModel
            {
                RecipientId = 1
            });


            Assert.True(isDeleted);
        }

        [Fact]
        public async Task FindRecipientAsyncShouldReturnTrue()
        {
            var orderService = GetOrderService.Return(db);
            var recipientRepository = new Repository<Recipient>(db);

            var recipientService = new RecipientService(recipientRepository, orderService);

            var returnId = await recipientService.CreateRecipientAsync("Test",
                                                                       "Testov",
                                                                       "TestTown", "Test12", "0000 000 000");

            var recipientId = await recipientService.FindRecipientAsync("0000 000 000");

            var expected = 1;
            var actual = returnId;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllRecipientsAsyncShouldReturnOne()
        {
            var orderService = GetOrderService.Return(db);
            var recipientRepository = new Repository<Recipient>(db);

            var recipientService = new RecipientService(recipientRepository, orderService);

            var returnId = await recipientService.CreateRecipientAsync("Test",
                                                                       "Testov",
                                                                       "TestTown", "Test12", "0000 000 000");

            var recipientsAll = await recipientService.GetAllRecipientsAsync();

            var expected = 1;
            var actual = recipientsAll.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetRecipientByIdAsyncShouldReturnFirstName()
        {
            var orderService = GetOrderService.Return(db);
            var recipientRepository = new Repository<Recipient>(db);

            var recipientService = new RecipientService(recipientRepository, orderService);

            var returnId = await recipientService.CreateRecipientAsync("Test",
                                                                       "Testov",
                                                                       "TestTown", "Test12", "0000 000 000");

            var recipient = await recipientService.GetRecipientByIdAsync(1);

            var expected = "Test";
            var actual = recipient.FirstName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetRecipientParamsForUpdateAsyncShouldReturnFirstName()
        {
            var orderService = GetOrderService.Return(db);
            var recipientRepository = new Repository<Recipient>(db);

            var recipientService = new RecipientService(recipientRepository, orderService);

            var returnId = await recipientService.CreateRecipientAsync("Test",
                                                                       "Testov",
                                                                       "TestTown", "Test12", "0000 000 000");

            var recipient = await recipientService.GetRecipientParamsForUpdateAsync(new RecipientUpdateOutputDtoModel
            {
                RecipientId = 1
            });

            var expected = "Test";
            var actual = recipient.FirstName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task IfRecipientExistsAsyncShouldReturnTrue()
        {
            var orderService = GetOrderService.Return(db);
            var recipientRepository = new Repository<Recipient>(db);

            var recipientService = new RecipientService(recipientRepository, orderService);

            var returnId = await recipientService.CreateRecipientAsync("Test",
                                                                       "Testov",
                                                                       "TestTown", "Test12", "0000 000 000");

            var ifExists = await recipientService.IfRecipientExistsAsync("0000 000 000");

            Assert.True(ifExists);
        }

        [Fact]
        public async Task UpdateRecipientAsyncShouldReturnTrue()
        {
            var orderService = GetOrderService.Return(db);
            var recipientRepository = new Repository<Recipient>(db);

            var recipientService = new RecipientService(recipientRepository, orderService);

            var returnId = await recipientService.CreateRecipientAsync("Test",
                                                                       "Testov",
                                                                       "TestTown", "Test12", "0000 000 000");

            var isUpdated = await recipientService.UpdateRecipientAsync(new RecipientUpdateInputDtoModel
            {
                RecipientId = 1,
                FirstName = "Test1",
                LastName = "Testov",
                RecipientTown = "TestTown",
                Address = "Test12",
                Phone = "0000 000 000"
            });

            Assert.True(isUpdated);
        }
    }
}
