using System;

namespace GitHubJobServerSide
{
    public class AppState
    {
        public GithubJobsModel Job { get; set; }

        public event Action OnChange;

        public void SetJob(GithubJobsModel jobToSet)
        {
            Job = jobToSet;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}