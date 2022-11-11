namespace MovieApi.Iservices
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllAsync();

        Task<Genre> GetGenreByIdAsync(int id);

        Task<Genre> CreateAsync(GenreDto dto);

        Genre UpdateAsync(Genre genre);

        Genre DeleteAsync(Genre genre);

        Task<bool> IsAvaIsvaildGenre(int id);

    }
}
