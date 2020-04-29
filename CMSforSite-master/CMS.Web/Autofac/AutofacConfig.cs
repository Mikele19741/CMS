using Autofac;
using CMS.Domain.Classes;
using CMS.Domain.Interfaces;
using System.Web.Mvc;
using Autofac.Integration.Mvc;
using Autofac.Core;
using CMS.Domain.Interface;

namespace CMS.Web.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            // получаем экземпляр контейнера
            var builder = new ContainerBuilder();

            // регистрируем контроллер в текущей сборке
           builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // регистрируем споставление типов
            builder.RegisterType<PostsRepository>().As<IPosts>();
            builder.RegisterType<AdminRepository>().As<IAdmin>();
            builder.RegisterType<FeedbackRepository>().As<IFeedback>();
            builder.RegisterType<CategoriesRepository>().As<ICategories>();
            builder.RegisterType<UsligiRepository>().As<IUsligi>();
            builder.RegisterType<PhotoGalleryRepository>().As<IPhoto>();

            // создаем новый контейнер с теми зависимостями, которые определены выше
            var container = builder.Build();

            // установка сопоставителя зависимостей
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}