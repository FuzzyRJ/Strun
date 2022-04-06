// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class YouWin : Popup
{
	private Timer returnToMenu;
	private TransitionScreen transitioner;
	private Label youWinText;
	private AudioStreamPlayer sound;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Show();
		youWinText = GetNode<Label>("Label");
		transitioner = GetNode<TransitionScreen>("TransitionScreen");
		returnToMenu = GetNode<Timer>("Timer");
		sound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		sound.Play();
		returnToMenu.Start(3);
	}//End Ready

private void _on_Timer_timeout()
{
	transitioner.Offset = new Vector2(0,0);
	youWinText.Text = "";
	transitioner.transition();
}//End OnTimerTimeout


private void _on_TransitionScreen_transitioned()
{
   GetTree().ChangeScene("res://assets/Menus/MainMenu.tscn");
}//End TransitionScreenTransitioned
}//End Class
