namespace PBugTracker.Models
{
    public class Bug : ModelBase
    {
        public string Title { get; set; }
        public string Reporter { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Assignee { get; set; }
        public string Status { get; set; }
    }
}
