// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class SpawnExplosion : AnimatedSprite
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Playing = true;
	}//End Ready

public void _on_SpawnExplosion_animation_finished()
{
	QueueFree();
}//End OnAnimationFinished
}//End Class
