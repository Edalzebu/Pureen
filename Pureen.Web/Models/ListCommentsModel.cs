namespace Pureen.Web.Models
{
    public class ListCommentsModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Information { get; set; }
        public string CommentedDate { get; set; }
        public long NewId { get; set; }
    }
}