using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AbsoluteLayoutExperiments
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Button> buttons =
        [
            new Button(){ Text="congrats"},
            new Button(){ Text="you did it"},
            new Button()
        ];

        [ObservableProperty]
        string myText = "hello";


        private void ButtonClickedHandler(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("you clicked " + ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        void OnButtonClicked(Button button)
        {
            try
            {
                Debug.WriteLine("you clicked " + button.Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
