using AutoMapper;
using CaimanProject.Models;
using CaimanProject.VM;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaimanProject.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Member, MembersDTO>();
            Mapper.CreateMap<MembersDTO, Member>();
            Mapper.CreateMap<NoteP, NotePDTO>();
            Mapper.CreateMap<NotePDTO, NoteP>();
           /* IQueryable<IdentityRole> MembersDTOs = null;*/    
         
            Mapper.CreateMap<Projet, ProjetsDTO>()
                .ForMember(dto => dto.MembersDTOs, opt => opt.MapFrom(x => x.ProjetMembers.Select(y=>y.Member).ToList()));
            Mapper.CreateMap<ProjetMember, MembersDTO>();
            Mapper.CreateMap<Projet, ProjetDetailDTO>()
                .ForMember(dto => dto.MembersDTOs, opt => opt.MapFrom(x => x.ProjetMembers.Select(y => y.Member).ToList()))
                .ForMember(dto => dto.NotePDTOs, opt => opt.MapFrom(x => x.NotePs));
            Mapper.CreateMap<Projet, NoteAddProDetailDTO>()
                .ForMember(dto => dto.NotePDTOs, opt => opt.MapFrom(x => x.NotePs));

        }
    }
}