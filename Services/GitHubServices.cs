using GitHubUserActivity.Models;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GitHubUserActivity.Services
{
    public class GitHubServices
    {
        private readonly HttpClient _httpClient;

        public GitHubServices(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# Console App");
        }

        public async Task ValidateUserNameAsync(string username)
        {
            var findUserApi = $"https://api.github.com/users/{username}";
            var response = await _httpClient.GetAsync(findUserApi);

            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException("Username is incorrect");
            }
        }

        public async Task<List<string>> GetUserActivitiesAsync(string username)
        {
            var apiUrl = $"https://api.github.com/users/{username}/events";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var events = JsonSerializer.Deserialize<List<GitHubEvent>>(responseData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var activities = new List<string>();

            foreach (var githubEvent in events)
            {
                GitHubEventType eventType;
                if (Enum.TryParse(githubEvent.Type, out eventType))
                {
                    switch (eventType)
                    {
                        case GitHubEventType.PushEvent:
                            activities.Add($"Pushed {githubEvent.Payload.Commits.Count} commits to {githubEvent.Repo.Name}");
                            break;

                        case GitHubEventType.ForkEvent:
                            activities.Add($"Forked {githubEvent.Repo.Name}");
                            break;

                        case GitHubEventType.IssuesEvent:
                            if (githubEvent.Payload.Action == "opened")
                            {
                                activities.Add($"Opened a new issue in {githubEvent.Repo.Name}");
                            }
                            else if (githubEvent.Payload.Action == "closed")
                            {
                                activities.Add($"Closed an issue in {githubEvent.Repo.Name}");
                            }
                            break;

                        case GitHubEventType.PullRequestEvent:
                            if (githubEvent.Payload.Action == "opened")
                            {
                                activities.Add($"Opened a pull request in {githubEvent.Repo.Name}");
                            }
                            else if (githubEvent.Payload.Action == "closed")
                            {
                                activities.Add($"Closed a pull request in {githubEvent.Repo.Name}");
                            }
                            else if (githubEvent.Payload.Action == "merged")
                            {
                                activities.Add($"Merged a pull request in {githubEvent.Repo.Name}");
                            }
                            break;

                        case GitHubEventType.CreateEvent:
                            if (githubEvent.Payload.RefType == "branch")
                            {
                                activities.Add($"Created a new branch in {githubEvent.Repo.Name}");
                            }
                            else if (githubEvent.Payload.RefType == "tag")
                            {
                                activities.Add($"Created a new tag in {githubEvent.Repo.Name}");
                            }
                            break;

                        case GitHubEventType.DeleteEvent:
                            if (githubEvent.Payload.RefType == "branch")
                            {
                                activities.Add($"Deleted a branch in {githubEvent.Repo.Name}");
                            }
                            else if (githubEvent.Payload.RefType == "tag")
                            {
                                activities.Add($"Deleted a tag in {githubEvent.Repo.Name}");
                            }
                            break;

                        case GitHubEventType.WatchEvent:
                            activities.Add($"Starred {githubEvent.Repo.Name}");
                            break;

                        case GitHubEventType.MemberEvent:
                            if (githubEvent.Payload.Action == "added")
                            {
                                activities.Add($"Added a collaborator to {githubEvent.Repo.Name}");
                            }
                            else if (githubEvent.Payload.Action == "removed")
                            {
                                activities.Add($"Removed a collaborator from {githubEvent.Repo.Name}");
                            }
                            break;

                        case GitHubEventType.ReleaseEvent:
                            if (githubEvent.Payload.Action == "published")
                            {
                                activities.Add($"Released a new version of {githubEvent.Repo.Name}");
                            }
                            break;

                        case GitHubEventType.GollumEvent:
                            activities.Add($"Updated wiki page in {githubEvent.Repo.Name}");
                            break;

                        case GitHubEventType.PublicEvent:
                            activities.Add($"Made {githubEvent.Repo.Name} public");
                            break;

                        case GitHubEventType.CommitCommentEvent:
                            activities.Add($"Commented on a commit in {githubEvent.Repo.Name}");
                            break;

                        case GitHubEventType.IssueCommentEvent:
                            activities.Add($"Commented on an issue in {githubEvent.Repo.Name}");
                            break;

                        default:
                            break;
                    }
                }
            }
            return activities;
        }
    }
}
