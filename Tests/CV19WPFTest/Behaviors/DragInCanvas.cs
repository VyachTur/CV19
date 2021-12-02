using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CV19WPFTest.Behaviors
{
    internal class DragInCanvas : Behavior<UIElement>
    {
        private Point _startPoint;

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += OnLeftButtonDown;
        }


        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnLeftButtonDown;
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.MouseMove -= OnMouseMove;
        }


        private void OnLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(AssociatedObject);
            AssociatedObject.CaptureMouse();
            AssociatedObject.MouseMove += OnMouseMove;
            AssociatedObject.MouseUp += OnMouseUp;
        }


        private void OnMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AssociatedObject.MouseUp -= OnMouseUp;
            AssociatedObject.MouseMove -= OnMouseMove;
            AssociatedObject.ReleaseMouseCapture();
        }
    }
}
