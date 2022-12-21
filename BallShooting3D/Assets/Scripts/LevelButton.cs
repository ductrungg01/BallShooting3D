using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public enum LevelButtonState
    {
        NowLevel,
        PassedLevel,
        CannotPlayLevel
    }

    Color passedLevelColor = new Color(0, 121f / 255f, 243f / 255f);
    Color nowLevelColor = new Color(255f / 255f, 160f / 255f, 0);
    Color cannotPlayLevelColor = new Color(0, 121f / 255f, 243f / 255f);

    [SerializeField] GameObject blurButton;

    public void SetState(LevelButtonState state)
    {
        switch (state)
        {
            case LevelButtonState.NowLevel:
                this.blurButton.SetActive(false);
                this.GetComponent<Image>().color = nowLevelColor;
                break;
            case LevelButtonState.PassedLevel:
                this.blurButton.SetActive(false);
                this.GetComponent<Image>().color = passedLevelColor;
                break;
            case LevelButtonState.CannotPlayLevel:
                this.blurButton.SetActive(true);
                this.GetComponent<Image>().color = cannotPlayLevelColor;
                break;
        }
    }
}
