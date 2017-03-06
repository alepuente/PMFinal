using UnityEngine;
//using UnityEditor;
using System.Collections;

public class DungeonController:MonoBehaviour 
{
	private int dungeonWidth;
	private int dungeonHeight;
	private int roomMaxSize;
	private int roomMinSize;
	private int roomMaxMonsters;	
	private int maxRooms;

	public GameObject CameraPrefab = null;
	public GameObject pasilloTilePrefab = null;
	public GameObject floorTilePrefab = null;
	public GameObject floorTilePrefab2 = null;
	public GameObject floorTilePrefab3 = null;
	public GameObject floorTilePrefab4 = null;
	public GameObject wallTilePrefab = null;
	public GameObject playerPrefab = null;
    public GameObject ObjetivePrefab = null;
	public GameObject Object1 = null;
	public GameObject Object2 = null;
	public GameObject Object3 = null;
	public GameObject Object4 = null;
	public GameObject Object5 = null;
	public GameObject Object6 = null;
	public GameObject Object7 = null;
	public GameObject Object8 = null;
	public GameObject Object9 = null;


	public GameObject enemy1Prefab = null;
    public GameObject enemy2Prefab = null;
    public GameObject enemy3Prefab = null;
    public GameObject enemy4Prefab = null;

    private GameObject _objetive = null;
    private int _objetiveX = 0;
    private int _objetiveY = 0;

	private GameObject _camera = null;
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
        dungeonWidth = DungeonStates.instance.dungeonWidth;
        dungeonHeight = DungeonStates.instance.dungeonHeight;
        roomMaxSize = DungeonStates.instance.roomMaxSize;
        roomMinSize = DungeonStates.instance.roomMinSize;
        roomMaxMonsters = DungeonStates.instance.roomMaxMonsters;
        maxRooms = DungeonStates.instance.maxRooms;

		// generamos el dungeon
		_dungeonGenerator = new AABBGenerator ();
		//_dungeonGenerator = new BSPGenerator();
		//_dungeonGenerator = new AutomataGenerator();

		_tileMap = _dungeonGenerator.makeMap (dungeonWidth, dungeonHeight, maxRooms, roomMinSize, roomMaxSize);
		//_tileMap = _dungeonGenerator.makeMap(dungeonWidth, dungeonHeight);
		//_tileMap = _dungeonGenerator.makeMap(dungeonWidth, dungeonHeight);


		_tileObjects = new GameObject[dungeonWidth, dungeonHeight];
		float tileWidth = floorTilePrefab.GetComponent<Renderer> ().bounds.size.z;
		float tileHeight = floorTilePrefab.GetComponent<Renderer> ().bounds.size.x;

		Vector3 translation = new Vector3 ();
		for (int x = 0; x < dungeonWidth; x++) {
			for (int y = 0; y < dungeonHeight; y++) {
				Tile currentTile = _tileMap [x, y];
				GameObject tileObject = null;

				if (currentTile.blocked != true) {
					if (currentTile.room1) {
						tileObject = (GameObject)GameObject.Instantiate (floorTilePrefab);
					} 
					else if (currentTile.room2) {
						tileObject = (GameObject)GameObject.Instantiate (floorTilePrefab2);
					}
					else if (currentTile.room3) {
						tileObject = (GameObject)GameObject.Instantiate (floorTilePrefab3);
					} 
					else if (currentTile.room4) {
						tileObject = (GameObject)GameObject.Instantiate (floorTilePrefab4);
					}
					else if (currentTile.pasillo){
						tileObject = (GameObject)GameObject.Instantiate (pasilloTilePrefab);
					}
					else{
						tileObject = (GameObject)GameObject.Instantiate (floorTilePrefab);
					}

					translation.x = tileWidth * x;
					translation.z = -tileHeight * y;
					tileObject.transform.Translate (translation);
					_tileObjects [x, y] = tileObject;
					tileObject.transform.parent = gameObject.transform;

				} else {
					bool wall = false;

					//checkear si es pared de algun room
					for (int i = y - 1; i <= y + 1; i++) {
						for (int j = x - 1; j <= x + 1; j++) {
							if (i > 0 && i < dungeonHeight)
							if (j > 0 && j < dungeonWidth)
							if (_tileMap [j, i].blocked != true)
								wall = true;
						}
					}
					if (wall) {
						tileObject = (GameObject)GameObject.Instantiate (wallTilePrefab);
						translation.x = tileWidth * x;
						translation.z = -tileHeight * y;
						tileObject.transform.Translate (translation);
						_tileObjects [x, y] = tileObject;
						tileObject.transform.parent = gameObject.transform;
					}
		
				}
			}
		}

		placeObjects ();
		//NavMeshBuilder.BuildNavMesh ();

		int PlayerRoomSpawnNumer = Random.Range (0, _dungeonGenerator.ArrayRooms.Count);
		Room PlayerRoomSpawn = _dungeonGenerator.ArrayRooms [PlayerRoomSpawnNumer];
		_dungeonGenerator.ArrayRooms [PlayerRoomSpawnNumer].canSpawnEnemys = false;
		_player = (GameObject)Instantiate (playerPrefab);
		_playerX = (int)Random.Range (PlayerRoomSpawn.rect.xMin + 1, PlayerRoomSpawn.rect.xMax);
		_playerY = (int)Random.Range (PlayerRoomSpawn.rect.yMin + 1, PlayerRoomSpawn.rect.yMax);	
		GameObject currentPlayerTile = (GameObject)_tileObjects [_playerX, _playerY];
		_player.transform.position = currentPlayerTile.transform.position;
		_player.transform.position += (Vector3.up * 5);
		_camera = (GameObject)Instantiate (CameraPrefab);
		_camera.transform.position = currentPlayerTile.transform.position;
		_camera.transform.position += (Vector3.up * 10);

