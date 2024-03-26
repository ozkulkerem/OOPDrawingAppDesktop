using NesneyeDayaliProgramlamaYL.Utils.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneyeDayaliProgramlamaYL.Utils.Base
{
    [JsonConverter(typeof(ShapeJsonConverter))]
    public abstract class Shape
    {
        public abstract ShapeType ShapeType { get; }
        public Color Color { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public bool Selected { get; set; }

        protected Shape(Color color, Point startPoint, Point endPoint)
        {
            Color = color;
            StartPoint = startPoint;
            EndPoint = endPoint;
        }

        public abstract void Draw(Graphics g);
        public abstract bool IsPointInside(Point point);
    }
}