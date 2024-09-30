using System;
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

        double radius = 250;
        int containerSize = 1000;
        double radiusMultiplier = 3.0 * Buttons.Count() / (50.0 * 19) + (0.5 - (3 * 48.0) / (50 * 19));
        radius = radiusMultiplier * containerSize;
        Point center = new Point(containerSize / 2, containerSize / 2);

        double circumfrence = 2 * Math.PI * radius;
        double maxSize = radius * .55;
        int index = 0;
        foreach (LugButton btn in Buttons)
        {
            double size = (circumfrence / Buttons.Count);// * .90;
            if (size > maxSize) size = maxSize;

            double radians = index * 2 * Math.PI / Buttons.Count;
            double x = center.X + radius * Math.Sin(radians) - size / 2;
            double y = center.Y - radius * Math.Cos(radians) - size / 2;
            this.Add(btn);
            this.SetLayoutBounds(btn, new Microsoft.Maui.Graphics.Rect(x, y, size, size));
            //this.SetLayoutFlags(btn, Microsoft.Maui.Layouts.AbsoluteLayoutFlags.PositionProportional);
            index++;
        }

    }

    public CircleLayout()
	{
		InitializeComponent();

        DrawACircle();
	}
}