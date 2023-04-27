namespace PBugTracker.Models
{
    public class Comment : ModelBase
    {
        public int BugID { get; set; }
        public string Commentor { get; set; }
        public string CommentDescription { get; set; }

    }
}
