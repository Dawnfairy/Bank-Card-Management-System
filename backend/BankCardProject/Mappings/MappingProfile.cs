using AutoMapper;
using BankCardProject.DTOs;
using BankCardProject.Models;

namespace BankCardProject.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreditCard, CreditCardDto>();
            CreateMap<CreditCardDto, CreditCard>().ForMember(dest => dest.Id, opt => opt.Ignore()); // Id'nin maplenmesini engellemek için
            CreateMap<BankCard, BankCardDto>();
            CreateMap<BankCardDto, BankCard>().ForMember(dest => dest.Id, opt => opt.Ignore()); // Id'nin maplenmesini engellemek için
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>().ForMember(dest => dest.Id, opt => opt.Ignore());

        }
    }
}
