using Microsoft.EntityFrameworkCore;

namespace MovieApi.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _dbContext;

        public GenreService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Genre> CreateAsync(GenreDto dto)
        {
            var genre = new Genre() { Name = dto.Name };

            await _dbContext.AddAsync(genre);

            _dbContext.SaveChanges();

            return genre;
        }

        public Genre DeleteAsync(Genre genre)
        {
            _dbContext.Remove(genre);
            _dbContext.SaveChanges();

            return genre;
        }


        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _dbContext.Genres.FindAsync(id);
        }


        public Genre UpdateAsync(Genre genre)
        {
            _dbContext.Update(genre);
            _dbContext.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _dbContext.Genres.OrderBy(g => g.Name).ToListAsync();
        }

        public async Task<bool> IsAvaIsvaildGenre(int id)
        {
            return await _dbContext.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
