namespace Atomation.GameMap;

using Godot;
using GameSettings;
using Ui;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// defines a collection of values used to configure the world. as well as methods used 
/// to create ui to modify/change these values
/// </summary>
public partial class WorldSettingsUi : UserInterface
{
    private static readonly string CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    private VBoxContainer configsContainer;

    private WorldConfigs settings;

    public WorldSettingsUi() : base()
    {
        LayoutMode = 1;

        settings = new();
        Name = "World Settings Ui";

        isOpen = true;

        CreateUIElements();
    }

    /// <summary>
    /// used to format the key which will be put into the setting dictionary
    /// in "worldSetting" object
    /// </summary>
    private string FormatSettingKey(string section, string element)
    {
        return $"{section}_{element}";
    }

    /// <summary>
    /// generates a random string
    /// </summary>
    private string GenerateRandomText(int length)
    {
        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();

        StringBuilder result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            // Pick a random character from the CHARACTERS string
            int randomIndex = rng.RandiRange(0, CHARACTERS.Length - 1);
            result.Append(CHARACTERS[randomIndex]);
        }

        return result.ToString();
    }

    protected override void CreateUIElements()
    {
        background = new PanelContainer()
        {
            CustomMinimumSize = new Vector2(250, 525),
        };
        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(uiPadding);
        background.AddChild(marginContainer);

        List<PanelContainer> sections = new List<PanelContainer>{
            CreateGeneralSettingsSection(),
            CreateSection("Elevation", new SettingsElement[]{
                new SettingsSlider{
                    Name = "WaterLevel",
                    MinValue = 0,
                    MaxValue = 1,
                    Step = 0.1f,
                },
                new SettingsSlider{
                    Name = "MountainSize",
                    MinValue = 0,
                    MaxValue = 1,
                    Step = 0.1f,
                }

            }),

            CreateSection("Temperature", new SettingsElement[]{
                new SettingsSlider{
                    Name = "Average",
                    MinValue = 0, //maybe make negative?
                    MaxValue = 1,
                    Step = 0.1f,
                },
                new SettingsToggle{
                    Name = "TrueCenter",
                    Toggled = false,
                }

            }),

            CreateSection("Moisture", new SettingsElement[]{
                new SettingsSlider{
                    Name = "Average",
                    MinValue = 0, //maybe make negative?
                    MaxValue = 1,
                    Step = 0.1f,
                },
            }),

            CreateSection("Vegetation", new SettingsElement[]{
                new SettingsSlider{
                    Name = "Density",
                    MinValue = 0, //maybe make negative?
                    MaxValue = 1,
                    Step = 0.1f,
                },
            })
        };

        configsContainer = new VBoxContainer();

        foreach (var section in sections)
        {
            configsContainer.AddChild(section);
        }

        configsContainer.AddChild(CreateButton("Generate", () => {Map.Instance.FinalizeGeneration(settings); ToggleUI();}));

        marginContainer.AddChild(configsContainer);

        AddChild(background);
        background.LayoutMode = 1;
    }

    /// <summary>
    /// creates a section for the settings that don't really 
    /// belong to a specific category
    /// </summary>
    private PanelContainer CreateGeneralSettingsSection()
    {
        string sectionName = "General";
        PanelContainer panelContainer = new PanelContainer();
        VBoxContainer vbox = new VBoxContainer();

        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(uiPadding);
        panelContainer.AddChild(marginContainer);

        vbox.AddChild(new Label
        {
            Text = sectionName,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        CreateSeedRandomizer(vbox);
        vbox.AddChild(CreateToggle(sectionName, new SettingsToggle
        {
            Name = "UpdateOnEdit",
            Toggled = false
        }));

        var hBox = CreateHBox("Spawn Size", new Vector2(90, 0));
        hBox.AddChild(CreateLineEdit(sectionName, new SettingsLineEdit
        {
            Name = "SpawnSize",
            Value = "1"
        }));

        vbox.AddChild(hBox);

        marginContainer.AddChild(vbox);

        return panelContainer;
    }

    private PanelContainer CreateSection(string sectionName, SettingsElement[] settings)
    {
        PanelContainer panelContainer = new PanelContainer();
        VBoxContainer vbox = new VBoxContainer();

        var marginContainer = new MarginContainer();
        marginContainer.AddMargin(uiPadding);
        panelContainer.AddChild(marginContainer);

        vbox.AddChild(new Label
        {
            Text = sectionName,
            HorizontalAlignment = HorizontalAlignment.Center
        });

        marginContainer.AddChild(vbox);

        foreach (var element in settings)
        {
            if (element is SettingsSlider sliderElement)
            {
                vbox.AddChild(CreateSlider(sectionName, sliderElement));
            }
            if (element is SettingsLineEdit lineEditElement)
            {
                vbox.AddChild(CreateLineEdit(sectionName, lineEditElement));
            }
            if (element is SettingsToggle checkBoxElement)
            {
                vbox.AddChild(CreateToggle(sectionName, checkBoxElement));
            }
        }

        return panelContainer;
    }

    /// <summary>
    /// creates a slider ui element, used to configure a certain  setting
    /// </summary>
    private HBoxContainer CreateSlider(string sectionName, SettingsSlider element)
    {
        HBoxContainer hBox = CreateHBox(element.Name, new Vector2(90, 0));
        HSlider slider = new HSlider
        {
            MinValue = element.MinValue,
            MaxValue = element.MaxValue,
            Step = element.Step,
            Value = element.Value,
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
            SizeFlagsVertical = SizeFlags.Fill
        };

        settings.Values[FormatSettingKey(sectionName, element.Name)] = element.Value;

        slider.ValueChanged += val =>
        {
            settings.Values[FormatSettingKey(sectionName, element.Name)] = (float)val;

            if ((bool)settings.Values["General_UpdateOnEdit"])
                Map.Instance.GenerateMap(settings);
        };

        hBox.AddChild(slider);

        return hBox;
    }

    /// <summary>
    /// creates a ui element which allows the user to input text
    /// </summary>
    private LineEdit CreateLineEdit(string sectionName, SettingsLineEdit element)
    {
        LineEdit lineEdit = new LineEdit()
        {
            Text = element.Value,
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
        };

        settings.Values[FormatSettingKey(sectionName, element.Name)] = element.Value;

        lineEdit.TextChanged += val =>
        {
            settings.Values[FormatSettingKey(sectionName, element.Name)] = val;

            if ((bool)settings.Values["General_UpdateOnEdit"])
                Map.Instance.GenerateMap(settings);
        };


        return lineEdit;
    }

    /// <summary>
    /// creates a ui element which allows the user toggle on or off an element
    /// </summary>
    private HBoxContainer CreateToggle(string sectionName, SettingsToggle element)
    {
        var hBox = CreateHBox(element.Name, new Vector2(90, 0));
        var checkbox = new CheckBox
        {
            ButtonPressed = element.Toggled
        };

        settings.Values[FormatSettingKey(sectionName, element.Name)] = element.Toggled;
        checkbox.Toggled += val =>
        {
            settings.Values[FormatSettingKey(sectionName, element.Name)] = val;

            if ((bool)settings.Values["General_UpdateOnEdit"])
                Map.Instance.GenerateMap(settings);

        };

        hBox.AddChild(checkbox);

        return hBox;
    }

    /// <summary>
    /// creates a line edit ui element used to type in seeds, and a 
    /// button used to randomize them. then assigns to given vbox
    /// </summary>
    private void CreateSeedRandomizer(VBoxContainer vBox)
    {
        LineEdit seedEdit = CreateLineEdit("General", new SettingsLineEdit
        {
            Name = "Seed",
            Value = "TestText",
        });

        var RandomizeSeed = CreateButton("Randomize Seed", () =>
        {
            string temp = GenerateRandomText(10);
            seedEdit.Text = temp;
        });

        var hBox = CreateHBox("Seed", new Vector2(90, 0));
        hBox.AddChild(seedEdit);
        vBox.AddChild(hBox);
        vBox.AddChild(RandomizeSeed);
    }
}