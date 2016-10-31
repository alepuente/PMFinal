using UnityEngine;
using System.Collections;

public class DungeonController:MonoBehaviour 
{
	public int dungeonWidth = 80;
	public int dungeonHeight = 50;

	public int roomMaxSize = 12;
	public int roomMinSize = 8;
	public int roomMaxMonsters = 3;
	
	public int maxRooms = 10;

	public GameObject floorTilePrefab = null;
	public GameObject floorTilePrefab2 = null;
	public GameObject wallTilePrefab = null;
	public GameObject playerPrefab = null;

	public GameObject enemy1Prefab = null;
	public GameObject enemy2Prefab = null;

	private GameObject _player = null;
	private int _playerX = 0;
	private int _playerY = 0;

	private AABBGenerator _dungeonGenerator = null;
	//private BSPGenerator _dungeonGenerator = null;
	//private AutomataGenerator _dungeonGenerator = null;

	private Tile[,] _tileMap = null;
	private GameObject[,] _tileObjects = null;

	void Start()
	{

		// generamos el dungeon
		_dungeonGenerator = new AABBGenerator();
		//_dungeonGenerator = new BSPGenerator();
		//_dungeonGenerator = new AutomataGenerator();

		_tileMap = _dungeonGenerator.makeMap(dungeonWidth, dungeonHeight, maxRooms, roomMinSize, roomMaxSize);
		//_tileMap = _dungeonGenerator.makeMap(dungeonWidth, dungeonHeight);
		//_tileMap = _dungeonGenerator.makeMap(dungeonWidth, dungeonHeight);


		_tileObjects = new GameObject[dungeonWidth, dungeonHeight];

		float tileWidth = floorTilePrefab.GetComponent<Renderer>().bounds.size.z;
		float tileHeight = floorTilePrefab.GetComponent<Renderer>().bounds.size.x;

		Vector3 translation = new Vector3();
		for (int x = 0; x < dungeonWidth; x++) 
		{
			for (int y = 0; y < dungeonHeight; y++) 
			{
				Tile currentTile = _tileMap[x, y];
				GameObject tileObject = null;

				if (currentTile.blocked != true)
				{
					if(currentTile.room1)	tileObject = (GameObject)GameObject.Instantiate (floorTilePrefab);
					else
					if(currentTile.room2)	tileObject = (GameObject)GameObject.Instantiate (floorTilePrefab2);
					else
					tileObject = (GameObject)GameObject.Instantiate (floorTilePrefab);
					
					translation.x = tileWidth * x;
					translation.z = -tileHeight * y;
					tileObject.transform.Translate (translation);
					_tileObjects [x, y] = tileObject;
				}
				else
				{
					bool wall = false;

					//checkear si es pared de algun room
					for (int i = y - 1; i <= y + 1; i++) {
						for (int j = x - 1; j <= x + 1; j++) {
							if( i > 0 && i < dungeonHeight)
							if( j > 0 && j < dungeonWidth )
							if ( _tileMap[j, i].blocked != true)
								wall = true;
						}
					}
					if (wall) {
						tileObject = (GameObject)GameObject.Instantiate(wallTilePrefab);
						translation.x = tileWidth * x;
						translation.z = -tileHeight * y;
						tileObject.transform.Translate(translation);
						_tileObjects[x, y] = tileObject;
					}
		
				}
			}
		}

		foreach (Room room in _dungeonGenerator.ArrayRooms) 
		{
			placeEnemies(room);
		}

		int PlayerRoomSpawnNumer = Random.Range(0, _dungeonGenerator.ArrayRooms.Count);
		Room PlayerRoomSpawn = _dungeonGenerator.ArrayRooms [PlayerRoomSpawnNumer];
		_dungeonGenerator.ArrayRooms [PlayerRoomSpawnNumer].canSpawnEnemys = false;
		_player = (GameObject)Instantiate (playerPrefab);
		_playerX = (int)Random.Range(PlayerRoomSpawn.rect.xMin + 1, PlayerRoomSpawn.rect.xMax);
		_playerY = (int)Random.Range(PlayerRoomSpawn.rect.yMin + 1, PlayerRoomSpawn.rect.yMax);	
		GameObject currentPlayerTile = (GameObject) _tileObjects[_playerX, _playerY];
		//currentPlayerTile.transform.localScale = new Vector3 (1, 10, 1);
		_player.transform.position = currentPlayerTile.transform.position;
		_player.transform.position += (Vector3.up * 5);
	}

	private void placeEnemies(Room room)
	{
		int monsterCount = Random.Range(0, roomMaxMonsters);
		
		for(int i = 0; i < monsterCount; i++)
		{
			int x = (int)Random.Range(room.rect.xMin + 1, room.rect.xMax);
			int y = (int)Random.Range(room.rect.yMin + 1, room.rect.yMax);

			GameObject newEnemyObject = null;
			
			if (Random.value < 0.50)
			{
				newEnemyObject = (GameObject)GameObject.Instantiate(enemy1Prefab);
			}
			else
			{
				newEnemyObject = (GameObject)GameObject.Instantiate(enemy2Prefab);
			}
			
			GameObject currentTile = (GameObject) _tileObjects[x, y];
			newEnemyObject.transform.Translate(currentTile.transform.position);
		}
	}
}	
