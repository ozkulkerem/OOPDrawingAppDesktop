using NesneyeDayaliProgramlamaYL.Utils.Base;
using NesneyeDayaliProgramlamaYL.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneyeDayaliProgramlamaYL.Utils.Base.Shapes
{
    public class Rectangle : Shape
    {
        public override ShapeType ShapeType => ShapeType.Rectangle;
        public Rectangle(Color color, Point startPoint, Point endPoint) : base(color, startPoint, endPoint)
        {
        }

        public override void Draw(Graphics g)
        {
            int width = Math.Abs(EndPoint.X - StartPoint.X);
            int height = Math.Abs(EndPoint.Y - StartPoint.Y);
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);

            using (Brush brush = new SolidBrush(Color))
            {
                g.FillRectangle(brush, x, y, width, height);
            }

            if (Selected)
            {
                using (Brush backgroundBrush = new SolidBrush(Color.FromArgb(200, 216, 197, 224)))
                {
                    g.FillRectangle(backgroundBrush, x - 10, y - 10, width + 20, height + 20);
                }
                using (Pen pen = new Pen(Color.FromArgb(128, Color.Blue), 2))
                {
                    pen.DashStyle = DashStyle.Dot;
                    g.DrawRectangle(pen, x - 10, y - 10, width + 20, height + 20);
                }
            }
        }
        public override bool IsPointInside(Point point)
        {
            int width = Math.Abs(EndPoint.X - StartPoint.X);
            int height = Math.Abs(EndPoint.Y - StartPoint.Y);
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);

            return point.X >= x && point.X <= x + width && point.Y >= y && point.Y <= y + height;
        }
    }
}
