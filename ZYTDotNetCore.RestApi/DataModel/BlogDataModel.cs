namespace ZYTDotNetCore.RestApi.DataModel
{
    public class BlogDataModel
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
        public bool DeleteFlage { get; set; }
    }
}
