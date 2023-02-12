using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Net.Http.Headers;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    #region Fields
    public static GameManager Instance;

    [Header("MAIN CHARACTER")]
    public GameObject _playerPrefabs;
    public Transform _playerStartingPos;
    [HideInInspector] public GameObject _player;

    [Header("ENEMY")]
    public List<Transform> _enemyPosList = new List<Transform>();
    
    [Header("BOOST")]
    public List<Transform> _boostPosList = new List<Transform>();
    
    [Header("VEHICLE")]
    public List<VehicleStartPointInfor> _vehicleStartInforList = new List<VehicleStartPointInfor>();
    
    [Header("DELAY")]
    public float _startDelay = 2f;
    public float _endDelay = 3f;

    [Header("OTHERS")]
    public Text _levelText;
    public Text _messageText;


    // 
    private int _level = 1;
    [HideInInspector] public Vector3 _bulletHeight = new Vector3(0, 0.8f, 0);
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {        
        GameLoop();
    }

    

    async void GameLoop()
    {
        await RoundStarting();
        await RoundPlaying();
        await RoundEnding();

        if (IsPlayerDead())
        {
            SceneManager.LoadScene("GameScene NEW");
        }
        else
        {
            GameLoop();
        }
    }

    private async UniTask RoundStarting()
    {
        // Generate new maps
        MapsGenerate.Instance.GenerateMaps();
        
        // Return all the remain bullet on the game
        PoolManager.Instance.bulletPooler.OnReturnAll();
        
        ResetAll();
        DisableEnemy();
        DisableShooting();
        
        _levelText.text = "Level " + _level;
        _messageText.text = "READY...";

        await UniTask.Delay(TimeSpan.FromSeconds(_startDelay));

        _messageText.text = "GO!";

        await UniTask.Delay(TimeSpan.FromSeconds(_startDelay));
    }

    private async UniTask RoundPlaying()
    {
        EnableEnemy();
        EnableShooting();

        _levelText.text = string.Empty;
        _messageText.text = string.Empty;

        while (!IsPlayerDead() && !NoEnemyLeft())
        {
            await UniTask.Yield();
        }
    }

    private async UniTask RoundEnding()
    {
        DisableEnemy();

        string message = EndMessage();
        _messageText.text = message;
        _level++;

        await UniTask.Delay(TimeSpan.FromSeconds(_endDelay));
    }

    private bool IsPlayerDead()
    {
        return this._player == null;
    }

    private bool NoEnemyLeft()
    {
        foreach (var enemy in EnemyManager.Instance._enemyList)
        {
            if (enemy.activeSelf == true) return false;
        }

        return true;
    }

    private string EndMessage()
    {
        return (IsPlayerDead() ? "Gameover!" : "You win!!!");
    }

    private void ResetAll()
    {
        Destroy(this._player);
        _player = Instantiate(_playerPrefabs, _playerStartingPos.position, Quaternion.identity);

        DestroyAllEnemy();
        SpawnAllEnemy();

        ClearAllBoost();
        SpawnAllBoost();
        
        ClearAllVehicle();
        SpawnAllVehicle();
    }

    #region Reset

    void SpawnAllEnemy()
    {
        System.Random rnd = new System.Random();
        int num = EnemyManager.Instance._enemyPrefabs.Count;

        for (int i = 0; i < this._enemyPosList.Count; i++)
        {
            int id = rnd.Next(num);

            GameObject enemy = Instantiate(EnemyManager.Instance._enemyPrefabs[id], 
                _enemyPosList[i].position, 
                Quaternion.identity);
            
            EnemyManager.Instance._enemyList.Add(enemy);
        }
    }

    void DestroyAllEnemy()
    {
        for (int i = 0; i < EnemyManager.Instance._enemyList.Count; i++)
        {
            Destroy(EnemyManager.Instance._enemyList[i]);
        }
        EnemyManager.Instance._enemyList.Clear();
    }

    void ClearAllBoost()
    {
        for (int i = 0; i < BoostManager.Instance._boostList.Count; i++)
        {
            Destroy(BoostManager.Instance._boostList[i]);
        }
        
        BoostManager.Instance._boostList.Clear();
    }

    void SpawnAllBoost()
    {
        for (int i = 0; i < _boostPosList.Count; i++)
        {
            GameObject boost = Instantiate(BoostManager.Instance._boostPrefab,
                _boostPosList[i].position,
                Quaternion.identity);

            boost.GetComponent<Boost>()._boostValue = BoostManager.Instance.getRandomBoostValue();
        }
    }

    void ClearAllVehicle()
    {
        for (int i = 0; i < VehicleManager.Instance._vehicleList.Count; i++)
        {
            Destroy(VehicleManager.Instance._vehicleList[i]);
        }
        
        VehicleManager.Instance._vehicleList.Clear();
    }

    void SpawnAllVehicle()
    {
        System.Random rnd = new System.Random();
        int num = VehicleManager.Instance._vehiclePrefabs.Count;

        for (int i = 0; i < this._vehicleStartInforList.Count; i++)
        {
            int id = rnd.Next(num);

            GameObject vehicle = Instantiate(VehicleManager.Instance._vehiclePrefabs[id],
                                                    _vehicleStartInforList[i].position,
                                                    _vehicleStartInforList[i].rotation);

            vehicle.GetComponent<Vehicle>()._direction = _vehicleStartInforList[i].direction;
            
            VehicleManager.Instance._vehicleList.Add(vehicle);
        }
    }

    #endregion

    private void DisableShooting()
    {
        this._player.GetComponent<PlayerShooting>().enabled = false;
    }
    
    private void EnableShooting()
    {
        this._player.GetComponent<PlayerShooting>().enabled = true;
    }


    private void EnableEnemy()
    {
        foreach (var e in EnemyManager.Instance._enemyList)
        {
            e.GetComponent<EnemyMovement>().enabled = true;
        }
    }

    private void DisableEnemy()
    {
        foreach (var e in EnemyManager.Instance._enemyList)
        {
            e.GetComponent<EnemyMovement>().enabled = false;
        }
    }
}
