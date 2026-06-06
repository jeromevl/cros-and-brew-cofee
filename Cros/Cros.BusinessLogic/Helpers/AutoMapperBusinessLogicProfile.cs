using AutoMapper;
using Cros.BusinessLogic.Dtos;
using Cros.DataAccess.Models;

namespace Cros.BusinessLogic.Helpers
{
    public class AutoMapperBusinessLogicProfile : Profile
    {
        public AutoMapperBusinessLogicProfile()
            : this("AutoMapperBusinessLogicProfile")
        {
        }

        protected AutoMapperBusinessLogicProfile(string profileName)
            : base(profileName)
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();

            CreateMap(typeof(PaginationResult<>), typeof(PaginationResultDto<>));
        }
    }
}