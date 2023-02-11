using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
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

    [Header("DELAY")]
    public float _startDelay = 2f;
    public float _endDelay = 3f;

    [Header("OTHERS")]
    public Text _levelText;
    public Text _messageText;


    // 
    private int _level = 1;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {        
        GameLoop();
    }

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
        EnemyManager.Instance._enemyList.Clear();
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
        MapsGenerate.Instance.GenerateMaps();
        PoolManager.Instance.bulletPooler.OnReturnAll();
        ResetAll();
        DisableEnemy();

        _levelText.text = "Level " + _level;
        _messageText.text = "READY...";

        await UniTask.Delay(TimeSpan.FromSeconds(_startDelay));

        _messageText.text = "GO!";

        await UniTask.Delay(TimeSpan.FromSeconds(_startDelay));
    }

    private async UniTask RoundPlaying()
    {
        EnableEnemy();

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
