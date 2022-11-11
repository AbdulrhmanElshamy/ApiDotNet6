namespace MovieApi.Dto
{
    public class GenreDto
    {
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
