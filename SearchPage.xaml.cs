using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Apibaza
{
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {
            // Wykonaj zapytanie do API i prześlij wyniki do ResultsPage
            // Na przykład:
            // List<Movie> movies = await GetMovies(TitleEntry.Text, YearEntry.Text, ActorEntry.Text);
            // await Navigation.PushAsync(new ResultsPage(movies));
        }
    }
}