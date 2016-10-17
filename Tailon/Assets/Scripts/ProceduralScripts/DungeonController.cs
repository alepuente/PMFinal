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

	enum GameState {Playing, Pass, None};

	private GameState _gameState = GameState.Playing;
	//private GameState _playerAction = GameState.None;
	
	void Start()
	{
		// creamos el sprite del jugador
		_player = (GameObject)Instantiate(playerPrefab);

		// generamos el dungeon
		_dungeonGenerator = new AABBGenerator();
		//_dungeonGenerator = new BSPGenerator();
		//_dungeonGenerator = new AutomataGenerator();

		_tileMap = _dungeonGenerator.makeMap(dungeonWidth, dungeonHeight, maxRooms, roomMinSize, roomMaxSize);
		//_tileMap = _dungeonGenerator.makeMap(dungeonWidth, dungeonHeight);
		//_tileMap = _dungeonGenerator.makeMap(dungeonWidth, dungeonHeight);


		_tileObjects = new GameObject[dungeonWidth, dungeonHeight];

		float tileWidth = floorTilePrefab.GetComponent<Renderer>().bounds.size.x;
		float tileHeight = floorTilePrefab.GetComponent<Renderer>().bounds.size.y;

		Vector3 translation = new Vector3();
		for (int x = 0; x < dungeonWidth; x++) 
		{
			for (int y = 0; y < dungeonHeight; y++) 
			{
				Tile currentTile = _tileMap[x, y];
				GameObject tileObject = null;

				if (currentTile.blocked == true)
				{
					tileObject = (GameObject)GameObject.Instantiate(wallTilePrefab);
				}
				else
				{
					tileObject = (GameObject)GameObject.Instantiate(floorTilePrefab);
				}
		
				translation.x = tileWidth * x;
				translation.z = -tileHeight * y;
				tileObject.transform.Translate(translation);

				_tileObjects[x, y] = tileObject;
			}
		}

		// crear enemigos
		foreach (Rect room in _dungeonGenerator.rooms) 
		{
			placeEnemies(room);
		}

		GameObject firstTile = (GameObject) _tileObjects[_dungeonGenerator.startX, _dungeonGenerator.startY];
		_player.transform.Translate(firstTile.transform.position);

		_playerX = _dungeonGenerator.startX;
		_playerY = _dungeonGenerator.startY;
	}

	void Update()
	{
		if (_gameState == GameState.Playing) 
		{	
			if (Input.GetKeyUp(KeyCode.UpArrow))
			{
				//movePlayerOrAttack(0, -1);
				_playerY--;
			} 
			else if (Input.GetKeyUp(KeyCode.DownArrow)) 
			{
				//movePlayerOrAttack(0, 1);
				_playerY++;
			}
			else if (Input.GetKeyUp(KeyCode.LeftArrow)) 
			{
				//movePlayerOrAttack(-1, 0);
				_playerX--;
			}
			else if (Input.GetKeyUp(KeyCode.RightArrow))
			{
				//movePlayerOrAttack(1, 0);
				_playerX++;
			}
		}
	}

	private void placeEnemies(Rect room)
	{
		int monsterCount = Random.Range(0, roomMaxMonsters);
		
		for(int i = 0; i < monsterCount; i++)
		{
			int x = (int)Random.Range(room.xMin + 1, room.xMax);
			int y = (int)Random.Range(room.yMin + 1, room.yMax);

			GameObject newEnemyObject = null;
			
			if (Random.value < 0.75)
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
