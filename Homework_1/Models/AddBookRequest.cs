namespace Homework_1.Models
{
    public class AddBookRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int YearOfWriting {  get; set; }
    }
}
