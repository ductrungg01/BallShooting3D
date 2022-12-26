using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private GameObject _mainCharacter;

    [Header("LEVEL")]
    [SerializeField] private GameObject _selectLevelPanel;
    public int whatLevelIsPlaying = 1;
    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject[] levelButtons;

    [Header("LOADING")]
    [SerializeField] private GameObject _loadingLevelCanvas;
    [SerializeField] private Slider _progressBar;
    public bool isLoading = false;

    [Header("GAME FIELD")]
    [SerializeField] private GameObject _backToSelectLevelButton;
    public bool isPlaying = false;

    [Header("HOME MENU")]
    [SerializeField] private GameObject _homeMenuScreen;

    [Header("SETTING SCREEN")]
    [SerializeField] private GameObject _settingScreen;


    private void Start()
    {
        ShowHomeMenuScreen();
    }

    public void SetNoActiveForAll()
    {
        _selectLevelPanel.SetActive(false);
        _homeMenuScreen.SetActive(false);
        _backToSelectLevelButton.SetActive(false);
        _settingScreen.SetActive(false);
        isPlaying = false;

        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }

    public async void LoadScene(int level)
    {
        SetNoActiveForAll();

        _mainCharacter.GetComponent<MainCharacter>().SetInitializePositionByLevel(level);

        #region Fake loading canvas
        isLoading = true;

        _loadingLevelCanvas.SetActive(true);
        float counter = 0;
        do
        {
            counter += 0.1f;
            await Task.Delay(100);
            _progressBar.value = counter;
        } while (counter < 0.9f);
        await Task.Delay(500);
        _loadingLevelCanvas.SetActive(false);

        isLoading = false;
        #endregion

        _backToSelectLevelButton.SetActive(true);
        levels[level].SetActive(true);
        isPlaying = true;
    }

    public void ShowSelectLevelScreen(int nowLevelCanPlay = 0)
    {
        SetNoActiveForAll();

        if (nowLevelCanPlay == 0)
        {
            nowLevelCanPlay = LevelManager.Instance.nowLevelCanPlay;
        }

        #region Change Button Color
        for (int i = 1; i < nowLevelCanPlay; i++)
        {
            levelButtons[i].GetComponent<LevelButton>().
                    SetState(LevelButton.LevelButtonState.PassedLevel);
        }

        levelButtons[nowLevelCanPlay].GetComponent<LevelButton>().
                    SetState(LevelButton.LevelButtonState.NowLevel);

        for (int i = nowLevelCanPlay + 1; i < levelButtons.Length; i++)
        {
            levelButtons[i].GetComponent<LevelButton>().
                    SetState(LevelButton.LevelButtonState.CannotPlayLevel);
        }
        #endregion

        _selectLevelPanel.SetActive(true);
    }

    public void ShowHomeMenuScreen()
    {
        SetNoActiveForAll();
        _homeMenuScreen.SetActive(true);
    }

    public void ShowSettingScreen()
    {
        SetNoActiveForAll();
        _settingScreen.SetActive(true);
    }
}
