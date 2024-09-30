using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AbsoluteLayoutExperiments;

public partial class CircleLayout : AbsoluteLayout
{
    public static readonly BindableProperty ButtonsProperty =
    BindableProperty.Create(nameof(Buttons), typeof(ObservableCollection<LugButton>), typeof(LugButton), new ObservableCollection<LugButton>(){new LugButton(), new LugButton(), new LugButton()},
        propertyChanged: (bindable, oldVal, newVal) => {
            foreach(var button in (ObservableCollection<LugButton>)newVal)
            {
                button.Command = ((CircleLayout)bindable).ButtonClickedCommand;
                button.CommandParameter = button;
            }
            ((CircleLayout)bindable).DrawACircle();  
        });

    public ObservableCollection<LugButton> Buttons
    {
        get
        {
            return (ObservableCollection<LugButton>)GetValue(ButtonsProperty);
        }
        set
        {
            SetValue(ButtonsProperty, value);
        }
    }

    public static readonly BindableProperty ButtonClickedCommandProperty  =
        BindableProperty.Create(nameof(ButtonClickedCommand), typeof(ICommand), typeof(CircleLayout), 
            propertyChanged : (bindable, oldVal, newVal) => {
                foreach (var button in ((CircleLayout)bindable).Buttons)
                {
                    button.Command = ((CircleLayout)bindable).ButtonClickedCommand;
                    button.CommandParameter = button;
                }
            });

    public ICommand ButtonClickedCommand
    {
        get
        {
            return (ICommand)GetValue(ButtonClickedCommandProperty);
        }
        set
        {
            SetValue(ButtonClickedCommandProperty, value);
        }
    }

    void DrawACircle()
    {
        this.Clear();
        double x = 1;
        foreach(LugButton btn in Buttons)
        {
            this.Add(btn);
            this.SetLayoutBounds(btn, new Microsoft.Maui.Graphics.Rect(x, 2, 200, 60));
            x += 220;
        }
    }

    public CircleLayout()
	{
		InitializeComponent();

        DrawACircle();
	}
}