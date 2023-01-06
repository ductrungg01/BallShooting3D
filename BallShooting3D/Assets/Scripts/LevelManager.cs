using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Singleton
    public static LevelManager Instance;

    // Enemy in a level
    public int[] enemyInLevel =
    {
        0,
        16, 
        40,
        33,
        14,
        36,
        33,
        22
    };
    public int enemyCounter = 0;

    // Bullet Spawned list
    public List<GameObject> bulletList = new List<GameObject>();

    
    public int levelIsPlayingRightNow = 1; // 1-4: passed level (can play), nowLevel : Can play, nowLevelCanPlay+1 -> end: Cannot play  
    private int _maxLevel = 7;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangeNowLevel(levelIsPlayingRightNow);
    }

    public void ChangeNowLevel(int levelIsPlayingRightNow)
    {
        this.levelIsPlayingRightNow = levelIsPlayingRightNow;
        enemyCounter = this.enemyInLevel[levelIsPlayingRightNow];
        UIManager.Instance.LoadLevel();
    }

    public void KillEnemy()
    {
        this.enemyCounter--;
        if (enemyCounter == 0)
        {
            UIManager.Instance.ShowWinPanel();
        }
    }

    public void PlayNextLevel()
    {
        DestroyAllBullet();

        if (this.levelIsPlayingRightNow == this._maxLevel)
        {
            return;
        }

        ChangeNowLevel(levelIsPlayingRightNow + 1);
    }

    public void PlayAgain()
    {
        DestroyAllBullet();

        ChangeNowLevel(levelIsPlayingRightNow);
    }

    public void DestroyAllBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (bulletList[i] != null)
            {
                Destroy(bulletList[i]);
            }
        }

        bulletList.Clear();
    }
}
