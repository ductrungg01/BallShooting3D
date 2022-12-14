using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    [SerializeField] private GameObject[] levels;

    [Header("WIN/LOSE panel")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

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
        AudioManager.Instance.PlayBackgroundSound("bg1");
        ShowHomeMenuScreen();
    }

    public void SetNoActiveForAll()
    {
        _homeMenuScreen.SetActive(false);
        _backToSelectLevelButton.SetActive(false);
        _settingScreen.SetActive(false);
        _winPanel.SetActive(false);
        _losePanel.SetActive(false);
        isPlaying = false;


        for (int i = 1; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }
    }

    public async void LoadLevel(int level = 0)
    {
        if (level == 0)
        {
            level = LevelManager.Instance.levelIsPlayingRightNow;
        }

        SetNoActiveForAll();

        _mainCharacter.GetComponent<MainCharacter>().SetInitializePositionByLevel(level);

        #region Fake loading canvas
        isLoading = true;

        _loadingLevelCanvas.SetActive(true);
        float counter = 0;
        do
        {
            counter += 0.1f;
            await Task.Delay(50);
            _progressBar.value = counter;
        } while (counter < 0.9f);
        await Task.Delay(300);
        _loadingLevelCanvas.SetActive(false);

        isLoading = false;
        #endregion

        _backToSelectLevelButton.SetActive(true);
        levels[level].SetActive(true); 
        isPlaying = true;
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

    public void ShowWinPanel()
    {
        SetNoActiveForAll();
        _winPanel.SetActive(true);
    }

    public void ShowLosePanel()
    {
        SetNoActiveForAll();
        _losePanel.SetActive(true);
    }
}
