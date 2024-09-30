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
        
        //public static readonly BindableProperty IsSelectedProperty =
        //    BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(LugButton), false,
        //     propertyChanged: (bindable, oldVal, newVal) =>
        //     {

        //     });

        //public bool IsSelected
        //{
        //    get
        //    {
        //        return (bool)GetValue(IsSelectedProperty);
        //    }
        //    set
        //    {
        //        SetValue(IsSelectedProperty, value);
        //    }
        //}

    }
}
