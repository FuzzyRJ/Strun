// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Weapon : Node2D
{

	public override void _Ready()
	{
		var area = GetNode<Area2D>("Node2D/Sprite/WeaponHitbox");
		area.Connect("area_entered",this,"OnCollision");
		area.Connect("body_entered",this,"OnCollision");
	} //End Ready

private void OnCollision(Area2D with)
{

	//If the The weapon collided with the area2D of an enemy damage it.
	if (with.GetParent() is Enemy enemy)
	{
	enemy.Damage();
	}//End If

} //End OnCollision
} //End Class
