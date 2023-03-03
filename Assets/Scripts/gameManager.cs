using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public bool holdingDisk = false;

    public int diskNum;
    public GetString gs;

    public Stack<GameObject> InitalStack = new Stack<GameObject>();
    public GameObject disk;

    public GameObject Tower1;
    public GameObject Tower2;
    public GameObject Tower3;

    // we need these disk size ninformation to know the positioning of each item
    // this is for the height diff per item, this is the height of 1 disk 
    public float heightDiff;
    // how big is the width interval of each disk
    public float yScale;
    // maximum scale is the largest x scale of an object
    public float maximumScale =5;

    public GameObject winScreen;

    private void Start()
    {
        heightDiff = disk.transform.GetChild(0).transform.localScale.y;
        yScale = disk.transform.GetChild(0).transform.localScale.x;
        Debug.Log(heightDiff);

        Tower1 = GameObject.Find("Tower 1");
        Tower2 = GameObject.Find("Tower 2");
        Tower3 = GameObject.Find("Tower 3");
    }

    public void DeleteStacks()
    {
        Tower1.GetComponent<MouseActions>().EmptyStack();
        Tower2.GetComponent<MouseActions>().EmptyStack();
        Tower3.GetComponent<MouseActions>().EmptyStack();

    }

    public void Generate()
    {
        winScreen.active = false;
        if (gs.inputIsValid)
        {
            // delete all items in all stacks
            DeleteStacks();
            EmptyStack();

            // Create the items
            CreateItems();

            Tower1.GetComponent<MouseActions>().TowerStack = InitalStack;
        }
    }

    public void CreateItems()
    {
        // get the amount of disks inputed by the player
        diskNum = gs.GetDiskNum();
        Debug.Log("Yo! You got it its: " + diskNum);

        // get the starting height
        float startingHeight = heightDiff / 2;

        float tempWidth = 5;
        // LOOP: create disks depending on how many is inputed
        for (int i = diskNum; i > 0; i--)
        {
            // create Disk (newDisk is the parent/disk holder) diskChild is the disk itself
            GameObject newDisk = Instantiate(disk, Tower1.transform, true);
            GameObject diskChild = newDisk.transform.GetChild(0).gameObject;
            SpriteRenderer sp = diskChild.GetComponent<SpriteRenderer>();
            

            //change the position of the disk container (meaning all children too) 
            float xPos = Tower1.transform.position.x;
            newDisk.transform.position = new Vector3(xPos, startingHeight, 0);
            startingHeight += heightDiff;

            //This is for changing the scale of the disks.
            //here we have a special occasion for diskNum=2, because disk size 5 and disk size 1 is awkward.
            //So we put this to make it look less awkward
            if (diskNum==2)
            {
                Vector3 newScale = new Vector3(tempWidth, heightDiff, 1);
                diskChild.transform.localScale = diskChild.transform.parent.InverseTransformVector(newScale);
                tempWidth /= 2;
            }
            else
            {
                //change scale according to value (I put in Math.max because if you input 1 it will give an error as you are dividing by 0)
                yScale = (maximumScale - 1) / Mathf.Max(1, diskNum - 1);
                Vector3 worldScale = new Vector3(maximumScale - (diskNum - i) * yScale, heightDiff, 1);
                diskChild.transform.localScale = diskChild.transform.parent.InverseTransformVector(worldScale);
            }
            

            
            // Change diskValue
            DiskValue dValue = diskChild.GetComponent<DiskValue>();
            dValue.Value = i;
            dValue.ChangeText();
            // Change disk name
            newDisk.name = "Disk " + (i);
            // put disks in the stack
            InitalStack.Push(newDisk);

            //change the color of the disk
            sp.color = Random.ColorHSV(0, 1, .53f, .53f, .76f, .76f);

        }
    }

    public void EmptyStack()
    {
        //destroy the item in the game manager stack
        while (InitalStack.Count != 0)
        {
            GameObject disk = InitalStack.Pop();
            Destroy(disk);
        }

        if (transform.childCount>0)
        {
            //destroy current held child
            Destroy(transform.GetChild(0).gameObject);
        }
        
    }


}
