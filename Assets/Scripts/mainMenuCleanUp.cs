using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuCleanUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var x = GameObject.FindGameObjectsWithTag("Player");
        foreach (var a in x)
        {
            Destroy(a);
        }
        Destroy(GameObject.Find("Input Manager"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
