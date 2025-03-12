using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Concrete;
using Business.Concretes;
using Business.Mapping;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using System.Reflection;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Servis ve Dal sınıflarının tek bir metodda kaydı
            RegisterServiceAndDal(builder);

            // AutoMapper Konfigürasyonu
            RegisterAutoMapper(builder);

            // Interceptors ve Aspectler için yapılandırma
            RegisterInterceptors(builder);
        }

        private void RegisterServiceAndDal(ContainerBuilder builder)
        {
            // Servis ve DataAccess katmanındaki sınıfları toplu şekilde kaydetmek için
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<AlanManager>().As<IAlanService>().SingleInstance();
            builder.RegisterType<EfAlanDal>().As<IAlanDal>().SingleInstance();
            builder.RegisterType<BolumManager>().As<IBolumService>().SingleInstance();
            builder.RegisterType<EfBolumDal>().As<IBolumDal>().SingleInstance();
            builder.RegisterType<EfPozisyonDal>().As<IPozisyonDal>().SingleInstance();
            builder.RegisterType<PozisyonManager>().As<IPozisyonService>().SingleInstance();

            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();
        }

        private void RegisterAutoMapper(ContainerBuilder builder)
        {
            // AutoMapper konfigürasyonunu buradan yapıyoruz
            builder.Register(c =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfile()); // MappingProfile'ı burada belirtiyoruz
                });
                return config.CreateMapper(); // AutoMapper'ı döndürüyoruz.
            }).As<IMapper>().SingleInstance();
        }

        private void RegisterInterceptors(ContainerBuilder builder)
        {
            // AspectInterceptorSelector'ı ve bütün interceptorları kaydediyoruz
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions
                   {
                       Selector = new AspectInterceptorSelector()
                   })
                   .SingleInstance();
        }
    }
}
