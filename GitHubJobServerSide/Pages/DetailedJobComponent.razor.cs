using Microsoft.AspNetCore.Components;

namespace GitHubJobServerSide.Pages
{
    public class DetailedJobComponentBase : ComponentBase
    {
        [Parameter]
        public GithubJobsModel Job { get; set; }

        [Inject]
        public AppState JobStateManager { get; set; }

        protected override void OnInitialized()
        {
            Job = JobStateManager.Job;
        }
    }
}