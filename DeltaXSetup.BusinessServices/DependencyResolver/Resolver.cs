using BusinessServices.Actor;
using BusinessServices.Producer;
using BusinessServices.Movies;
using DependencyResolver;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.UnitOfWork;

namespace BusinessServices.DependencyResolver
{
    [Export(typeof(IComponent))]
    public class Resolver : IComponent
    {
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IActorServices, ActorServices>();
            registerComponent.RegisterType<IProducerServices, ProducerServices>();
            registerComponent.RegisterType<IMoviesServices, MoviesServices>();
            registerComponent.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
