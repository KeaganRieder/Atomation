namespace Atomation.Ui;

using Godot;

public partial class RebindControls : ScrollContainer
{
    // private  bindingsContainer;

    public RebindControls()
    {
        Name = "Controls";
        Panel panel = new Panel() { Name = "test", CustomMinimumSize = new Vector2(20, 20) };

        AddChild(panel);
        panel = new Panel() { Name = "test2", CustomMinimumSize = new Vector2(20, 20) };
        AddChild(panel);
    }

    private void DefaultBindings()
    {
        //todo
    }
}