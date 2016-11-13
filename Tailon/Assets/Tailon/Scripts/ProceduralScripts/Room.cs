using UnityEngine;
using System.Collections;

public class Room{
	public Rect rect;
	public bool canSpawnEnemys = true;

	public Room(int roomX, int roomY, int roomWidth, int roomHeight){
		this.rect = new Rect (roomX, roomY, roomWidth, roomHeight);
	}
}

