namespace Atomation.Systems;

using Godot;

/// <summary>
/// the game clocks handles keeping track of the games time, in terms
///  of hours, and days passed
/// </summary>
public partial class GameClock : Node
{
    private static GameClock instance;
    public static GameClock Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }
    }

    [Signal]
    public delegate void NewMinuteEventHandler();
    [Signal]

    public delegate void NewHourEventHandler();
    public delegate void NewDayEventHandler();

    private int lastUpdate = 0;

    private int totalMinutes = 0;
    private int totalHours = 0;
    private int totalDays = 0;
    private int gameSpeed = 1;

    /// <summary>
    /// how many seconds are in a minute
    /// </summary>
    private readonly int ticsPerMinute = 60;
    /// <summary>
    /// how many minutes are in a hour
    /// </summary>
    private readonly int minutesPerHour = 60;
    /// <summary>
    /// how many hours are in a day
    /// default time means a day should take roughly 16 minutes
    /// </summary>
    private readonly int hoursPerDay = 60;

    protected GameClock() { GameSpeed = 3;}

    public int TotalDays { get => totalDays; private set => totalDays = value; }
    public int TotalHours { get => totalHours; private set => totalHours = value; }
    public int TotalMinutes { get => totalMinutes; set => totalMinutes = value; }
    public int GameSpeed
    {
        get => gameSpeed;
        set
        {
            if (value <=1)
            {
                gameSpeed = 1;
            }
            else if (value >= 4)
            {
                gameSpeed = 4;
            }
            else
            {
                gameSpeed = value;
            }
        }}

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        UpdateTime();
        GD.Print(GetTotalTime());
    }

    /// <summary>
    /// updates the clock 
    /// </summary>
    public void UpdateTime()
    {
        AddMinute();
        AddHour();
        AddDay();
    }


    public string GetTotalTime()
    {
        string time = $"Day: {totalDays}, time {totalHours}:{totalMinutes}";
        return time;
    }

    /// <summary>
    /// checks ticks passed sense last check if they equal the amount
    /// ticsPerSecond then add a second
    /// </summary>
    private void AddMinute()
    {
        int currentTime = (int)Time.GetTicksMsec();
        if (!(currentTime - lastUpdate > ticsPerMinute / gameSpeed))
        {
            return;
        }
        lastUpdate = currentTime;
        totalMinutes += 1;
        EmitSignal(nameof(NewHourEventHandler));
    }



    /// <summary>
    /// checks if enough minutes have passed to become an in game hour
    /// </summary>
    private void AddHour()
    {
        if (!(totalMinutes >= minutesPerHour / gameSpeed))
        {
            return;
        }
        totalMinutes = 0;
        totalHours += 1;
        EmitSignal("NewHour");
    }
    /// <summary>
    /// checks if enough hours have passed to become an in game day
    /// </summary>
    private void AddDay()
    {
        if (!(totalHours >= hoursPerDay / gameSpeed))
        {
            return;
        }
        totalHours = 0;
        totalDays += 1;
        EmitSignal("NewDay");

    }
}