using UnityEngine;
using System.Collections;

public class AABBGenerator 
{
	public int startX = 0;
	public int startY = 0;
	public ArrayList rooms = null;

	private int _dungeonWidth = 80;
	private int _dungeonHeight = 50;
	private int _roomMinSize = 8;
	private int _roomMaxSize = 12;
	private int _maxRooms = 10;

	private Tile[,] _tileMap = null;
	private int _numRooms = 0;


	public Tile[,] makeMap(int width, int height, int maxRooms, int roomMinSize, int roomMaxSize)
	{
		_dungeonWidth = width;
		_dungeonHeight = height;
		_maxRooms = maxRooms;
		_roomMinSize = roomMinSize;
		_roomMaxSize = roomMaxSize;

		// create map
		_tileMap = new Tile[_dungeonWidth, _dungeonHeight];
		
		for(int x = 0; x < _dungeonWidth; x++)
		{
			for(int y = 0; y < _dungeonHeight; y++)
			{
				// intantiate filled tiles
				Tile newTile = new Tile();
				newTile.blocked = true;
				newTile.blockSight = true;
				_tileMap[x, y] = newTile;
			}
		}

		rooms = new ArrayList();
		
		int roomX, roomY, roomWidth, roomHeight;
		roomX = roomY = roomWidth = roomHeight = 0; // wat
		
		for (int r = 0; r < _maxRooms; r++) 
		{
			roomWidth = (int) Random.Range(_roomMinSize, _roomMaxSize);
			roomHeight = (int) Random.Range(_roomMinSize, _roomMaxSize);
			
			roomX = (int) Random.Range(0, _dungeonWidth - roomWidth - 1);
			roomY = (int) Random.Range(0, _dungeonHeight - roomHeight - 1);
			
			Rect newRoom = new Rect(roomX, roomY, roomWidth, roomHeight);
			bool failed = false;

			for (int roomIndex = 0; roomIndex < rooms.Count; roomIndex++)
			{
				Rect otherRoom = (Rect)rooms[roomIndex];

				if (newRoom.Overlaps(otherRoom))
				{
					failed = true;
					break;
				}
			}
			
			if (!failed)
			{
				createRoom(newRoom);
				
				Vector2 center = newRoom.center;
				int newX, newY;
				
				newX = (int)center.x;
				newY = (int)center.y;
				
				if (_numRooms == 0)
				{
					startX = newX;
					startY = newY;
				}
				else
				{
					Rect prevRoom = (Rect)rooms[_numRooms - 1];
					Vector2 prevCenter = prevRoom.center;
					int prevX = (int)prevCenter.x;
					int prevY = (int)prevCenter.y;
					
					if ((int) Random.value == 1)
					{
						createHTunnel(prevX, newX, prevY);
						createVTunnel(prevY, newY, newX);
					} 
					else
					{
						createVTunnel(prevY, newY, prevX);
						createHTunnel(prevX, newX, newY);
					}
				}

				rooms.Add(newRoom);
				_numRooms++;
			}
		}

		return _tileMap;
	}

	private void createRoom(Rect rect)
	{
		for(int x = (int)rect.xMin + 1; x < (int)rect.xMax; x++)
		{
			for(int y = (int)rect.yMin + 1; y < (int)rect.yMax; y++)
			{
				Tile currentTile = _tileMap[x, y];
				currentTile.blocked = false;
				currentTile.blockSight = false;
			}
		}
	}
	
	private void createHTunnel(int x1, int x2, int y)
	{
		for (int x = Mathf.Min(x1, x2); x < Mathf.Max(x1, x2) + 1; x++)
		{
			Tile currentTile = _tileMap[x, y];
			currentTile.blocked = false;
			currentTile.blockSight = false;
		}
	}
	
	private void createVTunnel(int y1, int y2, int x)
	{
		for (int y = Mathf.Min( y1, y2 ); y < Mathf.Max(y1, y2) + 1; y++)
		{
			Tile currentTile = _tileMap[x, y];
			currentTile.blocked = false;
			currentTile.blockSight = false;
		}
	}
}
