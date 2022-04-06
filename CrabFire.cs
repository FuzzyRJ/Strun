// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class CrabFire : AnimatedSprite
{
	public float speed = 120;
	KinematicBody2D _player;
	public Vector2 direction = new Vector2(-1,0); //Sets the default direction to left.
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var area = GetNode<Area2D>("FireHitbox");
		area.Connect("area_entered",this,"OnCollision");
		area.Connect("body_entered",this,"OnCollision");
		_player = GetNode<Player>("../Player");
		var direction = GlobalPosition.DirectionTo(_player.GlobalPosition);
	}//End Ready

  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {  
	Translate(direction*speed*delta);
  }//End Process

private void OnCollision(Area2D with)
{

	//If the The weapon collided with the area2D of an enemy damage it.
	if (with.GetParent() is Player player)
	{
	GD.Print("fire");
	player.Damage();
	}//End If
QueueFree();
}//End OnCollision
}//End Class
