using Cros.DataAccess.Models;

namespace Cros.DataAccess.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int patronId);
        Task<PaginationResult<Customer>> GetAllAsync(int pageNumber, int pageSize, string searchText);

        Task CreateAsync(Customer newCustomer);
        Task UpdateAsync(Customer existingCustomer);
        Task DeleteAsync(int customerId, bool isSoftDelete = true);

        Task<bool> ExistsAsync(int customerId);
    }
}