// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Door : StaticBody2D
{
public AnimationPlayer _doorAnimation;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_doorAnimation = GetNode<AnimationPlayer>("AnimationPlayer");
	}//End Ready

public void Open()
{
	_doorAnimation.Play("open");
}//End Open
}//End Class
