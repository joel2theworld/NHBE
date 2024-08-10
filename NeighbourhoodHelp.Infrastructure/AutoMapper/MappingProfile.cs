using AutoMapper;
using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NeighbourhoodHelp.Model.DTOs;

namespace NeighbourhoodHelp.Infrastructure.AutoMapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, LoginDto>();

            CreateMap<AppUser, GetAppUserDto>();

            CreateMap<Agent, GetAgentDto>();

           
            CreateMap<SignUpDto, AppUser>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Assuming Email is used as the username
            CreateMap<Errand, GetErrandDto>()
                .ForMember(dest => dest.AppUser, opt => opt.MapFrom(src => src.AppUser)); // Map AppUser to ErrandDto


            /*CreateMap<AgentSignUpDto, Agent>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)); // Assuming Email is used as the username
            */
            // Add more mappings if needed
            CreateMap<PriceNegotiation, PriceNegotiationDto>();
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUser, AgentDto>();
            CreateMap<Agent, AgentDto>();
            CreateMap<Agent, PriceDto>();
            CreateMap<Errand, PendingErrandDto>();
                /*.ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))*/
                // If you have additional properties that match between Errand and ErrandDto, 
                // you can map them here as well.
                ;
        }
    }
}
