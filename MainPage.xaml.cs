using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Apibaza
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            if (TitleRadioButton.IsChecked)
            {
                List<Movie> movies = await MovieApi.GetMoviesByTitle(SearchEntry.Text);
                if (movies.Count == 0)
                {
                    await DisplayAlert("Brak wyników", "Nie znaleziono filmów o podanym tytule.", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new ResultsPage(movies));
                }
            }
            else if (YearRadioButton.IsChecked)
            {
                List<Movie> movies = await MovieApi.GetMoviesByYear(SearchEntry.Text);
                if (movies.Count == 0)
                {
                    await DisplayAlert("Brak wyników", "Nie znaleziono filmów z podanego roku.", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new ResultsPage(movies));
                }
            }
            else if (ActorRadioButton.IsChecked)
            {
                string[] actorNames = SearchEntry.Text.Split(',');
                List<Movie> movies = await MovieApi.GetMoviesByActor(actorNames[0].Trim());

                if (movies.Count == 0)
                {
                    await DisplayAlert("Brak wyników", "Nie znaleziono filmów z podanym aktorem.", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new ResultsPage(movies));
                }
            }
            else if (MultipleActorsRadioButton.IsChecked)
            {
                string[] actorNames = SearchEntry.Text.Split(',');
                List<Movie> movies = await MovieApi.GetMoviesByMultipleActors(actorNames);

                if (movies.Count == 0)
                {
                    await DisplayAlert("Brak wyników", "Nie znaleziono filmów, w których grają wszyscy podani aktorzy.", "OK");
                }
                else
                {
                    await Navigation.PushAsync(new ResultsPage(movies));
                }
            }
        }



    }



}
