// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;
using System.Collections.Generic;

public class Enemy : KinematicBody2D
{
	[Signal] delegate void Damaged();
	[Export] public int hp = 0;
	[Export] public int enemySpeed = 0;
	public KinematicBody2D _player;
	public AnimatedSprite _enemySprite;
	public Timer _timer;
	public Area2D area;
	public PackedScene spawnExplosionScene;

		public override void _Ready()
	{
	_player = GetNode<Player>("../Player");
	_enemySprite = GetNode<AnimatedSprite>("AnimatedSprite");

	var area = GetNode<Area2D>("EnemyHitbox");
	area.Connect("area_entered",this,"OnCollision");
	area.Connect("area_exited", this, "CollisionLeft");

	_timer = GetNode<Timer>("Timer");
	_timer.Connect("timeout",this,"OnTimerTimeout");
	} //End Ready


public void Damage()
	{
	hp -= 1;
	EmitSignal("Damaged");
	if (hp <= 0)
	{
	QueueFree(); //Destroys the enemy when hp hits 0 or less.
	} //End If
	} //End Damage

private void OnCollision(Area2D with) //with is the Area2D of the collision box that was collided with.
{
	if (with.GetParent() is Player player)
	{
	player.Damage();
	_timer.Start(.7F); //Starts a timer that goes off every seventh of a second while the collision boxes are colliding.
	} //End If
}//End OnCollision

private void CollisionLeft(Area2D with) 
{
	if (with.GetParent() is Player player)
	{
		_timer.Stop(); //Stops the timer when the collision boxes seperate.
	} //End If
} //End CollisionLeft

private void OnTimerTimeout() //Trigers every time the timer goes off.
{
var player = GetNode<Player>("../Player"); //Looks for the player node from the root node.
if (player != null)
{
player.Damage();
} //End If
} //End OnTimerTimeout

} //End Class
