using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Graphics.Drawables;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Graphics;
using Pos.Droid.Controls;
using Xamarin.Forms;
using System.Threading.Tasks;
using Pos.Controls;
using Color = Xamarin.Forms.Color;
using View = Android.Views.View;
using Pos.Droid.Extensions;

[assembly: ExportRenderer(typeof(ImageButton), typeof(ImageButtonRenderer))]
namespace Pos.Droid.Controls
{
    public partial class ImageButtonRenderer : ButtonRenderer
    {
        private static float _density = float.MinValue;

        /// <summary>
        /// Gets the underlying control typed as an <see cref="ImageButton"/>.
        /// </summary>
        private ImageButton ImageButton
        {
            get { return (ImageButton)Element; }
        }

        /// <summary>
        /// Sets up the button including the image. 
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected async override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            _density = Resources.DisplayMetrics.Density;

            var targetButton = Control;
            if (targetButton != null) targetButton.SetOnTouchListener(TouchListener.Instance.Value);

            if (Element != null && Element.Font != Font.Default && targetButton != null) targetButton.Typeface = Element.Font.ToExtendedTypeface(Context);

            if (Element != null && ImageButton.Source != null) await SetImageSourceAsync(targetButton, ImageButton).ConfigureAwait(false);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && Control != null)
            {
                Control.Dispose();
            }
        }


        /// <summary>
        /// Sets the image source.
        /// </summary>
        /// <param name="targetButton">The target button.</param>
        /// <param name="model">The model.</param>
        /// <returns>A <see cref="Task"/> for the awaited operation.</returns>
        private async Task SetImageSourceAsync(Android.Widget.Button targetButton, ImageButton model)
        {
            try
            {
                if (targetButton == null || targetButton.Handle == IntPtr.Zero || model == null) return;

                // const int Padding = 10;
                var source = model.IsEnabled ? model.Source : model.DisabledSource ?? model.Source;

                using (var bitmap = await GetBitmapAsync(source).ConfigureAwait(false))
                {
                    if (bitmap == null)
                        targetButton.SetCompoundDrawables(null, null, null, null);
                    else
                    {
                        var drawable = new BitmapDrawable(bitmap);
                        var tintColor = model.IsEnabled ? model.ImageTintColor : model.DisabledImageTintColor;
                        if (tintColor != Color.Transparent)
                        {
                            drawable.SetTint(tintColor.ToAndroid());
                            drawable.SetTintMode(PorterDuff.Mode.SrcIn);
                        }

                        using (var scaledDrawable = GetScaleDrawable(drawable, GetWidth(model.ImageWidthRequest), GetHeight(model.ImageHeightRequest)))
                        {
                            Drawable left = null;
                            Drawable right = null;
                            Drawable top = null;
                            Drawable bottom = null;
                            //System.Diagnostics.Debug.WriteLine($"SetImageSourceAsync intptr{targetButton.Handle}");
                            int padding = 10; // model.Padding
                            targetButton.CompoundDrawablePadding = RequestToPixels(padding);
                            switch (model.Orientation)
                            {
                                case ImageOrientation.ImageToLeft:
                                    targetButton.Gravity = GravityFlags.Left | GravityFlags.CenterVertical;
                                    left = scaledDrawable;
                                    break;
                                case ImageOrientation.ImageToRight:
                                    targetButton.Gravity = GravityFlags.Right | GravityFlags.CenterVertical;
                                    right = scaledDrawable;
                                    break;
                                case ImageOrientation.ImageOnTop:
                                    targetButton.Gravity = GravityFlags.Top | GravityFlags.CenterHorizontal;
                                    top = scaledDrawable;
                                    break;
                                case ImageOrientation.ImageOnBottom:
                                    targetButton.Gravity = GravityFlags.Bottom | GravityFlags.CenterHorizontal;
                                    bottom = scaledDrawable;
                                    break;
                                case ImageOrientation.ImageCentered:
                                    targetButton.Gravity = GravityFlags.Center; // | GravityFlags.Fill;
                                    top = scaledDrawable;
                                    break;
                            }

                            targetButton.SetCompoundDrawables(left, top, right, bottom);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// Gets a <see cref="Bitmap"/> for the supplied <see cref="ImageSource"/>.
        /// </summary>
        /// <param name="source">The <see cref="ImageSource"/> to get the image for.</param>
        /// <returns>A loaded <see cref="Bitmap"/>.</returns>
        private async Task<Bitmap> GetBitmapAsync(ImageSource source)
        {
            var handler = GetHandler(source);
            var returnValue = (Bitmap)null;

            if (handler != null)
                returnValue = await handler.LoadImageAsync(source, Context).ConfigureAwait(false);

            return returnValue;
        }
        private static IImageSourceHandler GetHandler(ImageSource source)
        {
            IImageSourceHandler returnValue = null;
            if (source is UriImageSource)
            {
                returnValue = new ImageLoaderSourceHandler();
            }
            else if (source is FileImageSource)
            {
                returnValue = new FileImageSourceHandler();
            }
            else if (source is StreamImageSource)
            {
                returnValue = new StreamImagesourceHandler();
            }
            return returnValue;
        }
        private int GetWidth(int requestedWidth)
        {
            const int DefaultWidth = 50;
            return requestedWidth <= 0 ? DefaultWidth : requestedWidth;
        }
        private int GetHeight(int requestedHeight)
        {
            const int DefaultHeight = 50;
            return requestedHeight <= 0 ? DefaultHeight : requestedHeight;
        }
        /// <summary>
        /// Called when the underlying model's properties are changed.
        /// </summary>
        /// <param name="sender">The Model used.</param>
        /// <param name="e">The event arguments.</param>
        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ImageButton.SourceProperty.PropertyName ||
                e.PropertyName == ImageButton.DisabledSourceProperty.PropertyName ||
                e.PropertyName == VisualElement.IsEnabledProperty.PropertyName ||
                e.PropertyName == ImageButton.ImageTintColorProperty.PropertyName ||
                e.PropertyName == ImageButton.DisabledImageTintColorProperty.PropertyName)
            {
                await SetImageSourceAsync(Control, ImageButton).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Returns a <see cref="Drawable"/> with the correct dimensions from an 
        /// Android resource id.
        /// </summary>
        /// <param name="drawable">An android <see cref="Drawable"/>.</param>
        /// <param name="width">The width to scale to.</param>
        /// <param name="height">The height to scale to.</param>
        /// <returns>A scaled <see cref="Drawable"/>.</returns>
        private Drawable GetScaleDrawable(Drawable drawable, int width, int height)
        {
            var returnValue = new ScaleDrawable(drawable, 0, 100, 100).Drawable;

            returnValue.SetBounds(0, 0, RequestToPixels(width), RequestToPixels(height));

            return returnValue;
        }

        /// <summary>
        /// Returns a drawable dimension modified according to the current display DPI.
        /// </summary>
        /// <param name="sizeRequest">The requested size in relative units.</param>
        /// <returns>Size in pixels.</returns>
        public int RequestToPixels(int sizeRequest)
        {
            if (_density == float.MinValue)
            {
                if (Resources.Handle == IntPtr.Zero || Resources.DisplayMetrics.Handle == IntPtr.Zero)
                    _density = 1.0f;
                else
                    _density = Resources.DisplayMetrics.Density;
            }

            return (int)(sizeRequest * _density);
        }
    }

    class TouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        public static readonly Lazy<TouchListener> Instance = new Lazy<TouchListener>(() => new TouchListener());

        /// <summary>
        /// Make TouchListener a singleton.
        /// </summary>
        private TouchListener()
        { }

        public bool OnTouch(View v, MotionEvent e)
        {
            var buttonRenderer = v.Tag as ButtonRenderer;
            if (buttonRenderer != null && e.Action == MotionEventActions.Down) buttonRenderer.Control.Text = buttonRenderer.Element.Text;

            return false;
        }
    }
}