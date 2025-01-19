using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserActivity.Models
{
    public class GitHubEvent
    {
        public string Type { get; set; } // Type of the event (e.g., PushEvent, ForkEvent)
        public GitHubRepo Repo { get; set; } // Repository info
        public DateTime CreatedAt { get; set; }
        public GitHubPayload Payload { get; set; } // Event payload data
    }
}
