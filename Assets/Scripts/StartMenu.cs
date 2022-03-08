using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject NarrationPanel;
    [SerializeField] GameObject MenuPanel; 
    [SerializeField] float waitTime = 6.0f;
    public Animator NarrationAnim; 
    IEnumerator co; 
    private void Start()
    {
        NarrationAnim = NarrationPanel.GetComponentInChildren<Animator>(); 
        co = NarrationCoroutine();
        StartCoroutine(co); 
    }
    IEnumerator NarrationCoroutine()
    {
        MenuPanel.SetActive(false);
        NarrationPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(waitTime);
        NarrationPanel.SetActive(false);
        MenuPanel.SetActive(true); 
    }
    public void OnPressStart()
    {
        SceneManager.LoadScene("Character Select Local");
    }

    public void OnPressOptions()
    {
        OptionsPanel.SetActive(true);
        MenuPanel.SetActive(false); 
    }

    public void OnPressQuit()
    {
        Application.Quit(); 
    }
}
