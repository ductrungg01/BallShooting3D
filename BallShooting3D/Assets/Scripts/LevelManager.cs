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

    public int nowLevelCanPlay = 7; // 1-4: passed level (can play), nowLevel : Can play, nowLevelCanPlay+1 -> end: Cannot play  
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
        ChangeNowLevel(nowLevelCanPlay);
    }

    public void ChangeNowLevel(int nowLevelCanPlay)
    {
        this.nowLevelCanPlay = nowLevelCanPlay;
    }
}
