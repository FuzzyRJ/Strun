// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;


public class MenuButton : TextureButton
{

[Export] string text = "Text button";
[Export] int arrowMarginFromCenter = 100;
Sprite arrow;
RichTextLabel TextLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		arrow = GetNode<Sprite>("LeftArrow");
		TextLabel = GetNode<RichTextLabel>("RichTextLabel");
		SetupText();
		FocusMode = FocusModeEnum.Click;
	}//End Ready

private void SetupText()
{
	TextLabel.BbcodeText = "[center] [/center]" + text;
}//End SetupText

private void ShowArrows()
{

var centerX = RectGlobalPosition.x + (RectSize.x / 2.0);
var centerY = RectGlobalPosition.y + (RectSize.y / 3.0);
Vector2 CenterPosition;
CenterPosition.x = (float)centerX - arrowMarginFromCenter;
CenterPosition.y = (float)centerY;
arrow.Visible = true;
arrow.GlobalPosition = CenterPosition;
}//End ShowArrows

private void HideArrows()
{
	arrow.Visible = false;
}//End HideArrows

private void _on_TextureButton_focus_entered()
{
	ShowArrows();
}//End OnTextureButtonFocusEntered

private void _on_TextureButton_focus_exited()
{
	HideArrows();
}//End OnTextureButtonFocusExited

private void _on_TextureButton_mouse_entered()
{
	GrabFocus();
}//End OnTextureButtonMouseEntered
}//End Class
