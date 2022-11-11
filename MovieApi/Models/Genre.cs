namespace MovieApi.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Name { get; set; }
    }
}
