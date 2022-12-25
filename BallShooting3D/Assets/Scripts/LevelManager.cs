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

    [SerializeField] private GameObject _mainCharacter;

    // UI
    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private GameObject[] levels;

    [SerializeField] private GameObject[] levelButtons;

    public bool isLoading = false;
    public int whatLevelIsPlaying = 1;

    private int nowLevelCanPlay = 7; // 1-4: passed level (can play), nowLevel : Can play, nowLevelCanPlay+1 -> end: Cannot play  
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
        levels[0].SetActive(true);
    }

    /// <summary>
    /// Change Scene/Level
    /// </summary>
    /// <param name="level"> 0: default-> Select level scene 
    ///                      else: load level</param>
    public async void LoadScene(int level = 0)
    {
        // Turn off all the level
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }

        _mainCharacter.GetComponent<MainCharacter>().SetInitializePositionByLevel(level);

        #region Fake loading canvas
        isLoading = true;

        _loaderCanvas.SetActive(true);
        float counter = 0;
        do
        {
            counter += 0.1f;
            await Task.Delay(100);
            _progressBar.value = counter;
        } while (counter < 0.9f);
        await Task.Delay(500);
        _loaderCanvas.SetActive(false);

        isLoading = false;
        #endregion

        levels[level].SetActive(true);
    }

    public void BackToHome()
    {
        LevelManager.Instance.LoadScene(0);
    }

    public void ChangeNowLevel(int nowLevel)
    {
        this.nowLevelCanPlay = nowLevel;

        for (int i = 1; i < nowLevel; i++)
        {
            levelButtons[i].GetComponent<LevelButton>().SetState(LevelButton.LevelButtonState.PassedLevel);
        }

        levelButtons[nowLevel].GetComponent<LevelButton>().SetState(LevelButton.LevelButtonState.NowLevel);

        for (int i = nowLevel + 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<LevelButton>().SetState(LevelButton.LevelButtonState.CannotPlayLevel);
        }
    }
}
