using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Storage storage;
    public Recipe testrecipe;
    public Item testclos;
    public Item testige;
    public int testnum =1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(storage.Storage_Finding(testrecipe));
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            storage.Storage_Organization();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(storage.Storage_Space_Finding(testclos, testnum));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            storage.Storage_Add(testclos, testnum);
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            storage.Storage_Remove(testige, testnum, false);
        }
    }
}
