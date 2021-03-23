using AutoMapper;
using GuestApp.DTO;
using GuestApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuestApp.Utility
{
    public class Mapper
    {
        public static T2 EventMapper<T1, T2>(T1 sourceType, T2 destinationType) where T1 : IEvent where T2 : IEvent
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ForMember(dest => dest.Id, opt => opt.Condition(source => source.Id != 0));
            });

            IMapper mapper = config.CreateMapper();
            destinationType = mapper.Map<T1, T2>(sourceType);

            return destinationType;
        }

        public static T2 UserMapper<T1, T2>(T1 sourceType, T2 destinationType) where T1 : IUser where T2 : IUser
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<T1, T2>().ForMember(dest => dest.Id, opt => opt.Condition(source => source.Id != null));
            });

            IMapper mapper = config.CreateMapper();
            destinationType = mapper.Map<T1, T2>(sourceType);

            return destinationType;
        }

    }
}
