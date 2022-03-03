using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DontDestroyInput : MonoBehaviour
{
    public static GameObject player1;
    public static GameObject player2; 
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
