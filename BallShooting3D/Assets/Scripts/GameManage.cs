using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public bool winState = false;
    public bool loseState = false;
    public void SetWin()
    {
        winState = true;
        loseState = false;
    }

    public void SetLose()
    {
        winState = false;
        loseState = true;
    }
}
