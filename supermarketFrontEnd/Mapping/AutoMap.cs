using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Mapping
{
    public static class AutoMap
    {
        public static IMapper Mapper { get; set; }

        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(
               config =>
               {
                   config.AddProfile<ModelToResourceProfile>();
               });

            Mapper = mapperConfiguration.CreateMapper();
        }

    }
}