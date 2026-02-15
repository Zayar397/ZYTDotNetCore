namespace ZYTDotNetCore.RestApi.ViewModel
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public bool DeleteFlage { get; set; }
    }
}
