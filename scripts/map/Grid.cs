namespace Atomation.Map;

using System.Collections.Generic;
using Atomation.Things;
using Godot;

public class Grid<GridObj> where GridObj : IThing
{
    private float cellSize;
    private Vector2I size;
    private Vector2 origin;

    private Node2D parentNode;

    private GridObj[,] gridArray;
    private List<Line2D> gridOutlines;

    public Grid(Vector2 size, Vector2 origin, Node2D parentNode, float cellSize = MapData.CELL_SIZE)
    {
        this.size = new Vector2I((int)size.X, (int)size.Y);
        this.origin = origin;
        this.parentNode = parentNode;
        this.cellSize = cellSize;

        InitializeGrid();
    }

    public Grid(Vector2 size, Coordinate coordinate, Node2D parentNode, float cellSize = MapData.CELL_SIZE)
    {
        this.size = new Vector2I((int)size.X, (int)size.Y);
        this.origin = coordinate.GetWorldPosition();
        this.parentNode = parentNode;
        this.cellSize = cellSize;

        InitializeGrid();
    }

    /// <summary> initializes the grid </summary>
    private void InitializeGrid()
    {
        gridArray = new GridObj[size.X, size.Y];
        gridOutlines = new List<Line2D>();

        for (int x = 0; x < size.X; x++)
        {
            for (int y = 0; y < size.Y; y++)
            {
                gridArray[x, y] = default;
            }
        }
        CreateOutline();
    }
    /// <summary> creates outline  </summary>
    private void CreateOutline()
    {
        Line2D line;
        Color color = Colors.Black;
        int width = 1;

        // grid bounds
        line = new Line2D();
        line.ZIndex = 10;
        line.AddPoint(GetWorldPosition(0, 0));
        line.AddPoint(GetWorldPosition(0, size.Y));
        line.AddPoint(GetWorldPosition(0, 0));
        line.AddPoint(GetWorldPosition(size.X, 0));
        line.AddPoint(GetWorldPosition(size.X, size.Y));
        line.AddPoint(GetWorldPosition(0, size.Y));
        line.AddPoint(GetWorldPosition(size.X, size.Y));
        line.AddPoint(GetWorldPosition(size.X, 0));
        line.VisibilityLayer = 2;
        line.Width = width + 2;
        line.DefaultColor = color;

        parentNode.AddChild(line);
        gridOutlines.Add(line);
    }

    /// <summary> gets world position based on provided pixel grid cords </summary>
    private Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * cellSize + origin;
    }

    /// <summary> converts the given cords to be based on the pixel grid </summary>
    private Vector2I GetXY(Vector2 worldPosition)
    {
        int x = Mathf.Abs(Mathf.FloorToInt((worldPosition - origin).X / cellSize));
        int y = Mathf.Abs(Mathf.FloorToInt((worldPosition - origin).Y / cellSize));
        return new Vector2I(x, y);
    }

    /// <summary> sets the object at given cords </summary>
    public void SetObject(int x, int y, GridObj obj)
    {
        if (x >= 0 && y >= 0 && x < size.X && y < size.Y)
        {
            gridArray[x, y] = obj;

            if (obj != null)
            {
                parentNode.AddChild(obj.GetNode());
            }
        }
    }

    /// <summary> sets the object at given cords </summary>
    public void SetObject(Coordinate cords, GridObj obj)
    {
        int x = cords.GetXYPosition().X;
        int y = cords.GetXYPosition().Y;
        SetObject(x, y, obj);

    }

    /// <summary> sets the object at given cords </summary>
    public void SetObject(Vector2 worldPosition, GridObj obj)
    {
        Vector2I cords = GetXY(worldPosition);
        SetObject(cords.X, cords.Y, obj);

    }

    /// <summary> returns the object at given cords </summary>
    public GridObj GetObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < size.X && y < size.Y)
        {
            return gridArray[x, y];
        }
        else
        {
            GD.PushError($"{x},{y} are out of bounds for current grid");
            return default;
        }
    }

    /// <summary> returns the object at given cords </summary>
    public GridObj GetObject(Vector2 worldPosition)
    {
        Vector2I cords = GetXY(worldPosition);
        return GetObject(cords.X, cords.Y);
    }

    /// <summary> returns the object at given cords </summary>
    public GridObj GetObject(Coordinate cord)
    {
        int x = cord.GetXYPosition().X;
        int y = cord.GetXYPosition().Y;
        return GetObject(x, y);
    }


}