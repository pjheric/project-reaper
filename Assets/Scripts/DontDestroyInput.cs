using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyInput : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
