using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{

    public GameObject quitPanel;
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void openQuitPanel()
    {
        quitPanel.active = true;
    }
    public void resumeGame()
    {
        quitPanel.active = false;
    }
}