		int ObjetiveRoomNumber;
		while (true) {
			ObjetiveRoomNumber = Random.Range (0, _dungeonGenerator.ArrayRooms.Count);

			if (ObjetiveRoomNumber != PlayerRoomSpawnNumer)
				break;
		}

		Room ObjetiveRoom = _dungeonGenerator.ArrayRooms [ObjetiveRoomNumber];
		_objetive = (GameObject)Instantiate (ObjetivePrefab);
		_objetiveX = (int)Random.Range (ObjetiveRoom.rect.xMin + 1, ObjetiveRoom.rect.xMax);
		_objetiveY = (int)Random.Range (ObjetiveRoom.rect.yMin + 1, ObjetiveRoom.rect.yMax);
		GameObject ObjetiveTile = (GameObject)_tileObjects [_objetiveX, _objetiveY];
		_objetive.transform.position = ObjetiveTile.transform.position;
		_objetive.transform.position += (Vector3.up * 5);


		Invoke ("placeEnemies", 2f);
	}

	private void placeEnemies(){
		
	foreach (Room room in _dungeonGenerator.ArrayRooms) {
        if (room.canSpawnEnemys) { 
		int monsterCount = Random.Range(0, roomMaxMonsters);

        for (int i = 0; i < monsterCount; i++)
        {
            int NewEnemyX = (int)Random.Range(room.rect.xMin + 2, room.rect.xMax - 2);
			int NewEnemyY = (int)Random.Range(room.rect.yMin + 2, room.rect.yMax - 2);

            GameObject newEnemyObject = null;

			int value = Random.Range (0, 4);
            switch (value)
            {
                case 0:
                    {
                        newEnemyObject = (GameObject)GameObject.Instantiate(enemy1Prefab);
                    }
                    break;
                case 1:
                    {
                        newEnemyObject = (GameObject)GameObject.Instantiate(enemy2Prefab);
                    }
                    break;
                case 2:
                    {
                        newEnemyObject = (GameObject)GameObject.Instantiate(enemy3Prefab);
                    }
                    break;
                case 3:
                    {
                        newEnemyObject = (GameObject)GameObject.Instantiate(enemy4Prefab);
                    }
                    break;
                default:
                    {
                        newEnemyObject = (GameObject)GameObject.Instantiate(enemy2Prefab);
                    }
                    break;
            }
					
				if (!_tileMap [NewEnemyX, NewEnemyY].blocked) {
					GameObject currentTile = (GameObject)_tileObjects [NewEnemyX, NewEnemyY];
					newEnemyObject.transform.position = currentTile.transform.position;
					newEnemyObject.transform.position += (newEnemyObject.transform.up * 20);
					newEnemyObject.transform.rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);
				}
       		 }
	    
			}
		}
	}

	private void placeObjects(){
		foreach (Room room in _dungeonGenerator.ArrayRooms) {
			
				int objects = Random.Range(1, 3);
			for (int i = 0; i < objects; i++) {
				int tmp = Random.Range (0, 2);
				int tmp2 = Random.Range (0, 2);
				int NewObjectX;	
				int NewObjectY;

				if (tmp > 0){
						NewObjectX = (int)Random.Range (room.rect.xMin, room.rect.xMax);
					if (tmp2 > 0)
						NewObjectY = (int)room.rect.yMax - 1;
					else
						NewObjectY = (int)room.rect.yMin + 1;
				}else {
						NewObjectY = (int)Random.Range (room.rect.yMin, room.rect.yMax);
					if (tmp2 > 0)
						NewObjectX = (int)room.rect.xMax - 1;
					else
						NewObjectX = (int)room.rect.xMin + 1;
				}

				GameObject newObject = null;
				int value = Random.Range (0, 10);
				switch(value){
				case 1:
					{
						newObject = (GameObject)GameObject.Instantiate(Object1);
						break;
					}
				case 2:
					{
						newObject = (GameObject)GameObject.Instantiate(Object2);
						break;
					}
				case 3:
					{
						newObject = (GameObject)GameObject.Instantiate(Object3);
						break;
					}
				case 4:
					{
						newObject = (GameObject)GameObject.Instantiate(Object4);
						break;
					}
				case 5:
					{
						newObject = (GameObject)GameObject.Instantiate(Object5);
						break;
					}
				case 6:
					{
						newObject = (GameObject)GameObject.Instantiate(Object6);
						break;
					}
				case 7:
					{
						newObject = (GameObject)GameObject.Instantiate(Object7);
						break;
					}
				case 8:
					{
						newObject = (GameObject)GameObject.Instantiate(Object8);
						break;
					}
				case 9:
					{
						newObject = (GameObject)GameObject.Instantiate(Object9);
						break;
					}
				default:
					{
						newObject = (GameObject)GameObject.Instantiate(Object1);						
						break;
					}
				}

					if (!_tileMap [NewObjectX, NewObjectY].blocked) {
						GameObject currentTile = (GameObject)_tileObjects [NewObjectX, NewObjectY];
						newObject.transform.position = currentTile.transform.position;
						newObject.transform.position += (newObject.transform.up * 0.5f);
						newObject.transform.rotation = Quaternion.Euler (0, Random.Range (0, 360), 0);
					}
				}
			}
		}
}	
