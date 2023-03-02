using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseActions : MonoBehaviour
{
    public GameObject self;
    public SpriteRenderer sp;

    public GameObject GameManager;
    public gameManager gm;


    //Create the stack to put all the disks in
    public Stack<GameObject> TowerStack = new Stack<GameObject>();

    public Color hoverColor = new Color(0.5f, 0f, 1f);
    Color defaultColor;

    //Win screen to show player they won
    public GameObject winScreen;

    public GameObject spotLight;
    public GameObject volumeLight;


    private void Start()
    {
        //get the existing sprite color
        defaultColor = sp.color;
        GameManager = GameObject.Find("GameManager");
        gm = GameManager.GetComponent<gameManager>();


        
    }



    private void OnMouseEnter()
    {
        //sp.color = hoverColor;
        spotLight.active = true;
    }

    private void OnMouseExit()
    {
        //sp.color = defaultColor;
        spotLight.active = false;
    }


    //upon clicking the tower
    private void OnMouseDown()
    {
        //check if we are holding a child or not

        // if we are not holding a disk
        if (!gm.holdingDisk)
        {
            GrabDisk();
        }

        // place child
        else
        {
            PlaceDisk();
            
        }

    }

    void GrabDisk()
    {
        //get the disk from the tower
        // first we check if we have children
        if (TowerStack.Count > 0)
        {
            //get child and put it in game manager
            GameObject disk = TowerStack.Pop();
            disk.transform.SetParent(GameManager.transform, true);
            //we are now holding a disk so we should set this as true
            gm.holdingDisk = true;

            //When selected we want the disk to go up to signify it is being held. WE keep the x position but raise the y coordinate 
            disk.transform.position = new Vector3(disk.transform.position.x, 6.75f, 0);

        }
    }

    void PlaceDisk()
    {
        //get the held disk and its value
        GameObject disk = GameManager.transform.GetChild(0).gameObject;
        GameObject diskChild = disk.transform.GetChild(0).gameObject;
        int diskVal = diskChild.GetComponent<DiskValue>().Value;

        // check if the top disk is higher value
        if (TowerStack.Count > 0)
        {
            //get the diskval of the top disk in the current tower
            GameObject topDisk = TowerStack.Peek().gameObject;
            GameObject topChild = topDisk.transform.GetChild(0).gameObject;
            int topDiskVal = topChild.GetComponent<DiskValue>().Value;

            //if the disk value of the current tower is higher than the held disk... (AKA if valid move)
            if (topDiskVal > diskVal)
            {
                //...parent it and put it to the selected tower
                disk.transform.SetParent(self.transform, true);
                //change position of disk  x = position of self in x axis, y = count how many is in the stack + the initial one
                disk.transform.position = new Vector3(self.transform.position.x, (gm.heightDiff / 2) + (TowerStack.Count * gm.heightDiff), 0);
                gm.holdingDisk = false;
                //push to array
                TowerStack.Push(disk);
            }

        }
        else
        {
            //parent it and put it to the selected tower
            disk.transform.SetParent(self.transform, true);
            gm.holdingDisk = false;
            TowerStack.Push(disk);

            //move disk to new tower
            disk.transform.position = new Vector3(self.transform.position.x, gm.heightDiff/2, 0);
        }

        // win condition
        Debug.Log(TowerStack.Count + gm.diskNum);
        if (TowerStack.Count == gm.diskNum && self.name!="Tower 1")
        {
            Debug.Log("OMG WINNER WINNDER!!!");
            winScreen.active = true;
        }
    }

    public void EmptyStack()
    {
        while (TowerStack.Count!=0)
        {
           GameObject disk = TowerStack.Pop();
            Destroy(disk);
        }
    }
}
