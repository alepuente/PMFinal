using UnityEngine;
using UnityEditor;
using System.Collections;

public class DungeonController:MonoBehaviour 
{
	public DungeonStates dungeonController;
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
	public GameObject wallTilePrefab = null;
	public GameObject playerPrefab = null;
    public GameObject ObjetivePrefab = null;

	public GameObject enemy1Prefab = null;
	public GameObject enemy2Prefab = null;

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
		dungeonWidth = dungeonController.dungeonWidth;
		dungeonHeight = dungeonController.dungeonHeight;
		roomMaxSize = dungeonController.roomMaxSize;
		roomMinSize = dungeonController.roomMinSize;
		roomMaxMonsters = dungeonController.roomMaxMonsters;
		maxRooms = dungeonController.maxRooms;

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

		NavMeshBuilder.BuildNavMesh ();

		int PlayerRoomSpawnNumer = Random.Range (0, _dungeonGenerator.ArrayRooms.Count);
		Debug.Log (PlayerRoomSpawnNumer);
		Room PlayerRoomSpawn = _dungeonGenerator.ArrayRooms [PlayerRoomSpawnNumer];
		_dungeonGenerator.ArrayRooms [PlayerRoomSpawnNumer].canSpawnEnemys = false;
		_player = (GameObject)Instantiate (playerPrefab);
		_playerX = (int)Random.Range (PlayerRoomSpawn.rect.xMin + 1, PlayerRoomSpawn.rect.xMax);
		_playerY = (int)Random.Range (PlayerRoomSpawn.rect.yMin + 1, PlayerRoomSpawn.rect.yMax);	
		GameObject currentPlayerTile = (GameObject)_tileObjects [_playerX, _playerY];
		//currentPlayerTile.transform.localScale = new Vector3 (1, 10, 1);
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

		Debug.Log (ObjetiveRoomNumber);
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
            int x = (int)Random.Range(room.rect.xMin, room.rect.xMax);
            int y = (int)Random.Range(room.rect.yMin, room.rect.yMax);

            GameObject newEnemyObject = null;

            if (Random.value < 0.50)
            {
                newEnemyObject = (GameObject)GameObject.Instantiate(enemy1Prefab);
            }
            else
            {
                newEnemyObject = (GameObject)GameObject.Instantiate(enemy1Prefab);
            }
					
				if (!_tileMap [x, y].blocked) {
					GameObject currentTile = (GameObject)_tileObjects [x, y];
					newEnemyObject.transform.Translate (currentTile.transform.position);
					newEnemyObject.transform.position += (newEnemyObject.transform.up * 20);
				}
       		 }
	    
			}
		}
	}
}	
