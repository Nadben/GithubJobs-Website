using Microsoft.AspNetCore.Components;

namespace GitHubJobServerSide.Pages
{
    public class CardJobPreviewComponentBase : ComponentBase
    {
        [Inject]
        public NavigationManager navigationManager { get; set; }

        [Inject]
        public AppState JobStateManager { get; set; }

        [Parameter]
        public GithubJobsModel Job { get; set; }

        public void OnClick_NavigateToDetailedPage()
        {
            //Notify state has changed and set the job
            JobStateManager.SetJob(Job);
            navigationManager.NavigateTo($"/detailedJobPage");
        }
    }
}