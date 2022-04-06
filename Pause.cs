// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Pause : CanvasLayer
{
TextureRect background;
Button continueButton;
Label pausedLabel;
Button menuReturn;

public override void _Ready()
{
background = GetNode<TextureRect>("Background");
continueButton = GetNode<Button>("Continue");
pausedLabel = GetNode<Label>("PausedText");
menuReturn = GetNode<Button>("BackToMenu");
Visibile(0);
}//End Ready

public void Visibile(int isVisible)
{
if (isVisible == 1)
{
background.Visible = true;
continueButton.Visible = true;
pausedLabel.Visible = true;
menuReturn.Visible = true;
}//End If
else
{
background.Visible = false;
continueButton.Visible = false;
pausedLabel.Visible = false;
menuReturn.Visible = false;
}//End Else
}//End Visible

	public override void _UnhandledKeyInput(InputEventKey @event)
	{
		if (@event.IsActionPressed("ui_cancel") && GetTree().CurrentScene.Name != "MainMenu" && GetTree().CurrentScene.Name != "HowToPlay")
		if (GetTree().Paused == false)
		{
			GetTree().Paused = true;
			Visibile(1);
			
		}//End If
		else
		{
			GetTree().Paused = false;
			Visibile(0);
		}//End Else
	}//End UnhandledKeyInput
	
private void _on_Button_pressed()
{
	GetTree().Paused = false;
	Visibile(0);
}//End OnButtonPressed

private void _on_BackToMenu_pressed()
{
	Visibile(0);
	GetTree().ChangeScene("res://assets/Menus/MainMenu.tscn");
	GetTree().Paused = false;
}//End BackToMenuPressed

}//End Class
