using ApiApp.Models.Entities;
using ApiApp.Models.RequestDTO;
using ApiApp.Models.ResponseDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp.Models
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AuthorAddRequest, Author>()
                //المتغير اللي قيمته مش مبعةته //this is the value i want to send
                .ForMember(dest=>dest.IsDeleted, opt=>opt.MapFrom(y=>false) );

            CreateMap<AuthorUpdateRequestDTO, Author>();

            CreateMap<Author, AuthorReponseDTO>()
                .ForMember(dest=>dest.BookCount,
                opt=>opt.MapFrom(s=>s.Books.Count()+1));

            CreateMap<UserAddTDO, Users>()
                .ForMember(m => m.UserId, op => op.MapFrom(s => Guid.NewGuid()));
        }
    }
}
