// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Floor1 : Floor
{
Door doorOpener;
TileMap tiletest;
PackedScene impEnemyScene;
PackedScene orcEnemyScene;
PackedScene spawnExplosionScene;
bool inRoom;
int roomsCleared = 0;

CollisionShape2D playerCollisionBox;
Position2D enemySpawn1;
Position2D enemySpawn2;
RandomNumberGenerator rng;
TransitionScreen transitioner;
AudioStreamPlayer music;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		transitioner = GetNode<TransitionScreen>("TransitionScreen");
		var area = GetNode<Area2D>("PlayerDetection");
		area.Connect("area_entered",this,"RoomEntered");
		tiletest = GetNode<TileMap>("Navigation2D/TileMap2");
		impEnemyScene = GD.Load<PackedScene>("res://Characters/Enemies/Imp.tscn");
		spawnExplosionScene = GD.Load<PackedScene>("res://Characters/Enemies/SpawnExplosion.tscn");
		orcEnemyScene = GD.Load<PackedScene>("res://Characters/Enemies/Orc.tscn");

		_entrancePoint1 = GetNode<Position2D>("Entrance/RoomEntrance");
		_entrancePoint2 = GetNode<Position2D>("Entrance/RoomEntrance2");
		playerCollisionBox = GetNode<CollisionShape2D>("PlayerDetection/CollisionShape2D");
		enemySpawn1 = GetNode<Position2D>("EnemyPosition/Enemy");
		enemySpawn2 = GetNode<Position2D>("EnemyPosition/Enemy2");
		music = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		rng = new RandomNumberGenerator();
		rng.Randomize();
		transitioner.Layer = 1;
		transitioner.FadeFromBlack();
		music.Play();
		
	}//End Ready

private void RoomEntered(Area2D with)
{
playerCollisionBox.SetDeferred("disabled",true); //Sets the collisionbox to disabled during idle time.
SpawnEnemies();
MakeWall();
}//End RoomEntered

private void MakeWall()
{
//Creates a wall at the Position2D's location.
if (roomsCleared == 0)
{
//Places Horizontal walls for the first room.
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint1.Position),1); 
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint2.Position),2);
}//End If
else
{
//Places vertical walls for the second room.
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint1.Position),3, flipX: true); 
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint2.Position),4);
}//End Else
inRoom = true;
}//End MakeWall

private void OpenDoors()
{
if (roomsCleared == 0)
{
doorOpener = GetNode<Door>("Doors/Door");
}//End If
else
{
doorOpener = GetNode<Door>("Doors/Door2");
}//End Else
doorOpener.Open();
}//End OpenDoors

private void SpawnEnemies()
{
Node2D enemyPosition;
enemyPosition = GetNode<Node2D>("EnemyPosition");

for (int i = 0; i < enemyPosition.GetChildCount(); i++)
{
Position2D childPosition;
var child = GetNode<Node2D>("EnemyPosition").GetChild(i); //Grabs a child of the EnemyPosition Node.
childPosition = (Position2D)child;

SpawnExplosion explosion = (SpawnExplosion)spawnExplosionScene.Instance();
explosion.Position = childPosition.Position; 
GetNode("../Floor1").CallDeferred("add_child",explosion); //Creates a spawn explosion effect as the child of the Floor1 node, CallDeferred calls method during idle time.

var numberGenerated =rng.RandiRange(1,2);
if (numberGenerated == 1)
{
Enemy imp = (Enemy)impEnemyScene.Instance();
imp.Position = childPosition.Position;
imp.AddToGroup("Enemy");
GetNode("../Floor1").CallDeferred("add_child",imp); //Creates an imp enemy as the child of the Floor1 node while queries are not being flushed.
}//End If
else
{
Enemy orc = (Enemy)orcEnemyScene.Instance();
orc.Position = childPosition.Position;
orc.AddToGroup("Enemy");
GetNode("../Floor1").CallDeferred("add_child",orc); //Creates an orc enemy as the child of the Floor1 node while queries are not being flushed.
}//End Else
}//End For
}//End SpawnEnemies

private void CheckEnemiesDead()
{
	if (GetTree().GetNodesInGroup("Enemy").Count == 0 && inRoom == true)
	{
		inRoom = false;
		OpenDoors();
		DestroyWalls();
		if (roomsCleared < 1) //Makes sure that the RoomCleared function only runs one time.
		{
		RoomCleared();
		}//End If
	}//End If
}//End CheckEnemiesDead


public void RoomCleared()
{
roomsCleared += 1;
_entrancePoint1.Position = new Vector2(179,-156);
_entrancePoint2.Position = new Vector2(179, -140);
playerCollisionBox.Position = new Vector2(197, -145);
playerCollisionBox.Rotation = 90;
enemySpawn1.Position = new Vector2(280, -168);
enemySpawn2.Position = new Vector2(216, -217);
playerCollisionBox.SetDeferred("disabled",false); //sets the collision box to enabled during idle time.
}//End RoomCleared

public void DestroyWalls()
{
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint1.Position),0); 
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint2.Position),0);
}//End DestroyWalls

// Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(float delta)
{
	CheckEnemiesDead();
}//End Process

private void _on_Player_dead()
{
	GetTree().ChangeScene("res://assets/Menus/GameOver.tscn");
}//End OnPlayerDead

private void _on_StaticBody2D_NextStage()
{
	transitioner.transition();
}//End NextStage

private void _on_TransitionScreen_transitioned()
{
	GetTree().ChangeScene("res://Rooms/Floor2.tscn");
}//End Transition

}//End class
