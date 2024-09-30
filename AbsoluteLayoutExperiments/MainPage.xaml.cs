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
            //MyCircle.BindingContext = vm;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
