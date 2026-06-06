using Cros.BusinessLogic.Dtos;

namespace Cros.BusinessLogic.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerByIdAsync(int customerId);
        Task<PaginationResultDto<CustomerDto>> GetCustomersAsync(int pageNumber, int pageSize, string searchText);

        Task CreateCustomerAsync(CustomerDto newCustomer);
        Task UpdateCustomerAsync(CustomerDto existingCustomer);
        Task DeleteCustomerAsync(int customerId, bool isSoftDelete = true);
    }
}