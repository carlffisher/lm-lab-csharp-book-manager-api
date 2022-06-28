namespace BookManagerApi.Models
{
	public class Book
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Author { get; set; }
		public Genre Genre { get; set; }
	}
}
