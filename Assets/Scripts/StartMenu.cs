using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject NarrationPanel;
    [SerializeField] GameObject MenuPanel; 
    [SerializeField] float waitTime;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject ControlsPanel;
    public Animator NarrationAnim; 
    IEnumerator co;
    public bool AlreadyNarrated = false; 
    private void Awake()
    {
        if (!AlreadyNarrated)
        {
            AlreadyNarrated = true; 
            NarrationAnim = NarrationPanel.GetComponentInChildren<Animator>();
            co = NarrationCoroutine();
            StartCoroutine(co);
        }
    }
    IEnumerator NarrationCoroutine()
    {
        NarrationPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(waitTime);
        NarrationPanel.SetActive(false);
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

    public void OnPressCredits()
    {
        MenuPanel.SetActive(false);
        CreditsPanel.SetActive(true); 
    }
    public void OnPressControls()
    {
        MenuPanel.SetActive(false);
        ControlsPanel.SetActive(true); 
    }
    public void OnPressBackCredits()
    {
        CreditsPanel.SetActive(false);
        MenuPanel.SetActive(true); 
    }
    public void OnPressBackControls()
    {
        ControlsPanel.SetActive(false);
        MenuPanel.SetActive(true); 
    }
}
