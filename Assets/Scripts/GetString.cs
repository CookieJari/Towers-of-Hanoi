using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetString : MonoBehaviour
{
    private string strInput;
    public bool inputIsValid =false;

    public TMP_Text tx;

    

    public Color invalidColor;
    public Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This is the string from the Input Field
    public void StringInput(string s)
    {
        strInput = s;

        bool par = int.TryParse(strInput, out _);
        if (par && int.Parse(strInput)<= 10)
        {
            tx.text = "Generate Puzzle";
            inputIsValid = true;
            //tx.color = defaultColor;
            Debug.Log("Valid");
        }
        else
        {
            inputIsValid = false;
            tx.text = "INVALID";
            //tx.color = invalidColor;
            Debug.Log("Invalid");
        }
    }


    public int GetDiskNum()
    {
        bool par = int.TryParse(strInput, out _);

        if (par)
        {
            return int.Parse(strInput);
        }
        else
        {
            // CHANGE THIS LATER IF IT IS AN INVALID NUMBER
            Debug.Log("Invalid number");
            return 0;
        }
    }
}
