﻿using System;
using System.ComponentModel;
using AndroidX.Fragment.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.FastRenderers;
using q5id.guardian.Controls;
using q5id.guardian.Droid.Renderers;
using q5id.guardian.Droid.Controls;

[assembly: ExportRenderer(typeof(CameraPreview), typeof(CameraPreviewRenderer))]
namespace q5id.guardian.Droid.Renderers
{
    public class CameraPreviewRenderer : FrameLayout, IVisualElementRenderer, IViewRenderer
    {
        int? defaultLabelFor;
        bool disposed;
        CameraPreview element;
        VisualElementTracker visualElementTracker;
        VisualElementRenderer visualElementRenderer;
        FragmentManager fragmentManager;
        CameraFragment cameraFragment;

        FragmentManager FragmentManager => fragmentManager ??= Context.GetFragmentManager();

        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
        public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;

        CameraPreview Element
        {
            get => element;
            set
            {
                if (element == value)
                {
                    return;
                }

                var oldElement = element;
                element = value;
                OnElementChanged(new ElementChangedEventArgs<CameraPreview>(oldElement, element));
            }
        }

        public CameraPreviewRenderer(Context context) : base(context)
        {
            visualElementRenderer = new VisualElementRenderer(this);
        }

        void OnElementChanged(ElementChangedEventArgs<CameraPreview> e)
        {
            CameraFragment newFragment = null;

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
                cameraFragment.Dispose();
            }
            if (e.NewElement != null)
            {
                this.EnsureId();

                e.NewElement.PropertyChanged += OnElementPropertyChanged;

                ElevationHelper.SetElevation(this, e.NewElement);
                newFragment = new CameraFragment { Element = element };
            }

            FragmentManager.BeginTransaction()
                .Replace(Id, cameraFragment = newFragment, "camera")
                .Commit();
            ElementChanged?.Invoke(this, new VisualElementChangedEventArgs(e.OldElement, e.NewElement));
        }

        async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ElementPropertyChanged?.Invoke(this, e);
            if(e.PropertyName == "Width")
            {
                await cameraFragment.RetrieveCameraDevice();
            }
            else if(e.PropertyName == CameraPreview.CameraProperty.PropertyName)
            {
                UpdateCamera();
            }
        }

        private void UpdateCamera()
        {
            if(cameraFragment != null)
            {
                cameraFragment.Dispose();
            }
            CameraFragment newFragment = new CameraFragment { Element = element };
            FragmentManager.BeginTransaction()
                .Replace(Id, cameraFragment = newFragment, "camera")
                .Commit();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            cameraFragment.Dispose();
            disposed = true;

            if (disposing)
            {
                SetOnClickListener(null);
                SetOnTouchListener(null);

                if (visualElementTracker != null)
                {
                    visualElementTracker.Dispose();
                    visualElementTracker = null;
                }

                if (visualElementRenderer != null)
                {
                    visualElementRenderer.Dispose();
                    visualElementRenderer = null;
                }

                if (Element != null)
                {
                    Element.PropertyChanged -= OnElementPropertyChanged;

                    if (Platform.GetRenderer(Element) == this)
                    {
                        Platform.SetRenderer(Element, null);
                    }
                }
            }

            base.Dispose(disposing);
        }

        #region IViewRenderer

        void IViewRenderer.MeasureExactly() => MeasureExactly(this, Element, Context);

        static void MeasureExactly(Android.Views.View control, VisualElement element, Context context)
        {
            if (control == null || element == null)
            {
                return;
            }

            double width = element.Width;
            double height = element.Height;

            if (width <= 0 || height <= 0)
            {
                return;
            }

            int realWidth = (int)context.ToPixels(width);
            int realHeight = (int)context.ToPixels(height);

            int widthMeasureSpec = MeasureSpecFactory.MakeMeasureSpec(realWidth, MeasureSpecMode.Exactly);
            int heightMeasureSpec = MeasureSpecFactory.MakeMeasureSpec(realHeight, MeasureSpecMode.Exactly);

            control.Measure(widthMeasureSpec, heightMeasureSpec);
        }

        #endregion

        #region IVisualElementRenderer

        VisualElement IVisualElementRenderer.Element => Element;

        VisualElementTracker IVisualElementRenderer.Tracker => visualElementTracker;

        ViewGroup IVisualElementRenderer.ViewGroup => null;

        Android.Views.View IVisualElementRenderer.View => this;

        SizeRequest IVisualElementRenderer.GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            Measure(widthConstraint, heightConstraint);
            SizeRequest result = new SizeRequest(new Size(MeasuredWidth, MeasuredHeight), new Size(Context.ToPixels(20), Context.ToPixels(20)));
            return result;
        }

        void IVisualElementRenderer.SetElement(VisualElement element)
        {
            if (!(element is CameraPreview camera))
            {
                throw new ArgumentException($"{nameof(element)} must be of type {nameof(CameraPreview)}");
            }

            if (visualElementTracker == null)
            {
                visualElementTracker = new VisualElementTracker(this);
            }
            Element = camera;
        }

        void IVisualElementRenderer.SetLabelFor(int? id)
        {
            if (defaultLabelFor == null)
            {
                defaultLabelFor = LabelFor;
            }
            LabelFor = (int)(id ?? defaultLabelFor);
        }

        void IVisualElementRenderer.UpdateLayout() => visualElementTracker?.UpdateLayout();

        #endregion

        static class MeasureSpecFactory
        {
            public static int GetSize(int measureSpec)
            {
                const int modeMask = 0x3 << 30;
                return measureSpec & ~modeMask;
            }

            public static int MakeMeasureSpec(int size, MeasureSpecMode mode) => size + (int)mode;
        }
    }
}
