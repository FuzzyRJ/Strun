// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Imp : Enemy
{


private AnimationPlayer _enemyAnimationPlayer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	_enemyAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
	_player = GetNode<Player>("../Player");
	_enemySprite = GetNode<AnimatedSprite>("AnimatedSprite");
	_timer = GetNode<Timer>("Timer");

	area = GetNode<Area2D>("EnemyHitbox");
	area.Connect("area_entered",this,"OnCollision");
	area.Connect("area_exited", this, "CollisionLeft");
	_timer.Connect("timeout",this,"OnTimerTimeout");
	} //End Ready

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
	  if (_enemyAnimationPlayer.CurrentAnimation != "Hurt")
	  {
	  _enemyAnimationPlayer.Play("Chase");
	  }//End If
  } //End Process

private void OnImpDamaged()
{
_enemyAnimationPlayer.Stop();
_enemyAnimationPlayer.Play("Hurt");

//Applies knockback to the enemy by taking the players position from the enemy position and multiplying by the force;
Vector2 knockbackVector;
knockbackVector = (GlobalPosition - _player.GlobalPosition) * 50;
MoveAndSlide(knockbackVector);
}//End OnImpDamaged
}//End Class
