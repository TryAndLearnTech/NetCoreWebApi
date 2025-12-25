namespace NetCoreWebApi.Application.Dtos.Book
{
    public class CreateBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
    }
}
