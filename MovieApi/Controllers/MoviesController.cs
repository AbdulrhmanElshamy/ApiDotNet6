using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MovieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;
        private readonly List<string> _allowedExtentions = new List<string> { ".jpg", ".png", ".jepg" };
        private readonly int _maxAllowedPosterSize = 1048576;

        public MoviesController(IMovieService movieService,
            IGenreService genreService, IMapper mapper)
        {
            _movieService = movieService;
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var Movies = await _movieService.GetAsync();

            var dto = _mapper.Map<IEnumerable<MoviesDetailsDto>>(Movies);

            return Ok(dto);
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var movie = await _movieService.GetByIdAsync(Id);

            if(movie is null)
                return NotFound($"No movie was found with id : {Id}");

            var dto = _mapper.Map<MoviesDetailsDto>(movie);

            //var dto = new MoviesDetailsDto
            //{
            //    Id = movie.Id,
            //    Title = movie.Title,
            //    GenreName = movie.Genre.Name,
            //    GenreId = movie.Genre.Id,
            //    Poster = movie.Poster,
            //    Year = movie.Year,
            //    Rate = movie.Rate,
            //    StoryLine = movie.StoryLine,
            //};

            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreId(int GenreId)
        {
            var Movies = await _movieService.GetAsync(GenreId);

            var dto = _mapper.Map<IEnumerable<MoviesDetailsDto>>(Movies);

            return Ok(dto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            if (dto.Poster is null)
                return BadRequest("Poster is requierd");

            if (!_allowedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only png, jpg and jpeg extention allwoed!");

            if (dto.Poster.Length > _maxAllowedPosterSize)
                return BadRequest("Max size for poster allwoed is 1M!");

            var IsvaildGenre = await _genreService.IsAvaIsvaildGenre(dto.GenreId);

            if (!IsvaildGenre)
                return BadRequest("Invaild genre id");

            using var datastreem = new MemoryStream();
            await dto.Poster.CopyToAsync(datastreem);

            var movie = _mapper.Map<Movie>(dto);
            movie.Poster = datastreem.ToArray();

            movie = await _movieService.CreateAsync(movie);

            return Ok(movie);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Edit(int Id,[FromForm] MovieDto dto)
        {
            var movie = await _movieService.GetByIdAsync(Id);

            if (movie is null)
                return NotFound($"No movie was found with is {Id}");

            movie = _mapper.Map<Movie>(dto);

            if(dto.Poster is not null)
            {
                if (!_allowedExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only png, jpg and jpeg extention allwoed!");

                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max size for poster allwoed is 1M!");

                var IsvaildGenre = await _genreService.IsAvaIsvaildGenre(dto.GenreId);
                if (!IsvaildGenre)
                    return BadRequest("Invaild genre id");

                using var datastreem = new MemoryStream();
                await dto.Poster.CopyToAsync(datastreem);

                movie.Poster = datastreem.ToArray();
            }
            
            _movieService.UpdateAsync(movie);

            return Ok(movie);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            var movie = await _movieService.GetByIdAsync(Id);

            if (movie is null)
                return NotFound($"No movie was found with id: {Id}");

            movie = _movieService.DeleteAsync(movie);

            return Ok(movie);
        }
    }
}
