using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiskValue : MonoBehaviour
{
    public int Value = 1;

    public TMP_Text tx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeText()
    {
        Debug.Log(Value.ToString());
        tx.text = Value.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
