using AutoMapper;
using DLL.DTOS;
using DLL.DTOS.Search;
public static class AutoMapperConfig
{
    public static IMapper Initialize()
    {
        var mapperConfig = new MapperConfiguration(cfg =>   
        {
            cfg.CreateMap<Client, ClientDTO>();
            cfg.CreateMap<ClientEmail, ClientEmailDTO>();
            cfg.CreateMap<ClientContact, ClientContactDTO>();
            cfg.CreateMap<Order, OrderDTO>();
            cfg.CreateMap<ClientAddresses, ClientAddressesDTO>();
            cfg.CreateMap<ClientAddressCreateDTO, ClientAddresses>();
            cfg.CreateMap<ClientContactCreateDTO, ClientContact>();
            cfg.CreateMap<ClientEmailCreateDTO, ClientEmail>(); 
            cfg.CreateMap<OrderCreateDTO, OrderDTO>();
            cfg.CreateMap<Client, SearchResultDTO>();
            cfg.CreateMap<Client,ClientDDTO>();
        });
            
        IMapper mapper = mapperConfig.CreateMapper();
        return mapper;
    }
}
