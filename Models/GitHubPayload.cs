namespace GitHubUserActivity.Models
{
    public class GitHubPayload
    {
        public List<GitHubCommit> Commits { get; set; } // List of commits for PushEvent
        public string Action { get; set; }
        public string RefType {  get; set; }

    }
}