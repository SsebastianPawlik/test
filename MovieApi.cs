using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Apibaza
{
    public class MovieApi
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<Movie>> GetMoviesByTitle(string title)
        {
            List<Movie> movies = new List<Movie>();
            string url = $"https://api.themoviedb.org/3/search/movie?api_key=f4aa9b2018e5a31b646962d9ae7df1d5&query={Uri.EscapeDataString(title)}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                JArray results = (JArray)json["results"];

                foreach (JObject result in results)
                {
                    Movie movie = new Movie
                    {
                        Title = (string)result["title"],
                        ReleaseDate = (string)result["release_date"],
                        Overview = (string)result["overview"],
                        PosterPath = (string)result["poster_path"]
                    };
                    movies.Add(movie);
                }
            }

            return movies;
        }

        public static async Task<List<Movie>> GetMoviesByYear(string year)
        {
            List<Movie> movies = new List<Movie>();
            string url = $"https://api.themoviedb.org/3/discover/movie?api_key=f4aa9b2018e5a31b646962d9ae7df1d5&primary_release_year={Uri.EscapeDataString(year)}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(content);
                JArray results = (JArray)json["results"];

                foreach (JObject result in results)
                {
                    Movie movie = new Movie
                    {
                        Title = (string)result["title"],
                        ReleaseDate = (string)result["release_date"],
                        Overview = (string)result["overview"],
                        PosterPath = (string)result["poster_path"]
                    };
                    movies.Add(movie);
                }
            }

            return movies;
        }
        public static async Task<List<Movie>> GetMoviesByMultipleActors(string[] actorNames)
        {
            List<Movie> allMovies = new List<Movie>();

            // Dla każdego aktora pobierz jego filmy
            foreach (string actorName in actorNames)
            {
                List<Movie> actorMovies = await GetMoviesByActor(actorName.Trim());
                allMovies.AddRange(actorMovies);
            }

            // Teraz znajdź filmy, które są wspólne dla wszystkich aktorów
            List<Movie> commonMovies = allMovies.GroupBy(m => m.Title)
                                                 .Where(g => g.Count() == actorNames.Length)
                                                 .Select(g => g.First())
                                                 .ToList();

            return commonMovies;
        }


        public static async Task<List<Movie>> GetMoviesByActor(string actorName)
        {
            List<Movie> movies = new List<Movie>();
            string actorSearchUrl = $"https://api.themoviedb.org/3/search/person?api_key=f4aa9b2018e5a31b646962d9ae7df1d5&query={Uri.EscapeDataString(actorName)}";

            HttpResponseMessage actorResponse = await client.GetAsync(actorSearchUrl);
            if (actorResponse.IsSuccessStatusCode)
            {
                string actorContent = await actorResponse.Content.ReadAsStringAsync();
                JObject actorJson = JObject.Parse(actorContent);
                JArray actorResults = (JArray)actorJson["results"];

                if (actorResults.Count > 0)
                {
                    string actorId = (string)actorResults[0]["id"];
                    string movieSearchUrl = $"https://api.themoviedb.org/3/person/{actorId}/movie_credits?api_key=f4aa9b2018e5a31b646962d9ae7df1d5";

                    HttpResponseMessage movieResponse = await client.GetAsync(movieSearchUrl);
                    if (movieResponse.IsSuccessStatusCode)
                    {
                        string movieContent = await movieResponse.Content.ReadAsStringAsync();
                        JObject movieJson = JObject.Parse(movieContent);
                        JArray movieResults = (JArray)movieJson["cast"];

                        foreach (JObject result in movieResults)
                        {
                            Movie movie = new Movie
                            {
                                Title = (string)result["title"],
                                ReleaseDate = (string)result["release_date"],
                                Overview = (string)result["overview"],
                                PosterPath = $"https://image.tmdb.org/t/p/w500{(string)result["poster_path"]}"
                            };
                            movies.Add(movie);
                        }
                    }
                }
            }

            return movies;
        }
    }
}
