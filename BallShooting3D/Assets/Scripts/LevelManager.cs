using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private GameObject[] levels;

    public bool isLoading = false;

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

        isLoading = true;
        _loaderCanvas.SetActive(true);
        float counter = 0;
        do
        {
            counter += 0.1f;
            await Task.Delay(200);
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
}
