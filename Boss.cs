// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Boss : Enemy
{
	Timer _fireTimer;
	PackedScene crabFireScene;
	AnimationPlayer _bossAnimationPlayer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	_player = GetNode<Player>("../Player");
	_enemySprite = GetNode<AnimatedSprite>("AnimatedSprite");

	_timer = GetNode<Timer>("Timer");
	_timer.Connect("timeout",this,"OnTimerTimeout");

	_fireTimer = GetNode<Timer>("FireTimer");
	_bossAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	

	area = GetNode<Area2D>("EnemyHitbox");
	area.Connect("area_entered",this,"OnCollision");
	area.Connect("area_exited", this, "CollisionLeft");
	_fireTimer.Connect("timeout",this,"AttackTimerTimeout");

	crabFireScene = GD.Load<PackedScene>("res://Characters/Enemies/CrabFire.tscn");
	_fireTimer.Start(5F);	
	}//End Ready


private void Attack()
{
var PI = Math.PI;
CrabFire fire = (CrabFire)crabFireScene.Instance();
GetParent().CallDeferred("add_child",fire);
fire.GlobalPosition = GlobalPosition; //Sets the global position of the attack to the enemy
var dir = (_player.GlobalPosition - GlobalPosition).Normalized(); //Gets the normalized distance between the player and enemy
fire.GlobalRotation = dir.Angle() + (float)PI /2.0F;
fire.direction = dir; //Sends the attack towards the point gotten
}//End Attack

private void AttackTimerTimeout()
{
	Attack();

}//End TimerTimeout

private void OnBossDamaged()
{
	_bossAnimationPlayer.Play("Hurt");
	_enemySprite.Stop();
}

 // Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(float delta)
  {
	 var direction = (_player.Position - Position).Normalized();
	  if (direction.x < 0)
	  {
		_enemySprite.FlipH = true;
	  } //End If
	  else
	  {
		_enemySprite.FlipH = false;
	  } //End Else
	  MoveAndSlide(direction * enemySpeed); 

	if (_bossAnimationPlayer.CurrentAnimation != "Hurt")
	{
		_enemySprite.Play("Chase");
	}

  }//End Process
}//End Class

