using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class Player : MonoBehaviour
{
    [SerializeField] Entity entity;
    public Slider healthBar;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistentData.isGameStarted)
        {
            healthBar.value = entity.currentHealth;
        }
    }
}
