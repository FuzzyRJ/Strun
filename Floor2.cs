// Program: Strun
// Author: Sean Moore
//Last Updated: 4/3/2022

using Godot;
using System;

public class Floor2 : Floor
{

Door doorOpener;
TileMap tiletest;
PackedScene impEnemyScene;
PackedScene orcEnemyScene;
PackedScene crabBossScene;
PackedScene spawnExplosionScene;
PackedScene bossSpawnEffectScene;
bool inRoom;
int roomsCleared = 0;
CollisionShape2D playerCollisionBox;
Position2D enemySpawn1;
Position2D enemySpawn2;
Position2D enemySpawn3;
Position2D bossPosition;
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
		crabBossScene = GD.Load<PackedScene>("res://Characters/Enemies/Boss.tscn");
		bossSpawnEffectScene = GD.Load<PackedScene>("res://Characters/Enemies/BossSpawnEffect.tscn");

		_entrancePoint1 = GetNode<Position2D>("Entrance/RoomEntrance");
		_entrancePoint2 = GetNode<Position2D>("Entrance/RoomEntrance2");
		playerCollisionBox = GetNode<CollisionShape2D>("PlayerDetection/CollisionShape2D");
		enemySpawn1 = GetNode<Position2D>("EnemyPosition/Enemy");
		enemySpawn2 = GetNode<Position2D>("EnemyPosition/Enemy2");
		enemySpawn3 = GetNode<Position2D>("EnemyPosition/Enemy3");
		bossPosition = GetNode<Position2D>("BossPosition");
		music = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		rng = new RandomNumberGenerator();
		rng.Randomize();
		transitioner.Layer = 1;
		transitioner.FadeFromBlack();
		music.Play();
	} //End Ready

private void RoomEntered(Area2D with)
{
playerCollisionBox.SetDeferred("disabled",true); //Sets the collisionbox to disabled during idle time.
SpawnEnemies();
MakeWall();
}//End RoomEntered

private void MakeWall()
{
//Creates a wall at the Position2D's location.
if (roomsCleared == 1)
{
//Places Horizontal walls for the first room.
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint1.Position),1, flipY: true); 
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint2.Position),2, flipY: true);
}
else if (roomsCleared == 2)
{
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint1.Position),3, flipX: true, flipY: true); 
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint2.Position),4, flipY: true);
}
else
{
//Places vertical walls for the third room.
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint1.Position),1); 
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint2.Position),2);
}
inRoom = true;
} //End MakeWall

private void OpenDoors()
{
if (roomsCleared == 0)
{
doorOpener = GetNode<Door>("Doors/Door");
}
else if (roomsCleared == 1)
{
doorOpener = GetNode<Door>("Doors/Door2");
}
else if (roomsCleared == 2)
{
doorOpener = GetNode<Door>("Doors/Door3");
}
else
{
doorOpener = doorOpener = GetNode<Door>("Doors/Door4");
}
doorOpener.Open();
} //End OpenDoors

private void SpawnEnemies()
{
if (roomsCleared < 3)
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
GetNode("../Floor2").CallDeferred("add_child",explosion); //Creates a spawn explosion effect as the child of the Floor2 node, CallDeferred calls method during idle time.

var numberGenerated =rng.RandiRange(1,2);
if (numberGenerated == 1)
{
Enemy imp = (Enemy)impEnemyScene.Instance();
imp.Position = childPosition.Position;
imp.AddToGroup("Enemy");
GetNode("../Floor2").CallDeferred("add_child",imp); //Creates an imp enemy as the child of the Floor2 node while queries are not being flushed.
}//End If
else
{
Enemy orc = (Enemy)orcEnemyScene.Instance();
orc.Position = childPosition.Position;
orc.AddToGroup("Enemy");
GetNode("../Floor2").CallDeferred("add_child",orc); //Creates an orc enemy as the child of the Floor2 node while queries are not being flushed.
}//End Else
}//End For
}//End If
else
{

BossSpawnEffect effect = (BossSpawnEffect)bossSpawnEffectScene.Instance();
effect.Position = bossPosition.Position;
GetNode("../Floor2").CallDeferred("add_child",effect);

Enemy crabBoss = (Enemy)crabBossScene.Instance();
crabBoss.Position = bossPosition.Position;
crabBoss.AddToGroup("Enemy");
GetNode("../Floor2").CallDeferred("add_child",crabBoss);
}//End Else
} //End Spawn Enemies

private void CheckEnemiesDead()
{
	if (GetTree().GetNodesInGroup("Enemy").Count == 0 && inRoom == true)
	{
		inRoom = false;
		OpenDoors();
		DestroyWalls();
		if (roomsCleared <= 3) //Makes sure that the RoomCleared function only runs three time.
		{
		RoomCleared();
		}//End If
	}//End If
}//End CheckEnemiesDead

public void RoomCleared()
{
roomsCleared += 1;
if (roomsCleared == 1)
{
_entrancePoint1.Position = new Vector2(55, -415);
_entrancePoint2.Position = new Vector2(71, -415);
playerCollisionBox.Position = new Vector2(64, -427);
enemySpawn1.Position = new Vector2(-6, -442);
enemySpawn2.Position = new Vector2(64, -502);
enemySpawn3.Position = new Vector2(136, -537);
}//End If
else if (roomsCleared == 2)
{
_entrancePoint1.Position = new Vector2(-188, -825);
_entrancePoint2.Position = new Vector2(-188, -809);
playerCollisionBox.Position = new Vector2(-215, -822);
playerCollisionBox.Rotation = 90;
enemySpawn1.Position = new Vector2(-280, -936);
enemySpawn2.Position = new Vector2(-216, -936);
enemySpawn3.Position = new Vector2(-280, -777);
}//End ElseIf
else 
{
_entrancePoint1.Position = new Vector2(-265, -1368);
_entrancePoint2.Position = new Vector2(-249, -1368);
playerCollisionBox.Position = new Vector2(-258, -1374);
playerCollisionBox.Rotation = 0;
}//End Else
playerCollisionBox.SetDeferred("disabled",false); //sets the collision box to enabled during idle time.
} //End RoomCleared

public void DestroyWalls()
{
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint1.Position),0); 
tiletest.SetCellv(tiletest.WorldToMap(_entrancePoint2.Position),0);
} //End DestroyWalls

// Called every frame. 'delta' is the elapsed time since the previous frame.
public override void _Process(float delta)
{
	CheckEnemiesDead();
} //End Process

private void _on_Player_dead()
{
	GetTree().ChangeScene("res://assets/Menus/GameOver.tscn");
} //End OnPlayerDead

private void _on_Area2D_area_entered(object area)
{
	GetTree().ChangeScene("res://assets/Menus/YouWin.tscn");
}//End Area2D Entered

}//End Class
