using DataModel.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Properties...
        GenericRepository<Actor> ActorRepository { get; }
        GenericRepository<Movie> MovieRepository { get; }
        GenericRepository<MovieActor> MovieActorRepository { get; }
        GenericRepository<MovieProducer> MovieProducerRepository { get; }
        GenericRepository<Producer> ProducerRepository { get; }
        #endregion

        #region Methods..
        /// <summary>  
        /// Save method.  
        /// </summary>  
        void Save();
        #endregion
    }  
}
