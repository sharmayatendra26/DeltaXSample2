using DataModel.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.UnitOfWork
{
    /// <summary>
    /// Unit of Work class responsible for DB transactions
    /// </summary>
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private member variables...
        private DeltaXEntities _context = null;
        private GenericRepository<Actor> _ActorRepository;
        private GenericRepository<Movie> _MovieRepository;
        private GenericRepository<MovieActor> _MovieActorRepository;
        private GenericRepository<MovieProducer> _MovieProducerRepository;
        private GenericRepository<Producer> _ProducerRepository;

        #endregion

        public UnitOfWork()
        {
            _context = new DeltaXEntities();
        }

        #region Public Repository Creation properties...

        /// <summary>
        /// Get Property for Actor repository
        /// </summary>
        public GenericRepository<Actor> ActorRepository
        {
            get
            {
                if (this._ActorRepository == null)
                    _ActorRepository = new GenericRepository<Actor>(_context);
                return _ActorRepository;
            }
        }

        /// <summary>  
        /// Get/Set Property for Movie repository.  
        /// </summary>  
        public GenericRepository<Movie> MovieRepository
        {
            get
            {
                if (this._MovieRepository == null)
                    this._MovieRepository = new GenericRepository<Movie>(_context);
                return _MovieRepository;
            }
        }

        /// <summary>  
        /// Get/Set Property for MovieActor repository.  
        /// </summary>  
        public GenericRepository<MovieActor> MovieActorRepository
        {
            get
            {
                if (this._MovieActorRepository == null)
                    this._MovieActorRepository = new GenericRepository<MovieActor>(_context);
                return _MovieActorRepository;
            }
        }

        /// <summary>
        /// Get property for MovieProducer Repository
        /// </summary>
        public GenericRepository<MovieProducer> MovieProducerRepository
        {
            get
            {
                if (this._MovieProducerRepository == null)
                    this._MovieProducerRepository = new GenericRepository<MovieProducer>(_context);
                return _MovieProducerRepository;
            }
        }

        /// <summary>
        /// Get Property for Producer Location Respository
        /// </summary>
        public GenericRepository<Producer> ProducerRepository
        {
            get
            {
                if (_ProducerRepository == null)
                    this._ProducerRepository = new GenericRepository<Producer>(_context);
                return _ProducerRepository;
            }
        }

        #endregion

        #region Public member methods...
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var outputLines = new List<string>();
                foreach (var item in ex.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, item.Entry.Entity.GetType().Name, item.Entry.State));
                    foreach (var ve in item.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw ex;
            }
        }
        #endregion

        #region Implementing IDiosposable...

        #region private Dispose variable Declaration...
        private bool _disposed = false;
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
