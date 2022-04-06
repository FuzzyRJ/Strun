// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class BossSpawnEffect : AnimatedSprite
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Playing = true;
	}//End Ready


private void _on_BossSpawnEffect_animation_finished()
{
	QueueFree();
}//End OnAnimationFinished
}//End Class
