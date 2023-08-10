using ClassLibrary.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace ClassLibrary.Repo
{
    public class RestApiRepo : IRepo
    {
        private readonly RestClient restClient = new(
            baseUrl: "https://worldcup-vua.nullbit.hr/",
            configureSerialization: s => s.UseNewtonsoftJson(Converter.Settings)
        );

        public Task<List<Team>> GetTeams()
        {
            return Request<List<Team>>("teams");
        }

        public Task<List<Match>> GetMatches(string countryCode)
        {
            return Request<List<Match>>($"matches/country?fifa_code={countryCode}");
        }

        public Task<List<Result>> GetResults()
        {
            return Request<List<Result>>("teams/results");
        }

        private Task<T> Request<T>(string path)
        {
            var request = new RestRequest($"{UserSettings.ChampionshipPath}/{path}");
            return restClient.GetAsync<T>(request)!;
        }
    }
}
