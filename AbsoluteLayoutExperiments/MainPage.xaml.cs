using System.Collections.ObjectModel;

namespace AbsoluteLayoutExperiments
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        MainViewModel vm;
        public MainPage()
        {
            InitializeComponent();
            vm = new MainViewModel();
            BindingContext = vm;
        }

    }

}
