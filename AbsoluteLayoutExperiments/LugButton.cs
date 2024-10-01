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

        public enum LugStatus
        {
            TODO,
            CURRENT,
            CURRENT_DO_OVER,
            SKIPPED,
            SUCCESS,
            PAUSED,
            FAILED
        }

        public static readonly BindableProperty StatusProperty =
            BindableProperty.Create(nameof(Status), typeof(LugStatus), typeof(LugButton), LugStatus.TODO,
             propertyChanged: (bindable, oldVal, newVal) =>
             {
                 LugButton thisButton = (LugButton)bindable;
                 switch ((LugStatus)newVal)
                 {
                     case (LugStatus.TODO):
                         {
                             thisButton.StopRotationAnimation();
                             thisButton.ImageSource = "lug_todo.png";
                             break;
                         }
                     case (LugStatus.CURRENT):
                         {
                             thisButton.StartRotationAnimation();
                             thisButton.ImageSource = "lug_current.png";
                             break;
                         }
                     case (LugStatus.CURRENT_DO_OVER):
                         {
                             thisButton.StartRotationAnimation();
                             thisButton.ImageSource = "lug_current_do_over.png";
                             break;
                         }
                     case (LugStatus.PAUSED):
                         {
                             thisButton.StopRotationAnimation();
                             ((LugButton)bindable).ImageSource = "lug_paused.png";
                             break;
                         }
                     case (LugStatus.SUCCESS):
                         {
                             thisButton.StopRotationAnimation();
                             ((LugButton)bindable).ImageSource = "lug_success.png";
                             break;
                         }
                     case (LugStatus.SKIPPED):
                         {
                             thisButton.StopRotationAnimation();
                             ((LugButton)bindable).ImageSource = "lug_skipped.png";
                             break;
                         }
                     case (LugStatus.FAILED):
                         {
                             thisButton.StopRotationAnimation();
                             thisButton.ImageSource = "lug_failed.png";
                             break;
                         }

                 }
             });

        public LugStatus Status
        {
            get
            {
                return (LugStatus)GetValue(StatusProperty);
            }
            set
            {
                SetValue(StatusProperty, value);
            }
        }

        CancellationTokenSource? _animationCancellationTokenSource;
        double currentRotation = 0;


        public async void StartRotationAnimation()
        {
            // Cancel any existing animation before starting a new one
            _animationCancellationTokenSource?.Cancel();
            await Task.Delay(100);

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
            ImageSource = "lug_todo.png";
            ContentLayout = new ButtonContentLayout(ButtonContentLayout.ImagePosition.Top, 0);
        }

    }
}
