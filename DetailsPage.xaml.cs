using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Apibaza
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Movie movie)
        {
            InitializeComponent();
            BindingContext = movie;
        }
    }
}
