using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Java.Interop;
using QLQuanCafe.Customers;
using QLQuanCafe.Droid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace QLQuanCafe.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context) { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(Entry.IsPassword))
            {
                UpdateIsPassword();
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null && e.NewElement != null)
            {
                // Set background to transparent
                GradientDrawable gradientDrawable = new GradientDrawable();
                gradientDrawable.SetColor(Android.Graphics.Color.Transparent);
                Control.SetBackground(gradientDrawable);

                /*// Remove suggestions
                Control.InputType = InputTypes.TextFlagNoSuggestions;*/
                // Set input type to number
                if (((Entry)e.NewElement).Keyboard == Keyboard.Numeric)
                {
                    // Set input type to number if the Entry's Keyboard is set to Numeric
                    Control.InputType = InputTypes.ClassNumber;
                }

                // Set hint color
                Control.SetHintTextColor(Android.Graphics.Color.AliceBlue);

                // Update IsPassword when Element changes
                UpdateIsPassword();
            }
        }

        private void UpdateIsPassword()
        {
            if (Control != null && Element is Entry entry)
            {
                Control.TransformationMethod = entry.IsPassword ? PasswordTransformationMethod.Instance : null;
            }
        }


    }
}