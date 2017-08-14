using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2Controller : MonoBehaviour
{
    public GameObject go;
    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            createGraphInit();
        }
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            removeGraphInit();
        }



    }

    void createGraphInit()
    {
       go = Instantiate(go);
    }

    void removeGraphInit()
    {
        Destroy(go);
    }
}
