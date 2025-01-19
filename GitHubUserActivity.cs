using GitHubUserActivity.Services;

namespace GitHubUserActivity
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("github-activity ");
            string username = Console.ReadLine();

            var gitHubService = new GitHubServices(new HttpClient());
            try
            {
                await gitHubService.ValidateUserNameAsync(username);
                var activities = await gitHubService.GetUserActivitiesAsync(username);

                Console.WriteLine("\nRecent Activities:");
                if (activities.Any())
                {
                    foreach (var activity in activities)
                    {
                        Console.WriteLine($"- {activity}");
                    }
                }
                else
                {
                    Console.WriteLine("No recent activity found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
