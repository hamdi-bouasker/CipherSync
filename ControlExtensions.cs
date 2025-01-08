using System.Drawing;
using System.Windows.Forms;

namespace CipherShield
{
    

    public static class ControlExtensions
    {
        private static bool dragging = false;
        private static Point dragCursorPoint;
        private static Point dragFormPoint;

        public static void MakeDraggable(this Control control, Form form)
        {
            control.MouseDown += (s, e) =>
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = form.Location;
            };

            control.MouseMove += (s, e) =>
            {
                if (dragging)
                {
                    Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    form.Location = Point.Add(dragFormPoint, new Size(diff));
                }
            };

            control.MouseUp += (s, e) => dragging = false;
        }
    }

}
