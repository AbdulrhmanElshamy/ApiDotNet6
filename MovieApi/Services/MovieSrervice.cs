using Microsoft.EntityFrameworkCore;

namespace MovieApi.Services
{
    public class MovieSrervice : IMovieService
    {
        private readonly ApplicationDbContext _dbContext;

        public MovieSrervice(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Movie> CreateAsync(Movie movie)
        {
            await _dbContext.AddAsync(movie);  
            _dbContext.SaveChanges();

            return movie;
        }

        public Movie DeleteAsync(Movie movie)
        {

            _dbContext.Remove(movie);
            _dbContext.SaveChanges();

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAsync(int genreId = 0)
        {
            var movies = await _dbContext.Movies
                .Where(g => g.GenreId == genreId || genreId == 0)
                .Include(g => g.Genre)
                .OrderByDescending(m => m.Rate)
                .ToListAsync();

            return movies;
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _dbContext.Movies.Include(g => g.Genre).SingleOrDefaultAsync(m => m.Id == id);
        }


        public Movie UpdateAsync(Movie movie)
        {
            _dbContext.Update(movie);
            _dbContext.SaveChanges();

            return movie;
        }
    }
}
