using AutoMapper;
using supermarketFrontEnd.Models;
using supermarketFrontEnd.Models.Domain.Services;
using supermarketFrontEnd.Models.ViewModels;
using supermarketFrontEnd.Resources;
using supermarketFrontEnd.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace supermarketFrontEnd
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IVariantService, VariantService>();
            container.RegisterType<IVariantOptionService, VariantOptionService>();
            container.RegisterType<ISKUService, SKUService>();
            container.RegisterType<IProductVariantService, ProductVariantService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICompositeProductService, CompositeProductService>();

            var config = new MapperConfiguration(cfg =>
            {
                //Create all maps here
                cfg.CreateMap<dynamic, APIError>();
                cfg.CreateMap<dynamic, Category>();
                cfg.CreateMap<dynamic, Variant>();
                cfg.CreateMap<dynamic, VariantOption>();
                cfg.CreateMap<dynamic, SKU>();
                cfg.CreateMap<dynamic, ProductVariant>();
                cfg.CreateMap<dynamic, CompositeProduct>();
                cfg.CreateMap<dynamic, Product>();
                cfg.CreateMap<Product, CreateProductViewModel>();
                cfg.CreateMap<CreateProductViewModel, Product>();

                //...
            });

            IMapper mapper = config.CreateMapper();

            container.RegisterInstance(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}