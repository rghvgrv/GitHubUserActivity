using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubUserActivity.Models
{
    public enum GitHubEventType
    {
        PushEvent,
        ForkEvent,
        IssuesEvent,
        PullRequestEvent,
        CreateEvent,
        DeleteEvent,
        WatchEvent,
        MemberEvent,
        ReleaseEvent,
        GollumEvent,
        PublicEvent,
        CommitCommentEvent,
        IssueCommentEvent
    }
}
