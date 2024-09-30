using System.Collections.ObjectModel;

namespace AbsoluteLayoutExperiments;

public partial class CircleLayout : AbsoluteLayout
{

    public static readonly BindableProperty ButtonsProperty =
    BindableProperty.Create(nameof(Buttons), typeof(ObservableCollection<Button>), typeof(Button), new ObservableCollection<Button>(){new Button(), new Button(), new Button()},
        propertyChanged: (bindable, oldVal, newVal) => { ((CircleLayout)bindable).DrawACircle();  });

    public ObservableCollection<Button> Buttons
    {
        get
        {
            return (ObservableCollection<Button>)GetValue(ButtonsProperty);
        }
        set
        {
            SetValue(ButtonsProperty, value);
        }
    }

     void DrawACircle()
    {
        this.Clear();
        double x = 1;
        foreach(Button btn in Buttons)
        {
            this.Add(btn);
            this.SetLayoutBounds(btn, new Microsoft.Maui.Graphics.Rect(x, 2, 60, 200));
            x += 220;
        }
    }

    public CircleLayout()
	{
		InitializeComponent();

        DrawACircle();
	}
}