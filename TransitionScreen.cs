// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022s

using Godot;
using System;

public class TransitionScreen : CanvasLayer
{
[Signal] delegate void transitioned();
AnimationPlayer fadePlayer;

public override void _Ready()
{
fadePlayer = GetNode<AnimationPlayer>("AnimationPlayer");
}//End Ready

public void transition()
{
fadePlayer.Play("Fade_To_Black");
}//End Transition

private void OnAnimationPlayerFinished(String anim_name)
{
	if (anim_name == "Fade_To_Black")
	{
		EmitSignal("transitioned");
	}//End If
}//End OnAnimationPlayerFinished

public void FadeFromBlack()
{
	fadePlayer.Play("Fade_To_Normal");
}//End FadeFromBlack
}//End Class
