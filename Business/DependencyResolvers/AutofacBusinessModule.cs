using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Concrete;
using Business.Concretes;
using Business.Mapping;
using Core.Aspects.Autofac.Logger;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstracts;
using DataAccess.Concretes.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();
            builder.RegisterType<AlanManager>().As<IAlanService>().SingleInstance();
            builder.RegisterType<EfAlanDal>().As<IAlanDal>().SingleInstance();


            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();
            builder.RegisterType<LoggerAspect>();  // LoggerAspect'i burada kaydediyoruz


            // AutoMapper Konfigürasyonu
            builder.Register(c =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    // Mapping profilini buradan ekleyebilirsiniz
                    cfg.AddProfile(new MappingProfile()); // MappingProfile'ı projede oluşturduğunuz yerden çağırıyorsunuz.
                });
                return config.CreateMapper(); // AutoMapper'ı döndürüyoruz.
            }).As<IMapper>().SingleInstance();


            // Interceptors ve Aspectler için yapılandırma
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
        }
    }
}
