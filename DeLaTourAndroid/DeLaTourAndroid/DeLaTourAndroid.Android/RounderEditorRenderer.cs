﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeLaTourAndroid;
using DeLaTourAndroid.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(RoundEditor), typeof(RounderEntryRenderer))]
namespace DeLaTourAndroid.Droid
{
    class RoundedEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = Forms.Context.GetDrawable(Resource.Drawable.RoundedEditText);
            }
        }
    }
}