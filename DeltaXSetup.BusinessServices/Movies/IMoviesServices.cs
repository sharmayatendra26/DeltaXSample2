using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Movies
{
    public interface IMoviesServices
    {
        decimal AddMovie(MovieEntity Movie);
        MovieEntity GetMovieById(int Id);
        List<MovieEntity> GetMovieList();
    }
}
