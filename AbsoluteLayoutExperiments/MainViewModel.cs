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
        LugButton? nextButton = null;

        partial void OnNextButtonChanged(LugButton? oldValue, LugButton? newValue)
        {
            //oldValue?.StopRotationAnimation();
            //newValue?.StartRotationAnimation();
        }

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
                NextButton = button.NextLug;
                if (NextButton == null)
                {
                    NextButton = Buttons[0];
                }
                NextButton.IsSelected = true;

                foreach (var btn in Buttons)
                {
                    if (btn != NextButton)
                    {
                        btn.IsSelected = false;
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

            LugButton four = new LugButton() { Text = "four" };
            LugButton three = new LugButton() { Text = "three", NextLug = four };
            LugButton two = new LugButton() { Text = "two", NextLug = three, };
            LugButton one = new LugButton() { Text = "one", NextLug = two, IsSelected = true };
            Buttons = new ObservableCollection<LugButton>()
            {
                one, two, three, four
            };
        }
    }
}
