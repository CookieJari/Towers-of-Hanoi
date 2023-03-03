using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteButton : MonoBehaviour
{
    public SpriteRenderer sp;

    public Color hoverColor;
    public Color defaultColor;

    public float distance;
    public GameObject camera;

    public bool isPlayButton=false;
    private void OnMouseEnter()
    {
        sp.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sp.color = defaultColor;
    }

    private void OnMouseDown()
    {
        if (isPlayButton)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            camera.transform.Translate(distance, 0, 0);
        }
        
    }
}
