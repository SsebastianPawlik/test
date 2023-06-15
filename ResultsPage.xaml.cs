using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Apibaza
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultsPage : ContentPage
    {
        public ResultsPage(List<Movie> movies)
        {
            InitializeComponent();
            MovieList.ItemsSource = movies;
            MovieList.ItemSelected += MovieList_ItemSelected;
        }

        private void MovieList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Movie selectedMovie = e.SelectedItem as Movie;
            Navigation.PushAsync(new DetailsPage(selectedMovie));
        }
    }
}
