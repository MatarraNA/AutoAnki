using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAnki.Core.API
{
    public static class GithubAPI
    {
        public static string UPDATE_ZIP_PATH => Path.Combine(AppContext.BaseDirectory, "update-zip.zip");
        private static readonly GitHubClient _client = new GitHubClient(new ProductHeaderValue("AnkiUpdater"));

        public static async Task<Release> GetLatestReleaseAsync()
        {
            return await _client.Repository.Release.GetLatest("MatarraNA", "AnkiInsert");
        }

        public static async Task<bool> IsUpdateAvailableByTimestampAsync()
        {
            var latest = await GetLatestReleaseAsync();

            DateTimeOffset releaseTime = latest.CreatedAt;
            DateTimeOffset buildTime = File.GetLastWriteTime(Process.GetCurrentProcess().MainModule!.FileName);

            return releaseTime > buildTime;
        }

        public static async Task<string> DownloadLatestAssetAsync()
        {
            var latest = await GetLatestReleaseAsync();

            // pick the ZIP asset
            var asset = latest.Assets
            .First(a => a.Name.EndsWith(".zip") &&
                !a.Name.Contains("source", StringComparison.OrdinalIgnoreCase));

            using var http = new HttpClient();
            var bytes = await http.GetByteArrayAsync(asset.BrowserDownloadUrl);

            if (File.Exists(UPDATE_ZIP_PATH)) File.Delete(UPDATE_ZIP_PATH);
            await File.WriteAllBytesAsync(UPDATE_ZIP_PATH, bytes);

            return UPDATE_ZIP_PATH;
        }

    }
}
