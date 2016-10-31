using AutoMapper;
using IoF_Admin.Models;
using IoF_Admin.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin
{
    public class AutoMapperProfileConfiguration : Profile
    {
        protected override void Configure()
        {
            CreateMap<Aquarium, ConfigurationResourceModel>()
                .ForMember(dest => dest.AquariumId, opts => opts.MapFrom(src => src.HardwareID))
                .ForMember(dest => dest.Fish, opts => opts.ResolveUsing(src => src.Fishes))
                .ReverseMap(); ;
            CreateMap<Office, OfficeResourceModel>()
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.CountryCode))
                .ReverseMap();
            CreateMap<Fish, FishMappingResourceModel>().ReverseMap();
            //CreateMap<List<Fish>, List<FishMappingResourceModel>>().ReverseMap();


        }

    }
}
