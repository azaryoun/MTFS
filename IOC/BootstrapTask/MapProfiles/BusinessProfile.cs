using AutoMapper;
using MTFS.Business.Domain.Model;
using MTFS.Business.Dtos.DtoClasses;
using System;

namespace MTFS.Business.Bootstrapper.MapProfiles
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {

            #region All DropdownList 

            CreateMap<Country, DdlDto>().ForMember(dest => dest.text, opts => opts.MapFrom(src => src.countryName)); 

            CreateMap<Location, DdlDto>().ForMember(dest => dest.text, opts => opts.MapFrom(src => src.locationName));
            CreateMap<Customer, DdlDto>().ForMember(dest => dest.text, opts => opts.MapFrom(src => src.fullName));
             
            CreateMap<Currency, DdlDto>().ForMember(dest => dest.text, opts => opts.MapFrom(src => src.name));

            CreateMap<Transporttype, DdlDto>().ForMember(dest => dest.text, opts => opts.MapFrom(src => src.typeName)); 

            CreateMap<AgentCarrier , DdlWithParentDto>()
                .ForMember(dest => dest.text, opts => opts.MapFrom(src =>$"{src.carrier.fullName} _Under Agent_ {src.agent.fullName}")) 
                .ForMember(dest => dest.parentId, opts => opts.MapFrom(src => src.agentId))
                .ForMember(dest => dest.id , opts => opts.MapFrom(src => src.carrierId));

            #endregion

            #region Administration

            CreateMap<Accessgroup, AccessgroupDto>()
                    .ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId));

            CreateMap<AccessgroupDto, Accessgroup>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId));

            CreateMap<Menuitem, MenuitemDto>()
                .ForMember(dest => dest.itemText, opts => opts.MapFrom(src => $"{src.menutitle.titleText}/{src.itemText}"));

            CreateMap<Menuitem, MenuitemMenuDto>();

            CreateMap<Menutitle, MenutitleDto>();

            CreateMap<User, UserDto>().ForMember(dest => dest.companyName, opts => opts.MapFrom(src => src.company.companyName))
                .ForMember(dest => dest.companyId, opts => opts.MapFrom(src => src.company.id))
                .ForMember(dest => dest.companyLogo, opts => opts.MapFrom(src => Convert.ToBase64String(src.company.logo)))
                .ForMember(dest => dest.companyLogoMime, opts => opts.MapFrom(src => src.company.logoMime));

            CreateMap<User, UserInfoDto>().ForMember(dest => dest.companyName, opts => opts.MapFrom(src => src.company.companyName))
                .ForMember(dest => dest.companyLogo, opts => opts.MapFrom(src => Convert.ToBase64String(src.company.logo)))
                .ForMember(dest => dest.companyLogoMime, opts => opts.MapFrom(src => src.company.logoMime));

            CreateMap<Menutitle, MenutitleDto>();

            #endregion

            #region BaseInfo 

            #region Location

            CreateMap<Location, GetLocationsDto>().ForMember(dest => dest.countryName, opts => opts.MapFrom(src => src.country.countryName))
                                      .ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId));

            CreateMap<Location, GetLocationDto>();

            CreateMap<GetLocationDto, Location>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId));

            #endregion

            #region Packagetype

            CreateMap<Packagetype, GetPackagetypesDto>().ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId));

            CreateMap<Packagetype, GetPackagetypeDto>();

            CreateMap<GetPackagetypeDto, Packagetype>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId));
            
            #endregion

            #region Lookup  

            CreateMap<Transporttype, TransporttypeDto>();

            #endregion

            #region Customer

            CreateMap<Customer, GetCustomersDto>().ForMember(dest => dest.email, opts => opts.MapFrom(src => src.email1))
                    .ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId));

            CreateMap<Customer, GetCustomerDto>();

            CreateMap<GetCustomerDto, Customer>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId));

            CreateMap<Customer, GetCustomerTypeDto>();

            #endregion 

            #region AgentCarriers

            CreateMap<AgentCarrier, GetAgentCarriersDto>().ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId))
                                                           .ForMember(dest => dest.carrierName, opts => opts.MapFrom(src => src.carrier.fullName))
                                                           .ForMember(dest => dest.locationName, opts => opts.MapFrom(src => src.location.locationName));

            CreateMap<AgentCarrier, GetAgentCarrierDto>();

            CreateMap<GetAgentCarrierDto, AgentCarrier>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId));

            #endregion
            
            #endregion

            #region SalesMarketting

            #region Negotiation


            CreateMap<Negotiation, GetNegotiationsDto>()
                .ForMember(dest => dest.placeofReceiptLocationName, opts => opts.MapFrom(src => src.placeofReceiptLocation.locationName))
                .ForMember(dest => dest.placeofDeliveryLocationName, opts => opts.MapFrom(src => src.placeofDeliveryLocation.locationName))
                .ForMember(dest => dest.contractorName, opts => opts.MapFrom(src => src.contractor.fullName))
                .ForMember(dest => dest.currencySymbol, opts => opts.MapFrom(src => src.currency.symbol))
                .ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId));

            CreateMap<Negotiation, GetNegotiationDto>().ForMember(x => x.state, opt => opt.Ignore())
                                                               .ForMember(x => x.contractor, opt => opt.Ignore());

            CreateMap<GetNegotiationDto, Negotiation>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId))
                                                               .ForMember(x => x.state, opt => opt.Ignore())
                                                               .ForMember(x => x.contractor, opt => opt.Ignore());

            #endregion

            #region Negotiation Plan

            CreateMap<Negotiationplan, GetNegotiationplansDto>().ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId));

            CreateMap<Negotiationplan, GetNegotiationplanDto>();

            CreateMap<GetNegotiationplanDto, Negotiationplan>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId));

            #endregion

            #region Negotiationplanroute

            CreateMap<Negotiationplanroute, GetNegotiationplanroutesDto>()
                        .ForMember(dest => dest.fromLocationName , opts => opts.MapFrom(src => src.fromLocation.locationName))
                        .ForMember(dest => dest.toLocationName, opts => opts.MapFrom(src => src.toLocation.locationName))
                        .ForMember(dest => dest.transporttypeName, opts => opts.MapFrom(src => src.transporttype.typeName))
                        .ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId));

            CreateMap<Negotiationplanroute, GetNegotiationplanrouteDto>();

            CreateMap<GetNegotiationplanrouteDto, Negotiationplanroute>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId));

            #endregion

            #region NegotiationplanrouteCF

            CreateMap<NegotiationplanrouteCF, GetNegotiationplanrouteCFsDto>()
            .ForMember(dest => dest.agentName, opts => opts.MapFrom(src => src.agent.fullName))
            .ForMember(dest => dest.carrierrName, opts => opts.MapFrom(src => src.carrier.fullName))
            .ForMember(dest => dest.forwarderName , opts => opts.MapFrom(src => src.forwarder .fullName))
            .ForMember(dest => dest.userId, opts => opts.MapFrom(src => src.createdUserId));

            CreateMap<NegotiationplanrouteCF, GetNegotiationplanrouteCFDto>();

            CreateMap<GetNegotiationplanrouteCFDto, NegotiationplanrouteCF>().ForMember(dest => dest.createdUserId, opts => opts.MapFrom(src => src.userId));

            #endregion

            #endregion

        }
    }
}
