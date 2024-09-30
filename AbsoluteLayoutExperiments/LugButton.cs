using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsoluteLayoutExperiments
{
    public class LugButton : Button
    {
        public static readonly BindableProperty NextLugProperty =
            BindableProperty.Create(nameof(NextLug), typeof(LugButton), typeof(LugButton), null,
             propertyChanged: (bindable, oldVal, newVal) =>
             {

             });

        public LugButton? NextLug
        {
            get
            {
                return (LugButton)GetValue(NextLugProperty);
            }
            set
            {
                SetValue(NextLugProperty, value);
            }
        }

        public static readonly BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(LugButton), false,
             propertyChanged: (bindable, oldVal, newVal) =>
             {
                 if((bool)newVal)
                 {
                     ((LugButton)bindable).StartRotationAnimation();
                     if (App.Current?.RequestedTheme == AppTheme.Dark)
                     {
                         ((LugButton)bindable).ImageSource = "lug_selected.png";
                     }
                     else
                     {
                         ((LugButton)bindable).ImageSource = "lug_selected.png";
                     }
                 }
                 else 
                 {
                     ((LugButton)bindable).StopRotationAnimation();

                     if (App.Current?.RequestedTheme == AppTheme.Dark)
                     {
                         ((LugButton)bindable).ImageSource = "lug_deselected.png";
                     }
                     else
                     {
                         ((LugButton)bindable).ImageSource = "lug_deselected.png";
                     }
                 }
             });

        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }
            set
            {
                SetValue(IsSelectedProperty, value);
            }
        }

        CancellationTokenSource? _animationCancellationTokenSource;
        double currentRotation = 0;


        public async void StartRotationAnimation()
        {
            // Cancel any existing animation before starting a new one
            _animationCancellationTokenSource?.Cancel();

            // Create a new cancellation token source for the new animation
            _animationCancellationTokenSource = new CancellationTokenSource();


            // Example animation: button translation in X-axis
            while (!_animationCancellationTokenSource.Token.IsCancellationRequested)
            {
                // Animate to the right
                await this.RotateTo(currentRotation, 100);
                currentRotation += 10;
            }

        }

        public void StopRotationAnimation()
        {
            _animationCancellationTokenSource?.Cancel();

        }

        public LugButton()
        {
            BackgroundColor = Colors.Transparent;
            Padding = 0;
            BorderWidth = 0;
            FontSize = 30;
            ImageSource = "lug_deselected.png";
            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Top, 0);
        }

    }
}
