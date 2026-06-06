using AutoMapper;
using Cros.DataAccess.Entities;
using Cros.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using dataModels = Cros.DataAccess.Models;

namespace Cros.DataAccess.Repositories.Concrete
{
    public class CustomerRepository(CrosDbContext dbContext,
        IMapper mapper) : ICustomerRepository
    {
        private readonly CrosDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<dataModels.Customer?> GetByIdAsync(int customerId)
        {
            var foundCustomer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == customerId);
            return _mapper.Map<dataModels.Customer>(foundCustomer);
        }

        public async Task<dataModels.PaginationResult<dataModels.Customer>> GetAllAsync(int pageNumber, int pageSize, string searchText)
        {
            var customers = _dbContext.Customers.Select(c => c);

            if (!String.IsNullOrWhiteSpace(searchText))
            {
                customers = customers.Where(c => (c.CustomerNo != null && c.CustomerNo.Contains(searchText))
                    || c.FirstName.Contains(searchText)
                    || c.LastName.Contains(searchText));
            }

            var total = await customers.CountAsync();
            if (pageSize > 0)
                customers = customers.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var mapeedCustomers = _mapper.Map<List<dataModels.Customer>>(customers);

            var result = new dataModels.PaginationResult<dataModels.Customer>
            {
                Items = mapeedCustomers,
                Total = total,
                PageSize = pageSize,
                CurrentPage = pageNumber
            };

            return result;
        }

        public async Task CreateAsync(dataModels.Customer newCustomer)
        {
            var customerToAdd = _mapper.Map<Customer>(newCustomer);
            _dbContext.Add(customerToAdd);
        }

        public async Task UpdateAsync(dataModels.Customer existingCustomer)
        {
            var customerToUpdate = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == existingCustomer.Id);
            if (customerToUpdate != null)
            {
                _mapper.Map(existingCustomer, customerToUpdate);
                _dbContext.Update(customerToUpdate);
            }
        }

        public async Task DeleteAsync(int customerId, bool isSoftDelete = true)
        {
            var customerToDelete = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Id == customerId);
            if (customerToDelete != null)
            {
                if (isSoftDelete)
                {
                    customerToDelete.DeletedAt = DateTime.UtcNow;
                    _dbContext.Update(customerToDelete);
                }
                else
                {
                    _dbContext.Remove(customerToDelete);
                }
            }
        }

        public async Task<bool> ExistsAsync(int customerId)
        {
            return await _dbContext.Customers.AnyAsync(e => e.Id == customerId);
        }
    }
}