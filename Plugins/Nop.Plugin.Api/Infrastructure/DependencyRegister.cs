using Autofac;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Plugin.Api.Services;

namespace Nop.Plugin.Api.Infrastructure
{
    using Nop.Core.Domain.Catalog;
    using Nop.Core.Domain.Common;
    using Nop.Core.Domain.Customers;
    using Nop.Core.Domain.Orders;
    using Nop.Plugin.Api.Converters;
    using Nop.Plugin.Api.Data;
    using Nop.Plugin.Api.Factories;
    using Nop.Plugin.Api.Helpers;
    using Nop.Plugin.Api.JSON.Serializers;
    using Nop.Plugin.Api.ModelBinders;
    using Nop.Plugin.Api.Services.Interface;
    using Nop.Plugin.Api.Services.Interfaces;
    //using Nop.Plugin.Api.Services.Interface;
    using Nop.Plugin.Api.Validators;
    using Nop.Plugin.Api.WebHooks;
    using Nop.Services.Residential.Helpers.ValidatorHelper;
    using Nop.Web.Framework.Infrastructure;
    using System;

    public class DependencyRegister : IDependencyRegistrar
    {
        private const string ObjectContextName = "nop_object_context_web_api";

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            this.RegisterPluginDataContext<ApiObjectContext>(builder, ObjectContextName);

            RegisterPluginServices(builder);

            RegisterModelBinders(builder);
        }

        private void RegisterModelBinders(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ParametersModelBinder<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(JsonModelBinder<>)).InstancePerLifetimeScope();
        }

        private void RegisterPluginServices(ContainerBuilder builder)
        {
            builder.RegisterType<ClientService>().As<IClientService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerApiService>().As<ICustomerApiService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryApiService>().As<ICategoryApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductApiService>().As<IProductApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductCategoryMappingsApiService>().As<IProductCategoryMappingsApiService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderApiService>().As<IOrderApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartItemApiService>().As<IShoppingCartItemApiService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderItemApiService>().As<IOrderItemApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributesApiService>().As<IProductAttributesApiService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductPictureService>().As<IProductPictureService>().InstancePerLifetimeScope();
            builder.RegisterType<ProductAttributeConverter>().As<IProductAttributeConverter>().InstancePerLifetimeScope();
            builder.RegisterType<NewsLetterSubscriptionApiService>().As<INewsLetterSubscriptionApiService>().InstancePerLifetimeScope();

            builder.RegisterType<MappingHelper>().As<IMappingHelper>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRolesHelper>().As<ICustomerRolesHelper>().InstancePerLifetimeScope();
            builder.RegisterType<JsonHelper>().As<IJsonHelper>().InstancePerLifetimeScope();
            builder.RegisterType<DTOHelper>().As<IDTOHelper>().InstancePerLifetimeScope();
            builder.RegisterType<NopConfigManagerHelper>().As<IConfigManagerHelper>().InstancePerLifetimeScope();

            builder.RegisterType<NopWebHooksLogger>().As<Microsoft.AspNet.WebHooks.Diagnostics.ILogger>().InstancePerLifetimeScope();

            builder.RegisterType<JsonFieldsSerializer>().As<IJsonFieldsSerializer>().InstancePerLifetimeScope();

            builder.RegisterType<FieldsValidator>().As<IFieldsValidator>().InstancePerLifetimeScope();

            builder.RegisterType<WebHookService>().As<IWebHookService>().SingleInstance();

            builder.RegisterType<ObjectConverter>().As<IObjectConverter>().InstancePerLifetimeScope();
            builder.RegisterType<ApiTypeConverter>().As<IApiTypeConverter>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryFactory>().As<IFactory<Category>>().InstancePerLifetimeScope();
            builder.RegisterType<ProductFactory>().As<IFactory<Product>>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerFactory>().As<IFactory<Customer>>().InstancePerLifetimeScope();
            builder.RegisterType<AddressFactory>().As<IFactory<Address>>().InstancePerLifetimeScope();
            builder.RegisterType<OrderFactory>().As<IFactory<Order>>().InstancePerLifetimeScope();
            builder.RegisterType<ShoppingCartItemFactory>().As<IFactory<ShoppingCartItem>>().InstancePerLifetimeScope();

            builder.RegisterType<Maps.JsonPropertyMapper>().As<Maps.IJsonPropertyMapper>().InstancePerLifetimeScope();

            builder.RegisterType<UtilityService>().As<IUtilityService>().InstancePerLifetimeScope();
            builder.RegisterType<ProfileApiService>().As<IProfileApiService>().InstancePerLifetimeScope();
            builder.RegisterType<IncidentApiService>().As<IIncidentApiService>().InstancePerLifetimeScope(); //Tony Liew 20190306 RDT-116
            builder.RegisterType<ValidatorHelper>().As<IValidatorHelper>().InstancePerLifetimeScope(); //Tony Liew 20190308 RDT-118
            builder.RegisterType<AnnouncementApiService>().As<IAnnouncementApiService>().InstancePerLifetimeScope(); //WKK 20190315 RDT-121
            builder.RegisterType<GeneralApiService>().As<IGeneralApiService>().InstancePerLifetimeScope(); //JK 20190322 RDT-166
            builder.RegisterType<FamilyTenantApiServices>().As<IFamilyTenantApiServices>().InstancePerLifetimeScope(); //Tony Liew 20190403 RDT-175 
            builder.RegisterType<VisitorApiService>().As<IVisitorApiService>().InstancePerLifetimeScope(); //WKK 20190411 RDT-189 [API] Visitor - Record History Listing
            builder.RegisterType<FacilityApiServices>().As<IFacilityApiServices>().InstancePerLifetimeScope(); //Tony Liew 20190418 RDT-202 

        }

        public virtual int Order
        {
            get { return Int16.MaxValue; }
        }
    }
}