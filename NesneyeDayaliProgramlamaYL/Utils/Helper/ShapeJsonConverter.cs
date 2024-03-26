using NesneyeDayaliProgramlamaYL.Utils.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneyeDayaliProgramlamaYL.Utils.Helper
{
    public class ShapeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Shape).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            ShapeType shapeType = obj["ShapeType"].ToObject<ShapeType>(serializer);
            obj.Remove("ShapeType");
            Shape shape;

            Color color = obj["Color"].ToObject<Color>(serializer);
            Point startPoint = obj["StartPoint"].ToObject<Point>(serializer);
            Point endPoint = obj["EndPoint"].ToObject<Point>(serializer);

            switch (shapeType)
            {
                case ShapeType.Circle:
                    shape = new Utils.Base.Shapes.Circle(color, startPoint, endPoint);
                    break;
                case ShapeType.Rectangle:
                    shape = new Utils.Base.Shapes.Rectangle(color, startPoint, endPoint);
                    break;
                case ShapeType.Triangle:
                    shape = new Utils.Base.Shapes.Triangle(color, startPoint, endPoint);
                    break;
                case ShapeType.Hexagon:
                    shape = new Utils.Base.Shapes.Hexagon(color, startPoint, endPoint);
                    break;
                default:
                    throw new NotSupportedException($"Unknown shape type: {shapeType}");
            }

            serializer.Populate(obj.CreateReader(), shape);
            return shape;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JObject obj = new JObject();
            Shape shape = (Shape)value;
            obj["ShapeType"] = JToken.FromObject(shape.ShapeType, serializer);
            obj["Color"] = JToken.FromObject(shape.Color, serializer);
            obj["StartPoint"] = JToken.FromObject(shape.StartPoint, serializer);
            obj["EndPoint"] = JToken.FromObject(shape.EndPoint, serializer);

            var contractResolver = serializer.ContractResolver as DefaultContractResolver;
            var shapeContract = contractResolver?.ResolveContract(shape.GetType()) as JsonObjectContract;

            if (shapeContract != null)
            {
                foreach (var property in shapeContract.Properties)
                {
                    if (property.DeclaringType == shape.GetType())
                    {
                        var propertyValue = property.ValueProvider.GetValue(shape);
                        obj[property.PropertyName] = JToken.FromObject(propertyValue, serializer);
                    }
                }
            }

            obj.WriteTo(writer);
        }
    }

}
