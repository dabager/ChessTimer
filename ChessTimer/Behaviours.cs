using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ChessTimer
{
    public static class ScaleToWindowSizeBehavior
    {
        #region ParentWindow

        public static readonly DependencyProperty ParentWindowProperty =
            DependencyProperty.RegisterAttached("ParentWindow",
                                                 typeof(Window),
                                                 typeof(ScaleToWindowSizeBehavior),
                                                 new FrameworkPropertyMetadata(null, OnParentWindowChanged));

        public static void SetParentWindow(FrameworkElement element, Window value)
        {
            element.SetValue(ParentWindowProperty, value);
        }

        public static Window GetParentWindow(FrameworkElement element)
        {
            return (Window)element.GetValue(ParentWindowProperty);
        }

        private static void OnParentWindowChanged(DependencyObject target,
                                                  DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement mainElement = target as FrameworkElement;
            Window window = e.NewValue as Window;

            SetDenominators(target, new Size(window.Width, window.Height));

            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.CenterX = 0;
            scaleTransform.CenterY = 0;


            Binding scaleValueBinding = new Binding
            {
                Source = window,
                Path = new PropertyPath(ScaleValueProperty)
            };

            Binding scaleXValueBinding = new Binding
            {
                Source = window,
                Path = new PropertyPath(ScaleXValueProperty)
            };
            Binding scaleYValueBinding = new Binding
            {
                Source = window,
                Path = new PropertyPath(ScaleYValueProperty)
            };

            BindingOperations.SetBinding(scaleTransform, ScaleTransform.ScaleXProperty, scaleXValueBinding);
            BindingOperations.SetBinding(scaleTransform, ScaleTransform.ScaleYProperty, scaleYValueBinding);
            mainElement.LayoutTransform = scaleTransform;
            mainElement.SizeChanged += mainElement_SizeChanged;
        }

        #endregion // ParentWindow

        #region ScaleValue

        public static readonly DependencyProperty ScaleValueProperty =
            DependencyProperty.RegisterAttached("ScaleValue",
                                                typeof(double),
                                                typeof(ScaleToWindowSizeBehavior),
                                                new UIPropertyMetadata(1.0, OnScaleValueChanged, OnCoerceScaleValue));

        public static double GetScaleValue(DependencyObject target)
        {
            return (double)target.GetValue(ScaleValueProperty);
        }
        public static void SetScaleValue(DependencyObject target, double value)
        {
            target.SetValue(ScaleValueProperty, value);
        }

        public static readonly DependencyProperty ScaleXValueProperty =
            DependencyProperty.RegisterAttached("ScaleXValue",
                                                typeof(double),
                                                typeof(ScaleToWindowSizeBehavior),
                                                new UIPropertyMetadata(1.0, OnScaleValueChanged, OnCoerceScaleValue));

        public static double GetScaleXValue(DependencyObject target)
        {
            return (double)target.GetValue(ScaleXValueProperty);
        }
        public static void SetScaleXValue(DependencyObject target, double value)
        {
            target.SetValue(ScaleXValueProperty, value);
        }


        public static readonly DependencyProperty ScaleYValueProperty =
            DependencyProperty.RegisterAttached("ScaleYValue",
                                                typeof(double),
                                                typeof(ScaleToWindowSizeBehavior),
                                                new UIPropertyMetadata(1.0, OnScaleValueChanged, OnCoerceScaleValue));

        public static double GetScaleYValue(DependencyObject target)
        {
            return (double)target.GetValue(ScaleYValueProperty);
        }
        public static void SetScaleYValue(DependencyObject target, double value)
        {
            target.SetValue(ScaleYValueProperty, value);
        }

        private static void OnScaleValueChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
        }

        private static object OnCoerceScaleValue(DependencyObject d, object baseValue)
        {
            if (baseValue is double)
            {
                double value = (double)baseValue;
                if (double.IsNaN(value))
                {
                    return 1.0f;
                }
                value = Math.Max(0.1, value);
                return value;
            }
            return 1.0f;
        }

        private static void mainElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement mainElement = sender as FrameworkElement;
            Window window = GetParentWindow(mainElement);
            CalculateScale(window, mainElement);
        }

        private static void CalculateScale(Window window, FrameworkElement mainElement)
        {
            Size denominators = GetDenominators(mainElement);
            double xScale = window.ActualWidth / denominators.Width;
            double yScale = window.ActualHeight / denominators.Height;
            double value = Math.Min(xScale, yScale);
            SetScaleValue(window, value);
            SetScaleXValue(window, xScale);
            SetScaleYValue(window, yScale);
        }

        #endregion // ScaleValue

        #region Denominators
        public static readonly DependencyProperty DenominatorsProperty =
    DependencyProperty.RegisterAttached("Denominators",
                                        typeof(Size),
                                        typeof(ScaleToWindowSizeBehavior),
                                        new UIPropertyMetadata(new Size(1000.0, 700.0)));

        public static Size GetDenominators(DependencyObject target)
        {
            return (Size)target.GetValue(DenominatorsProperty);
        }
        public static void SetDenominators(DependencyObject target, Size value)
        {
            target.SetValue(DenominatorsProperty, value);
        }

        #endregion // Denominators
    }
}
