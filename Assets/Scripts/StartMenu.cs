using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class StartMenu : MonoBehaviour
{
    [SerializeField] GameObject OptionsPanel;
    [SerializeField] GameObject NarrationPanel;
    [SerializeField] GameObject MenuPanel;
    [SerializeField] GameObject startButton; 
    [SerializeField] GameObject controlsButton; 
    [SerializeField] GameObject creditsButton; 
    [SerializeField] float waitTime;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject ControlsPanel;
    [SerializeField] GameObject optionsBackButton;
    [SerializeField] GameObject creditsBackButton;
    [SerializeField] GameObject controlsBackButton;
    public Animator NarrationAnim;
    [SerializeField] EventSystem es; 
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
            es.SetSelectedGameObject(startButton); 
        }
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
        es.SetSelectedGameObject(optionsBackButton); 
    }

    public void OnPressQuit()
    {
        Application.Quit(); 
    }

    public void OnPressCredits()
    {
        MenuPanel.SetActive(false);
        CreditsPanel.SetActive(true); 
        es.SetSelectedGameObject(creditsBackButton); 

    }
    public void OnPressControls()
    {
        MenuPanel.SetActive(false);
        ControlsPanel.SetActive(true); 
        es.SetSelectedGameObject(controlsBackButton); 

    }
    public void OnPressBackCredits()
    {
        CreditsPanel.SetActive(false);
        MenuPanel.SetActive(true); 
        es.SetSelectedGameObject(creditsButton); 
    }
    public void OnPressBackControls()
    {
        ControlsPanel.SetActive(false);
        MenuPanel.SetActive(true); 
        es.SetSelectedGameObject(controlsButton); 
    }
}
