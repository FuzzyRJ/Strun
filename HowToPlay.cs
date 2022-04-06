// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class HowToPlay : Popup
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Show();
	}//End Ready


private void _on_Close_pressed()
{
	GetTree().ChangeScene("res://assets/Menus/MainMenu.tscn"); //Takes you back to the main menu when close is clicked
}//End OnClosePressed
}//End Class
