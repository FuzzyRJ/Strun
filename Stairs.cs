// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Stairs : StaticBody2D
{
	[Signal] delegate void NextStage(); 

private void _on_Area2D_area_entered(object area)
{
EmitSignal("NextStage");	
}//End OnAreaEntered
}//End Class


