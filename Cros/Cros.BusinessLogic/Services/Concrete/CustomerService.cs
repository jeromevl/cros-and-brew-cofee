using AutoMapper;
using Cros.BusinessLogic.Dtos;
using Cros.BusinessLogic.Services.Interfaces;
using Cros.DataAccess;
using Cros.DataAccess.Models;
using Cros.DataAccess.Repositories.Interfaces;

namespace Cros.BusinessLogic.Services.Concrete
{
    public class CustomerService(ICustomerRepository customerRepository,
        IUnitOfWork uow,
        IMapper mapper) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IUnitOfWork _uow = uow;
        private readonly IMapper _mapper = mapper;

        public async Task<CustomerDto?> GetCustomerByIdAsync(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Parameter 'customerId' must be greater than zero.");

            return _mapper.Map<CustomerDto?>(await _customerRepository.GetByIdAsync(customerId));
        }

        public async Task<PaginationResultDto<CustomerDto>> GetCustomersAsync(int pageNumber, int pageSize, string searchText)
        {
            var customers = await _customerRepository.GetAllAsync(pageNumber, pageSize, searchText);
            return _mapper.Map<PaginationResultDto<CustomerDto>>(customers);
        }

        public async Task CreateCustomerAsync(CustomerDto newCustomer)
        {
            var newCustomerToAdd = _mapper.Map<Customer>(newCustomer);
            await _customerRepository.CreateAsync(newCustomerToAdd);
            await _uow.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(CustomerDto existingCustomer)
        {
            if (!await _customerRepository.ExistsAsync(existingCustomer.Id))
                throw new ArgumentException("Customer does not exist");

            var existingCustomerToUpdate = _mapper.Map<Customer>(existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomerToUpdate);
            await _uow.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int customerId, bool isSoftDelete = true)
        {
            if (!await _customerRepository.ExistsAsync(customerId))
                throw new ArgumentException("Customer does not exist");

            await _customerRepository.DeleteAsync(customerId, isSoftDelete);
            await _uow.SaveChangesAsync();
        }
    }
}