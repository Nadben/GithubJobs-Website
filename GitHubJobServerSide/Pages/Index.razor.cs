using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace GitHubJobServerSide.Pages
{
    public class IndexComponentBase : ComponentBase
    {
        [Inject]
        public IHttpClientFactory Http { get; set; }

        [Parameter]
        public string Description { get; set; }

        [Parameter]
        public string Location { get; set; }

        [Parameter]
        public string InvalidMessage { get; set; } = "";

        [Parameter]
        public List<GithubJobsModel> JobsModels { get; set; }

        public string Visible { get; set; }

        private const string BaseUrl = @"https://jobs.github.com/positions.json?description=";
        private const string LocationExtensionBaseUrl = @"&location=";
        private const string IsFullTimeBaseUrl = @"&full_time=";

        protected override void OnInitialized()
        {
            JobsModels = new List<GithubJobsModel>();
            Visible = "is-hidden";
        }

        public async Task KeyPressAsync(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                await OnClick_SearchButton();
            }
        }

        public async Task OnClick_SearchButton()
        {
            Visible = "";
            JobsModels.Clear();
            StateHasChanged();
            try
            {
                var client = Http.CreateClient();

                HttpResponseMessage response = await client.GetAsync(BaseUrl +
                    Description +
                    LocationExtensionBaseUrl +
                    Location);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                JobsModels = JsonConvert.DeserializeObject<IEnumerable<GithubJobsModel>>(responseBody).ToList();
                StateHasChanged();

                Visible = "is-hidden";
            }
            catch (HttpRequestException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}