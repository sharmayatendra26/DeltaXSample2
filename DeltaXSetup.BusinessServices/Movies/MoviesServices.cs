using AutoMapper;
using BusinessEntities;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using DM = DataModel;

namespace BusinessServices.Movies
{
    public class MoviesServices: IMoviesServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public decimal AddMovie(MovieEntity Movie)
        {
            decimal result = 0;
            bool isExist = _unitOfWork.MovieRepository.IsExist(Movie.MovieId);
            if (!isExist)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DM.Movie MoviesDetail = new DM.Movie
                    {
                        MovieId = Movie.MovieId,                        
                        CreatedOn = DateTime.Now,
                        ModifiedOn = DateTime.Now,
                        ReleaseYear = Movie.ReleaseYear,
                        IsDeleted = Movie.IsDeleted,
                        Name = Movie.Name,
                        Poster = SaveMoviePosterInFolder(Movie.MovieId, Movie.File),
                        Plot = Movie.Plot,
                        ModifiedBy = string.Empty,
                        CreatedBy=string.Empty
                    };
                    foreach (var item in Movie.MovieActors)
                    {
                        DM.MovieActor movieActor = new DM.MovieActor
                        {
                            ActorId = item.ActorId,
                            MovieId = item.MovieId,
                            CreatedBy = string.Empty,
                            ModifiedBy = string.Empty,
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now
                        };
                        MoviesDetail.MovieActors.Add(movieActor);
                    }
                    foreach (var item in Movie.MovieProducers)
                    {
                        DM.MovieProducer movieProducer = new DM.MovieProducer
                        {
                            ProducerId = item.ProducerId,
                            MovieId = item.MovieId,
                            CreatedBy = string.Empty,
                            ModifiedBy = string.Empty,
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now
                        };
                        MoviesDetail.MovieProducers.Add(movieProducer);
                    }
                    _unitOfWork.MovieRepository.Insert(MoviesDetail);
                    _unitOfWork.Save();
                    scope.Complete();
                    result = MoviesDetail.MovieId;
                }
            }
            return result;
        }

        public bool UpdateMovie(int MovieId, MovieEntity MovieEntity)
        {
            bool success = false;
            if (MovieEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var Movies = _unitOfWork.MovieRepository.GetByID(MovieEntity.MovieId);
                    if (Movies != null)
                    {
                        Movies.Name = MovieEntity.Name;
                        Movies.MovieActors = MovieEntity.MovieActors as List<DM.MovieActor>;
                        Movies.MovieProducers = MovieEntity.MovieProducers as List<DM.MovieProducer>;
                        Movies.ReleaseYear = MovieEntity.ReleaseYear;
                        Movies.Poster = SaveMoviePosterInFolder(MovieEntity.MovieId, MovieEntity.File);
                        Movies.Plot = MovieEntity.Plot;
                        Movies.ModifiedOn = DateTime.Now;
                        _unitOfWork.MovieRepository.Update(Movies);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool DisContinueMovie(int MovieId)
        {
            bool success = false;
            if (MovieId > 0)
            {
                using (TransactionScope scope=new TransactionScope())
                {
                    DM.Movie Movies = _unitOfWork.MovieRepository.GetByID(MovieId);
                    if (MovieId > 0)
                    {
                        Movies.IsDeleted = true;
                        _unitOfWork.MovieRepository.Update(Movies);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }                
            }
            return success;
        }

        public MovieEntity GetMovieById(int Id)
        {
            DM.Movie Movies = _unitOfWork.MovieRepository.GetByID(Id);
            if (Movies != null)
            {
                Mapper.CreateMap<DM.Movie, MovieEntity>();
                var MoviesModel = Mapper.Map<DM.Movie, MovieEntity>(Movies);
                return MoviesModel;
            }
            return null;
        }

        public List<MovieEntity> GetMovieList()
        {
            try
            {
                var Movies = _unitOfWork.MovieRepository.GetWithInclude("MovieActors", "MovieProducers").ToList();
                if (Movies.Any())
                {
                    Mapper.CreateMap<DM.Movie, MovieEntity>();
                    var MoviesModel = Mapper.Map<List<DM.Movie>, List<MovieEntity>>(Movies);
                    return MoviesModel;
                }
            }
            catch (Exception ex)
            {                
                throw;
            }
            
            return null;
        }

        private string SaveMoviePosterInFolder(decimal movieId, HttpPostedFileBase file)
        {
            string fileName = movieId + "_" + file.FileName;
            string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/MovieImages"), Path.GetFileName(fileName));
            file.SaveAs(path);
            return fileName;
        }

    }
}
