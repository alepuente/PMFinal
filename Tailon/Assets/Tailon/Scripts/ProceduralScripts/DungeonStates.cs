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

    public float _meleeDamage;
    public float _rangeDamage;
    public float _maxStamine;
    public int upgradePoints;
    public int maxHealth;

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

        if (PlayerPrefs.GetInt("healthRestorage") != 0)
        {            
        _money = PlayerPrefs.GetInt("money");
        _healthRestorage = PlayerPrefs.GetInt("healthRestorage");
        _staminaRestorage = PlayerPrefs.GetInt("staminaRestorage");
        _meleeDamage = PlayerPrefs.GetFloat("meleeDamage");
        _rangeDamage = PlayerPrefs.GetFloat("rangeDamage");
        _playerLevel = PlayerPrefs.GetInt("lvl");
        _playerNextLevelExp = PlayerPrefs.GetFloat("nextLvl");
        _playerCurrentLevelExp = PlayerPrefs.GetFloat("currentEXP");
        upgradePoints = PlayerPrefs.GetInt("upgradePoints");
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
       _dungeonLvl = 1;
       _playerHealth = 100;
	}
    public void resetItems()
    {
        _healthPots = 0;
        _staminaPots = 0;
    }
    public void saveStats()
    {
        PlayerPrefs.SetInt("money", _money);
        PlayerPrefs.SetInt("healthRestorage", _healthRestorage);
        PlayerPrefs.SetInt("staminaRestorage", _staminaRestorage);
        PlayerPrefs.SetFloat("meleeDamage", _meleeDamage);
        PlayerPrefs.SetFloat("rangeDamage", _rangeDamage);
        PlayerPrefs.SetInt("lvl", _playerLevel);
        PlayerPrefs.SetFloat("nextLvl", _playerNextLevelExp);
        PlayerPrefs.SetFloat("currentEXP", _playerCurrentLevelExp);
        PlayerPrefs.SetInt("upgradePoints", upgradePoints);

        PlayerPrefs.Save();
    }
}

