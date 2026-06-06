using AutoMapper;
using Cros.BusinessLogic.Dtos;
using Cros.BusinessLogic.Helpers;
using Cros.BusinessLogic.Services.Concrete;
using Cros.DataAccess;
using Cros.DataAccess.Repositories.Interfaces;
using Moq;

namespace Cros.BusinessLogic.Tests
{
    public class CustomerServiceTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetCustomerByIdAsync_IdNotPositive_ThrowsArgumentException(int id)
        {
            // Arrange
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var customerService = new CustomerService(mockCustomerRepository.Object, mockUnitOfWork.Object, mockMapper.Object);

            // Act
            Exception ex = await Record.ExceptionAsync(() => customerService.GetCustomerByIdAsync(id));

            // Assert
            Assert.IsType<ArgumentException>(ex);
        }

        [Fact]
        public async Task DeleteCustomerAsync_Existing_DeletesCustomer()
        {
            // Arrange
            var mockCustomerRepository = new Mock<ICustomerRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperBusinessLogicProfile()));
            var mapper = config.CreateMapper();

            var customerService = new CustomerService(mockCustomerRepository.Object, mockUnitOfWork.Object, mapper);
            mockCustomerRepository.Setup(m => m.ExistsAsync(1)).ReturnsAsync(true);

            // Act
            await customerService.DeleteCustomerAsync(1, true);

            // Assert
            mockCustomerRepository.Verify(m => m.DeleteAsync(1, true));
        }
        //
        // Helper classes
        //
        internal class CustomerFactory
        {
            internal static CustomerDto CreateNonNew(int customerId, string customerNo, string firstName, string lastName)
            {
                return new CustomerDto()
                {
                    Id = customerId,
                    CustomerNo = customerNo,
                    FirstName = firstName,
                    LastName = lastName
                };
            }
        }
    }
}