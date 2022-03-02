using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsPanel;

    public void OnPressStart()
    {
        SceneManager.LoadScene("Character Select Local");
    }

    public void OnPressOptions()
    {
        OptionsPanel.SetActive(true);
        this.gameObject.SetActive(false); 
    }

    public void OnPressQuit()
    {
        Application.Quit(); 
    }
}
