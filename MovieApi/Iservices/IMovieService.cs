namespace MovieApi.Iservices
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAsync(int genreId = 0);

        Task<Movie> GetByIdAsync(int id);

        Task<Movie> CreateAsync(Movie movie);
        
        Movie UpdateAsync(Movie movie);

        Movie DeleteAsync(Movie movie);

    }
}
