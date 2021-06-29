using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using SakaryaBel.Data.Repository;
using SakaryaBel.Core.DomainModel.Entities;
using SakaryaBel.Data.UnitOfWork;
using EducationProject.Data.UnitOfWork;
using SakaryaBel.Services.IService;
using SakaryaBel.Services.Service;

namespace SakaryaBel.IOC
{
    public static class IOCExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
        }

        public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
    }

    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            //container.BindInRequestScope<IGenericRepository<User>, GenericRepository<User>>();
            //container.BindInRequestScope<IGenericRepository<Role>, GenericRepository<Role>>();

            container.BindInRequestScope<IUnitOfWork, UnitOfWork>();

            //container.BindInRequestScope<IUserService, UserService>();
            //container.BindInRequestScope<IRoleService, RoleService>();
        }
    }
}