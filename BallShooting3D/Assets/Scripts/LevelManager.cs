using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _mainCharacter;
    Vector3[] _mcPositions =
    {
        new Vector3(),
        new Vector3(0.23f, 0f, -2.66f),                 // level1
        new Vector3(0.08f, 0f, 1.21f),                  // level2
        new Vector3(-4f, 0f, -2.51f),                   // level3                
        new Vector3(-3.49f, 0f, 4.61f),                 // level4
        new Vector3(0f, 0f, 1f),                        // level5
        new Vector3(0.04f, 0f, -2.55f),                 // level6
        new Vector3(3.17f, 0f, -1.65f),                 // level7
    };

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private GameObject[] levels;

    [SerializeField] private GameObject[] levelButtons;

    public bool isLoading = false;

    private int nowLevel = 7; // 1-4: passed level (can play), nowLevel : Can play, nowLevel+1 -> end: Cannot play  

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
        ChangeNowLevel(nowLevel);
        levels[0].SetActive(true);
    }

    /// <summary>
    /// Change Scene/Level
    /// </summary>
    /// <param name="level"> 0: default-> Select level scene 
    ///                      else: load level</param>
    public async void LoadScene(int level = 0)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(false);
        }

        _mainCharacter.transform.position = _mcPositions[level];

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

        levels[level].SetActive(true);
    }

    public void BackToHome()
    {
        LevelManager.Instance.LoadScene(0);
    }

    public void ChangeNowLevel(int nowLevel)
    {
        this.nowLevel = nowLevel;

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
