using AutoMapper;
using Cros.DataAccess.Entities;
using dataModels = Cros.DataAccess.Models;

namespace Cros.DataAccess.Helpers
{
    public class AutoMapperDataAccessProfile : Profile
    {
        public AutoMapperDataAccessProfile()
            : this("AutoMapperDataAccessProfile")
        {
        }

        protected AutoMapperDataAccessProfile(string profileName)
            : base(profileName)
        {
            CreateMap<Customer, dataModels.Customer>().ReverseMap();
        }
    }
}