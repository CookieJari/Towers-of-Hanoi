using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneSwap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstructionSwitch() 
    {
        SceneManager.LoadScene(1);
        
    }
    public void GameSwitch()
    {
        SceneManager.LoadScene(2);

    }

}
