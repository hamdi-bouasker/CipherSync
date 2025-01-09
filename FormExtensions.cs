using System.Drawing;
using System.Windows.Forms;

namespace CipherShield
{
    

    public static class FormExtensions
    {
        private static bool dragging = false;
        private static Point dragCursorPoint;
        private static Point dragFormPoint;

        // Make a form draggable
        public static void MakeDraggable(this Form form)
        {
            form.MouseDown += (s, e) =>
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = form.Location;
            };

            form.MouseMove += (s, e) =>
            {
                if (dragging)
                {
                    Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                    form.Location = Point.Add(dragFormPoint, new Size(diff));
                }
            };

            form.MouseUp += (s, e) => dragging = false;
        }
    }

}
