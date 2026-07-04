using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnki.Core.API
{
    public static class GithubAPI
    {
        public static GitHubClient Client = new GitHubClient(new ProductHeaderValue("AnkiUpdater"));

        public static Release GetLatestRelease()
        {
            var latest = Client.Repository.Release.GetLatest("MatarraNA", "AutoAnki").GetAwaiter().GetResult();
            return latest;
        }
    }
}
