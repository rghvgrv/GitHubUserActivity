namespace GitHubUserActivity.Models
{
    public class GitHubCommit
    {
        public string Sha { get; set; } // Commit SHA (identifier)
        public string Message { get; set; } // Commit message
    }
}