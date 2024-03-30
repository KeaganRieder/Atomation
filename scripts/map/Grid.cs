using System.Collections.Generic;
using Atomation.Thing;
using Godot;

namespace Atomation.Map
{

    /// <summary>
    /// the games grid in which all objects are aligned to
    /// </summary>
    public class Grid<GridObj> where GridObj : ICompThing
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float CellSize { get; private set; }
        public Vector2 Origin { get; private set; }
        private GridObj[,] gridArray;
        private Node2D parentNode;

        private List<Line2D> outLines;

        public Grid(int width, int height, float cellSize, Vector2 origin, Node2D node)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            Origin = origin;

            parentNode = node;

            InitializeGrid();
        }

        /// <summary>
        /// initializes the grid
        /// </summary>
        private void InitializeGrid()
        {
            outLines = new List<Line2D>();
            gridArray = new GridObj[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    gridArray[x, y] = default;
                }
            }
            CreateOutline();
        }

        /// <summary>
        /// creates outline 
        /// </summary>
        private void CreateOutline()
        {
            Line2D line;
            Color color = Colors.Black;
            int width = 1;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    line = new Line2D();
                    line.AddPoint(GetWorldPosition(x, y));
                    line.AddPoint(GetWorldPosition(x, y + 1));
                    line.VisibilityLayer = 3;
                    line.Width = width;
                    line.DefaultColor = color;
                    parentNode.AddChild(line);
                    outLines.Add(line);

                    line = new Line2D();
                    line.AddPoint(GetWorldPosition(x, y));
                    line.AddPoint(GetWorldPosition(x + 1, y));
                    line.VisibilityLayer = 3;
                    line.Width = width;
                    line.DefaultColor = color;

                    parentNode.AddChild(line);
                    outLines.Add(line);
                }
            }
            line = new Line2D();
            line.AddPoint(GetWorldPosition(0, 0));
            line.AddPoint(GetWorldPosition(0, Height));
            line.VisibilityLayer = 2;
            line.Width = width+2;
            line.DefaultColor = color;

            parentNode.AddChild(line);
            outLines.Add(line);

            line = new Line2D();
            line.AddPoint(GetWorldPosition(0, 0));
            line.AddPoint(GetWorldPosition(Width, 0));
            line.VisibilityLayer = 2;
            line.Width = width+2;
            line.DefaultColor = color;

            parentNode.AddChild(line);
            outLines.Add(line);

            line = new Line2D();
            line.AddPoint(GetWorldPosition(Width, Height));
            line.AddPoint(GetWorldPosition(0, Height));
            line.VisibilityLayer = 2;
            line.Width = width+2;
            line.DefaultColor = color;

            parentNode.AddChild(line);
            outLines.Add(line);

            line = new Line2D();
            line.AddPoint(GetWorldPosition(Width, Height));
            line.AddPoint(GetWorldPosition(Width, 0));
            line.VisibilityLayer = 2;
            line.Width = width+2;
            line.DefaultColor = color;

            parentNode.AddChild(line);
            outLines.Add(line);
        }

        /// <summary>
        /// converts the given cords to be based on CellSize
        /// </summary>
        private Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * CellSize;// +  Origin;
        }

        /// <summary>
        /// converts the given cords to be based on CellSize
        /// </summary>
        private void GetXY(Vector2 worldPosition, out int x, out int y)
        {
            x = Mathf.Abs(Mathf.FloorToInt((worldPosition - Origin).X));// /CellSize
            y = Mathf.Abs(Mathf.FloorToInt((worldPosition - Origin).Y));// /CellSize
        }

        /// <summary>
        /// sets the object at given cords
        /// </summary>
        public void SetObject(int x, int y, GridObj obj)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Width)
            {
                gridArray[x, y] = obj;
                parentNode.AddChild(obj.Node);
            }
            else
            {
                GD.PrintErr($"{x},{y} are out of bounds for current grid");
            }
        }

        /// <summary>
        /// sets the object at given cords
        /// </summary>
        public void SetObject(Vector2 worldPosition, GridObj obj)
        {
            GetXY(worldPosition, out int x, out int y);

            SetObject(x, y, obj);
        }

        /// <summary>
        /// returns the object at given cords
        /// </summary>
        public GridObj GetObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < Width && y < Width)
            {
                return gridArray[x, y];
            }
            else
            {
                GD.PrintErr($"{x},{y} are out of bounds for current grid");
                return default;
            }
        }

        /// <summary>
        /// returns the object at given cords
        /// </summary>
        public GridObj GetObject(Vector2 worldPosition)
        {
            GetXY(worldPosition, out int x, out int y);
            return GetObject(x, y);
        }
    }

}