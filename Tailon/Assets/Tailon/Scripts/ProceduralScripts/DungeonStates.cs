using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DungeonStates : MonoBehaviour {

    public static DungeonStates instance = null;
	
	public int dungeonWidth;
	public int dungeonHeight;
	public int roomMaxSize;
	public int roomMinSize;
	public int roomMaxMonsters;
	public int maxRooms;
	public int _playerLevel;
    public float _playerCurrentLevelExp;
    public float _playerNextLevelExp;
    public float _playerHealth;
    public int _dungeonLvl;
    public int _healthRestorage;
    public int _healthPots;
    public int _staminaRestorage;
    public int _staminaPots;
    public int _money;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

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
       _staminaRestorage = 10;
       _money = 0;
	}
    public void resetItems()
    {
        _healthPots = 0;
        _staminaPots = 0;
    }
}

