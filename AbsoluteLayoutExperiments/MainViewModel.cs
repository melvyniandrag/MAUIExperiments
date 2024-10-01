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
        LugButton? redoLug = null;
        
        [ObservableProperty]
        LugButton? currentLug = null;

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
            // This will trigger a REDO

            // Case1, we are in redo already and you click a different lug. 
            // if that lug can be redone, set the current redo lug to skipped and go to the other lug.
            if ((RedoLug != null) && (button != RedoLug))
            {
                if (button == CurrentLug)
                {
                    if (RedoLug.Status == LugButton.LugStatus.CURRENT_DO_OVER)
                    {
                        RedoLug.Status = LugButton.LugStatus.FAILED;

                    }
                    else
                    {
                        RedoLug.Status = LugButton.LugStatus.SKIPPED;
                    }
                    RedoLug = null;
                    CurrentLug.Status = LugButton.LugStatus.CURRENT;
                    return;
                }
                else if (button.Status == LugButton.LugStatus.FAILED)
                {
                    if (RedoLug.Status == LugButton.LugStatus.CURRENT_DO_OVER)
                    {
                        RedoLug.Status = LugButton.LugStatus.FAILED;

                    }
                    else
                    {
                        RedoLug.Status = LugButton.LugStatus.SKIPPED;
                    }
                    RedoLug = button;
                    RedoLug.Status = LugButton.LugStatus.CURRENT_DO_OVER;
                }
                else if (button.Status == LugButton.LugStatus.SKIPPED)
                {
                    if (RedoLug.Status == LugButton.LugStatus.CURRENT_DO_OVER)
                    {
                        RedoLug.Status = LugButton.LugStatus.FAILED;

                    }
                    else
                    {
                        RedoLug.Status = LugButton.LugStatus.SKIPPED;
                    }
                    RedoLug = button;
                    RedoLug.Status = LugButton.LugStatus.CURRENT;
                }
            }
            else if (button.Status == LugButton.LugStatus.SKIPPED)
            {
                RedoLug = button;
                RedoLug.Status = LugButton.LugStatus.CURRENT;
                if (CurrentLug != null)
                {
                    CurrentLug.Status = LugButton.LugStatus.PAUSED;
                }
            }
            else if (button.Status == LugButton.LugStatus.FAILED)
            {
                RedoLug = button;
                RedoLug.Status = LugButton.LugStatus.CURRENT_DO_OVER;
                if (CurrentLug != null)
                {
                    CurrentLug.Status = LugButton.LugStatus.PAUSED;
                }
            }
            else if (button.Status == LugButton.LugStatus.SUCCESS)
            {
                RedoLug = button;
                RedoLug.Status = LugButton.LugStatus.CURRENT;
                if (CurrentLug != null)
                {
                    CurrentLug.Status = LugButton.LugStatus.PAUSED;
                }
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

        [RelayCommand]
        void OnSkipClicked()
        {
            if(RedoLug != null)
            {
                if(RedoLug.Status == LugButton.LugStatus.CURRENT_DO_OVER)
                {
                    RedoLug.Status = LugButton.LugStatus.FAILED;

                }
                else
                {
                    RedoLug.Status = LugButton.LugStatus.SKIPPED;
                }
                RedoLug = null;
                if(CurrentLug != null)
                {
                    CurrentLug.Status = LugButton.LugStatus.CURRENT;
                }
            }
            else if (CurrentLug == null)
            {
                ResetButtons();
            }
            else
            {
                if (CurrentLug.Status == LugButton.LugStatus.CURRENT_DO_OVER)
                {
                    CurrentLug.Status = LugButton.LugStatus.FAILED;

                }
                else
                {
                    CurrentLug.Status = LugButton.LugStatus.SKIPPED;
                }
                CurrentLug = CurrentLug?.NextLug ?? null;
                if (CurrentLug != null)
                {
                    CurrentLug.Status = LugButton.LugStatus.CURRENT;
                }
            }
        }

        [RelayCommand]
        void OnSuccessClicked()
        {
            if (RedoLug != null)
            {
                RedoLug.Status = LugButton.LugStatus.SUCCESS;
                RedoLug = null;
                if (CurrentLug != null)
                {
                    CurrentLug.Status = LugButton.LugStatus.CURRENT;
                }
            }
            else
            {
                if (CurrentLug == null)
                {
                    ResetButtons();
                }
                else
                {
                    CurrentLug.Status = LugButton.LugStatus.SUCCESS;
                    CurrentLug = CurrentLug?.NextLug ?? null;
                    if (CurrentLug != null)
                    {
                        CurrentLug.Status = LugButton.LugStatus.CURRENT;
                    }
                }
            }
        }

        [RelayCommand]
        void OnFailClicked()
        {
            if (RedoLug != null)
            {
                RedoLug.Status = LugButton.LugStatus.CURRENT_DO_OVER;
            }
            else
            {
                if (CurrentLug == null)
                {
                    ResetButtons();
                }
                else
                {
                    CurrentLug.Status = LugButton.LugStatus.CURRENT_DO_OVER;
                }
            }
        }

        void ResetButtons()
        {
            LugButton seven = new LugButton() { Text = "seven" };
            LugButton six = new LugButton() { Text = "six", NextLug = seven };
            LugButton five = new LugButton() { Text = "five", NextLug = six };
            LugButton four = new LugButton() { Text = "four", NextLug = five };
            LugButton three = new LugButton() { Text = "three", NextLug = four };
            LugButton two = new LugButton() { Text = "two", NextLug = three, };
            LugButton one = new LugButton() { Text = "one", NextLug = two, Status = LugButton.LugStatus.CURRENT };
            Buttons = new ObservableCollection<LugButton>()
            {
                one, four, two, six,three,five, seven
            };
            CurrentLug = Buttons[0];
        }

        public MainViewModel()
        {
            ResetButtons();
        }
    }
}
