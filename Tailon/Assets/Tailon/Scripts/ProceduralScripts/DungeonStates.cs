using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DungeonStates : MonoBehaviour {
	
	public int dungeonWidth = 60;
	public int dungeonHeight = 60;
	public int roomMaxSize = 20;
	public int roomMinSize = 15;
	public int roomMaxMonsters = 2;
	public int maxRooms = 5;
	public int _playerLevel = 0;
    public float _playerCurrentLevelExp;
    public float _playerNextLevelExp;
    public float _playerHealth;
    public int _dungeonLvl;
    public int _healthRestorage;

	public void restartStates(){
		dungeonWidth = 30;
		dungeonHeight = 30;
		roomMaxSize = 10;
		roomMinSize = 8;
		roomMaxMonsters = 2;
		maxRooms = 3;
        _playerLevel = 1;
       _dungeonLvl = 1;
       _playerCurrentLevelExp = 0;
       _playerHealth = 100;
       _playerNextLevelExp = 1000;
       _healthRestorage = 20;
        
	}
}

