// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Orc : Enemy
{
Timer _attackTimer;
PackedScene orcLightningScene;
AnimationPlayer _orcAnimationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	_player = GetNode<Player>("../Player");
	_enemySprite = GetNode<AnimatedSprite>("AnimatedSprite");
	_orcAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	
	area = GetNode<Area2D>("EnemyHitbox");
	area.Connect("area_entered",this,"OnCollision");
	area.Connect("area_exited", this, "CollisionLeft");

	_timer = GetNode<Timer>("Timer");
	_timer.Connect("timeout",this,"OnTimerTimeout");

	_attackTimer = GetNode<Timer>("AttackTimer");
	_attackTimer.Connect("timeout",this,"PlayerTimerTimeout");

	orcLightningScene = GD.Load<PackedScene>("res://Characters/Enemies/OrcLightning.tscn");
	_attackTimer.Start(1.5F);	
	} //End Ready

private void Attack()
{
var PI = Math.PI;
OrcLightning lightning = (OrcLightning)orcLightningScene.Instance();
GetParent().CallDeferred("add_child",lightning);
lightning.GlobalPosition = GlobalPosition; //Sets the global position of the attack to the enemy
var dir = (_player.GlobalPosition - GlobalPosition).Normalized(); //Gets the normalized distance between the player and enemy
lightning.GlobalRotation = dir.Angle() + (float)PI /2.0F;
lightning.direction = dir; //Sends the attack towards the point gotten
}//End Attack

private void PlayerTimerTimeout()
{
Attack();
}//End PlayerTimerTimeout


private void OnOrcDamaged()
{
	_orcAnimationPlayer.Stop();
	_orcAnimationPlayer.Play("Hurt");
}//End OnOrcDamaged

public override void _Process(float delta)
{
	if (_orcAnimationPlayer.CurrentAnimation != "Hurt")
	{
		_orcAnimationPlayer.Play("Idle");
	}//End If
}//End Process
}//End Class
