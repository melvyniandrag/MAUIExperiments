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
        LugButton? selectedButton = null;

        [ObservableProperty]
        ObservableCollection<LugButton> buttons;

        [ObservableProperty]
        string myText = "hello";


        private void ButtonClickedHandler(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("you clicked " + ((LugButton)sender).Text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        void OnButtonClicked(LugButton button)
        {
            try
            {
                Debug.WriteLine("you clicked " + button.Text);
                SelectedButton = button.NextLug;
                if (SelectedButton != null)
                {
                    SelectedButton.BackgroundColor = Colors.Red;
                }
                foreach (var btn in Buttons)
                {
                    if(btn != SelectedButton)
                    {
                        btn.BackgroundColor = Colors.Green;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [RelayCommand]
        void OnChangeButtonColorsClicked()
        {
            foreach (var button in Buttons)
            {
                button.BackgroundColor = Colors.Orange;
            }
        }

        public MainViewModel()
        {

            LugButton four = new LugButton() { Text = "congrats", BackgroundColor = Colors.Green };
            LugButton three = new LugButton() { Text = "congrats", NextLug = four, BackgroundColor = Colors.Green };
            LugButton two = new LugButton() { Text = "congrats", NextLug = three, BackgroundColor = Colors.Green };
            LugButton one = new LugButton() { Text = "congrats", NextLug = two, BackgroundColor = Colors.Green };
            Buttons = new ObservableCollection<LugButton>()
            {
                one, two, three, four
            };
            SelectedButton = one;
            SelectedButton.BackgroundColor = Colors.Red;
        }
    }
}
