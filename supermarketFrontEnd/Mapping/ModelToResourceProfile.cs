using AutoMapper;
using supermarketFrontEnd.Models;
using supermarketFrontEnd.Models.ViewModels;
using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace supermarketFrontEnd.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<dynamic, APIError>();
            CreateMap<Product, CreateProductViewModel>();
        }
    }
}