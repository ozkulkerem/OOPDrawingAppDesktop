using NesneyeDayaliProgramlamaYL.Utils.Base;
using NesneyeDayaliProgramlamaYL.Utils.Helper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NesneyeDayaliProgramlamaYL
{
    public partial class Main : Form
    {


        private List<Shape> shapes;
        private Shape tempShape;
        private ShapeType selectedShape;
        private Color selectedColor;
        private bool drawing;
        private Point startPoint;
        private Point endPoint;
        private Shape selectedShapeInstance;
        private bool drawMode;
        private List<Button> shapeButtons;
        private List<Button> colorButtons;
        private List<Button> operationButtons;

        public Main()
        {
            InitializeComponent();

            shapes = new List<Shape>();
            selectedShape = ShapeType.Rectangle;
            selectedColor = Color.Orange;
            drawing = false;
            drawMode = true;

            shapeButtons = new List<Button>();
            colorButtons = new List<Button>();
            operationButtons = new List<Button>();

            shapeButtons.Add(button1);
            shapeButtons.Add(button2);
            shapeButtons.Add(button3);
            shapeButtons.Add(button4);

            colorButtons.Add(button5);
            colorButtons.Add(button6);
            colorButtons.Add(button7);
            colorButtons.Add(button8);
            colorButtons.Add(button9);
            colorButtons.Add(button10);
            colorButtons.Add(button11);
            colorButtons.Add(button12);
            colorButtons.Add(button13);

            operationButtons.Add(button14);
            operationButtons.Add(button15);

            DefaultSelected();
        }
        private void DefaultSelected()
        {
            button1.FlatStyle = FlatStyle.Flat;
            button8.FlatStyle = FlatStyle.Flat;
        }

        private void DrawShape(Graphics g, Shape shape)
        {
            shape.Draw(g);
        }


        private void actionPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (drawMode)
                {
                    if (selectedShape != ShapeType.None)
                    {
                        drawing = true;
                        startPoint = e.Location;
                    }
                    if (selectedShapeInstance != null)
                    {
                        selectedShapeInstance.Selected = false;
                    }

                    selectedShapeInstance = null;
                }
                else
                {
                    if (selectedShapeInstance != null)
                    {
                        selectedShapeInstance.Selected = false;
                    }

                    selectedShapeInstance = null;
                    foreach (Shape shape in shapes)
                    {
                        if (shape.IsPointInside(e.Location))
                        {
                            selectedShapeInstance = shape;
                            shape.Selected = true;
                            break;
                        }
                    }
                    actionPanel.Invalidate();
                }
            }
        }

        private void actionPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                endPoint = e.Location;
                Shape shape;

                switch (selectedShape)
                {
                    case ShapeType.Rectangle:
                        shape = new Utils.Base.Shapes.Rectangle(selectedColor, startPoint, endPoint);
                        break;
                    case ShapeType.Circle:
                        shape = new Utils.Base.Shapes.Circle(selectedColor, startPoint, endPoint);
                        break;
                    case ShapeType.Triangle:
                        shape = new Utils.Base.Shapes.Triangle(selectedColor, startPoint, endPoint);
                        break;
                    case ShapeType.Hexagon:
                        shape = new Utils.Base.Shapes.Hexagon(selectedColor, startPoint, endPoint);
                        break;
                    default:
                        throw new NotImplementedException("Selected shape is not supported.");
                }

                shapes.Add(shape);
                drawing = false;
                actionPanel.Invalidate(); // Refresh the drawing area
            }
        }

        private void actionPanel_Paint(object sender, PaintEventArgs e)
        {
            foreach (Shape shape in shapes)
            {
                DrawShape(e.Graphics, shape);
            }

            if (drawing && selectedShape != ShapeType.None)
            {
                DrawShape(e.Graphics, tempShape);
            }
        }

        private void actionPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                endPoint = e.Location;

                switch (selectedShape)
                {
                    case ShapeType.Rectangle:
                        tempShape = new Utils.Base.Shapes.Rectangle(selectedColor, startPoint, endPoint);
                        break;
                    case ShapeType.Circle:
                        tempShape = new Utils.Base.Shapes.Circle(selectedColor, startPoint, endPoint);
                        break;
                    case ShapeType.Triangle:
                        tempShape = new Utils.Base.Shapes.Triangle(selectedColor, startPoint, endPoint);
                        break;
                    case ShapeType.Hexagon:
                        tempShape = new Utils.Base.Shapes.Hexagon(selectedColor, startPoint, endPoint);
                        break;
                    default:
                        throw new NotImplementedException("Selected shape is not supported.");
                }

                actionPanel.Invalidate(); // Refresh the drawing area
            }
        }
        private void ChangeSelectedShapeColor(Color newColor)
        {
            ChangeSelectedColor(newColor);
            if (selectedShapeInstance != null)
            {
                selectedShapeInstance.Color = newColor;
                actionPanel.Invalidate();
            }
        }
        private void ChangeSelectedColor(Color selectColor)
        {
            selectedColor = selectColor;
        }

        private void RemoveSelectedShape()
        {
            if (selectedShapeInstance != null)
            {
                shapes.Remove(selectedShapeInstance);
                selectedShapeInstance = null;
                actionPanel.Invalidate();
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "JSON Files (*.json)|*.json";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string json = File.ReadAllText(openDialog.FileName);
                    var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                    shapes = JsonConvert.DeserializeObject<List<Shape>>(json, jsonSettings);
                    actionPanel.Invalidate();
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "JSON Files (*.json)|*.json";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    var jsonSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                    string json = JsonConvert.SerializeObject(shapes, Formatting.Indented, jsonSettings);
                    File.WriteAllText(saveDialog.FileName, json);
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectedShape = ShapeType.Rectangle;
            drawing = false;
            drawMode = true;
            UpdateButtonStyles(sender,"shape");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectedShape = ShapeType.Circle;
            drawing = false;
            drawMode = true;
            UpdateButtonStyles(sender, "shape");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            selectedShape = ShapeType.Triangle;
            drawing = false;
            drawMode = true;
            UpdateButtonStyles(sender, "shape");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            selectedShape = ShapeType.Hexagon;
            drawing = false;
            drawMode = true;
            UpdateButtonStyles(sender, "shape");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.Red);
            UpdateButtonStyles(sender, "color");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.Blue);
            UpdateButtonStyles(sender, "color");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            drawMode = !drawMode;
            UpdateButtonStyles(sender, "operation");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!drawMode)
            {
                RemoveSelectedShape();
            }
            UpdateButtonStyles(sender, "operation");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.Green);
            UpdateButtonStyles(sender, "color");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.Orange);
            UpdateButtonStyles(sender, "color");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.Black);
            UpdateButtonStyles(sender, "color");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.Yellow);
            UpdateButtonStyles(sender, "color");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.Purple);
            UpdateButtonStyles(sender, "color");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.Brown);
            UpdateButtonStyles(sender, "color");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ChangeSelectedShapeColor(Color.White);
            UpdateButtonStyles(sender, "color");
        }

        private void UpdateButtonStyles(object sender, string type)
        {
            Button clickedButton = sender as Button;

            List<Button> buttons = new List<Button>();
            if (type == "shape")
            {
                buttons = shapeButtons;
                foreach (Button button in operationButtons)
                {
                    button.FlatStyle = FlatStyle.Standard;
                }
            }
            else if (type == "color")
            {
                buttons = colorButtons;
            }
            else if(type=="operation"){
                buttons = operationButtons;
                foreach (Button button in shapeButtons)
                {
                    button.FlatStyle = FlatStyle.Standard;
                }
            }

            foreach (Button button in buttons)
            {
                if (button == clickedButton)
                {
                    button.FlatStyle = FlatStyle.Flat;
                }
                else
                {
                    button.FlatStyle = FlatStyle.Standard;
                }
            }
        }
    }
}