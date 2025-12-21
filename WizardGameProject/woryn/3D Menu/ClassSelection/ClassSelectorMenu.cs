using Godot;
using System;
using System.Collections.Generic;

public partial class ClassSelectorMenu : Control
{
	[Export] Label class1Description;
	[Export] Label class2Description;
	[Export] Label class3Description;
	[Export] Label class4Description;

	[Export] Button class1Icon;
	[Export] Button class2Icon;
	[Export] Button class3Icon;
	[Export] Button class4Icon;

    [Export] Button class1;
    [Export] Button class2;
    [Export] Button class3;
    [Export] Button class4;

    [Export] HBoxContainer container;

    List<Button> classes = new List<Button>();

    Button selectedClass;
    bool selected = false;
	public override void _Ready()
	{
        classes.Add(class1);
        classes.Add(class2);
        classes.Add(class3);
        classes.Add(class4);


        class1Description.Visible = false;
        class2Description.Visible = false;
        class3Description.Visible = false;
        class4Description.Visible = false;
	}

	public override void _Process(double delta)
	{
        if (selected)
        {
            ClassSelected(selectedClass);
            GD.Print($"Class selected --> {selectedClass.Name}");
            selected = false;
        }
	}
    private void ClassSelected(Button selectedClass)
    {
        foreach(Button _class in classes)
        {
            if (selectedClass != _class)
            {
                _class.QueueFree();
                GD.Print($"Class deleted: {_class.Name}");
            }
        }
        selectedClass.Disabled = true;

        
        //container.AddChild(selectedClass);
    }

    //Class selection
    private void OnClass1Pressed()
    {
        selectedClass = class1;
        selected = true;
    }
    private void OnClass2Pressed()
    {
        selectedClass = class2;
        selected = true;
    }
    private void OnClass3Pressed()
    {
        selectedClass = class3;
        selected = true;
    }
    private void OnClass4Pressed()
    {
        selectedClass = class4;
        selected = true;
    }

    //Show description
    private void OnClass1IconToggled(bool toggled)
	{
		if (toggled)
		{
			class1Description.Visible = true;
		}
		else
		{
			class1Description.Visible = false;
		}

	}
    private void OnClass2IconToggled(bool toggled)
    {
        if (toggled)
        {
            class2Description.Visible = true;
        }
        else
        {
            class2Description.Visible = false;
        }
    }
    private void OnClass3IconToggled(bool toggled)
    {
        if (toggled)
        {
            class3Description.Visible = true;
        }
        else
        {
            class3Description.Visible = false;
        }
    }
    private void OnClass4IconToggled(bool toggled)
    {
        if (toggled)
        {
            class4Description.Visible = true;
        }
        else
        {
            class4Description.Visible = false;
        }
    }
}
