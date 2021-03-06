namespace TheNewPanelists.MotoMoto.Models
{
    public class CommentPostModel : IPostModel
    {
        public string type { get; } = "comment";
        public int postID { get; set; }
        public string? postTitle { get; set; }
        public string? contentType { get; set; } // Not necessary
        public string? postUser { get; set; } // Username of user posting the comment
        public string? postDescription { get; set; }
        public IEnumerable<byte[]>? imageList { get; set; } // Do not use

        // Need default constructor for model binding
        public CommentPostModel() { }

        // The required data store attributes are here
        public CommentPostModel(int postid, string username, string description)
        {
            postID = postid;
            postUser = username;
            postDescription = description;
        }

        public CommentPostModel(int postid, string? posttitle, string username, string description)
        {
            postID = postid;
            postTitle = posttitle;
            postUser = username;
            postDescription = description;
        }
    }
}