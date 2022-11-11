namespace MovieApi.Dto
{
    public class MovieDto
    {
        [StringLength(255)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }

        [StringLength(2500)]
        public string StoryLine { get; set; }

        public IFormFile? Poster { get; set; }

        public int GenreId { get; set; }

    }
}
