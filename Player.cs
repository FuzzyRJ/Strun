// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Player : KinematicBody2D
{
	[Signal] delegate void dead();

	private AnimatedSprite _playerAnimatedSprite;
	private Node2D _weapon;
	private AnimationPlayer _weaponAnimationPlayer;
	private ProgressBar _healthbar;
	private AnimationPlayer _playerAnimationPlayer;

[Export] private int speed = 0; //How fast the character moves, export allows you to edit the value in the property editor.
private int hp = 4; //How much hp the character has
RandomNumberGenerator rng;
private Vector2 velocity = new Vector2(0,0);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_playerAnimatedSprite = GetNode<AnimatedSprite>("PlayerAnimation");
		_weapon = GetNode<Node2D>("Weapon");
		_weaponAnimationPlayer = GetNode<AnimationPlayer>("Weapon/WeaponAnimationPlayer");
		_playerAnimationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_healthbar = GetNode<ProgressBar>("Camera2D/ProgressBar");
		rng = new RandomNumberGenerator();
		rng.Randomize(); //Generates a new series of random values that will be pulled from
	} //End Ready

	public void Damage()
	{
	hp -= 1;
	_healthbar.Value -= 1;
	_playerAnimationPlayer.Play("Hurt");
	if (hp <= 0)
	{
	EmitSignal("dead");
	} //End If
	} //End Damage

private void Movement()
{
	//Determines if player should move left, right, or neither depending on which keys are pressed.
velocity.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
velocity.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
velocity = velocity.Normalized() * speed;
velocity = MoveAndSlide(velocity);
} //End Movement

private void PlayerAnimations()
{
	//Flips the sprite based on the mouse position.
	if (GlobalPosition.x - GetGlobalMousePosition().x < 0)
	{ 
	 _playerAnimatedSprite.FlipH = false;
	} //End If
	 else if (GlobalPosition.x - GetGlobalMousePosition().x > 0)
	 { 
	_playerAnimatedSprite.FlipH = true;
	 } //End ElseIf

//Plays the Run or Idle animation depending on wether the character is moving, plays the attack animation if the button is pressed.
	if (velocity.x != 0 || velocity.y != 0 && _playerAnimationPlayer.CurrentAnimation != "Hurt")
	{
_playerAnimatedSprite.Play("Run");
	} //End If
else if (velocity.x == 0 && velocity.y == 0 && _playerAnimationPlayer.CurrentAnimation != "Hurt") 
{
_playerAnimationPlayer.Play("Idle");
} //End ElseIf
 if (Input.IsActionPressed("Attack") && _weaponAnimationPlayer.CurrentAnimation != "attack" && _weaponAnimationPlayer.CurrentAnimation != "alt_attack")
 {
	 var numGenerated = rng.RandiRange(1, 100);
	 if (numGenerated == 100)
	 {
	_weaponAnimationPlayer.Play("alt_attack");
	 }//End If
	else
	{
	_weaponAnimationPlayer.Play("attack");	
	}//End Else
	 }//End If
} //End PlayerAnimations

private void WeaponRotation()
{
	var newWeaponScale = _weapon.Scale;
_weapon.Rotation = GetAngleTo(GetGlobalMousePosition());
//Flips the weapon from side to side depending on mouse position.
if (_weapon.Scale.y == 1 && GlobalPosition.x - GetGlobalMousePosition().x > 0)
{
	newWeaponScale.y = -1;
	_weapon.Scale = newWeaponScale;
} //End If
else if (_weapon.Scale.y == -1 && GlobalPosition.x - GetGlobalMousePosition().x < 0)
{
	newWeaponScale.y = 1;
	_weapon.Scale = newWeaponScale;
} //End ElseIf
} //End WeaponRotation

// Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
Movement();
PlayerAnimations();
WeaponRotation();
} //End Process
} //End Class
