// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class MainMenu : Node2D
{

private TransitionScreen transitioner;
AudioStreamPlayer music;
public override void _Ready()
{
	var container = GetNode<MenuButton>("VBoxContainer/VBoxContainer/Start");
	transitioner = GetNode<TransitionScreen>("TransitionScreen");
	container.GrabFocus();
	transitioner.Layer = -1;
	music = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	music.Play();
}//End Ready

private void _on_Start_pressed()
{	
	transitioner.Layer = 1;
	transitioner.transition();
}//End OnStartPressed

private void _on_How_To_Play_pressed()
{
	GetTree().ChangeScene("res://assets/Menus/HowToPlay.tscn");
}//End OnHowToPlayPressed


private void _on_Exit_pressed()
{
	GetTree().Quit();
}//End OnExitPressed

private void _on_TransitionScreen_transitioned()
{
	GetTree().ChangeScene("res://Rooms/Floor1.tscn");
}//End OnTransitionScreenTransitioned
}//End Class
