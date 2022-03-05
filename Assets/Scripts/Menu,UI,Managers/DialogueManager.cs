using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Linq; 
public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject DialoguePanel; 
    [SerializeField] GameObject inGameUI; 
    
    [SerializeField] mainManager mm; 
    [SerializeField] waveManager wm; 
    //Dialogue is triggered at the beginning of every wave
    [SerializeField] Image Speaker1Image;
    [SerializeField] Image Speaker2Image;

    [SerializeField] GameObject SpeakerName1;
    [SerializeField] GameObject SpeakerName2;
    [SerializeField] TextMeshProUGUI DialogueLine;

    //All dialogue is just an array of list of strings
    //Array index: wave (0, 1, 2, 3, 4)
    //List index: actual dialogue 
    [System.Serializable]
    public struct DialogueWrapper
    {
        public List<string> lines;
    }
    public List<DialogueWrapper> dialogue = new List<DialogueWrapper>(); 
    //Each dialogue starts with either M or G then space then the actual dialogue;
    //This is to distinguish the name of the speaker
    public int currentIndex = 0; 

    public void StartDialogue()
    {
        DialoguePanel.SetActive(true); 
        inGameUI.SetActive(false);
        //First, freeze time
        Time.timeScale = 0;
        DisplayDialogue(dialogue[waveManager.currentWaveNum].lines[currentIndex]); 
    }
    public void DisplayDialogue(string content)
    {
        char speakername = content[0];
        Debug.Log(speakername); 
        string speakerBody = content.Substring(2);
        Debug.Log(speakerBody); 
        if(speakername == 'M')
        {
            SpeakerName1.SetActive(false);
            SpeakerName2.SetActive(true); 
        }
        else if(speakername == 'G')
        {
            SpeakerName1.SetActive(true);
            SpeakerName2.SetActive(false);
        }
        DialogueLine.text = speakerBody; 
    }
    public void OnPressNextButton()
    {
        if(currentIndex == dialogue[waveManager.currentWaveNum].lines.Count - 1)
        {
            EndDialogue();
            return; 
        }
        currentIndex += 1;
        DisplayDialogue(dialogue[waveManager.currentWaveNum].lines[currentIndex]);
    }

    public void EndDialogue()
    {
        currentIndex = 0;
        DialoguePanel.SetActive(false);
        inGameUI.SetActive(true);
        Time.timeScale = 1f;
        if (waveManager.currentWaveNum == 0)
        {
            mm.gameStartFunc();
        }
        else
        {
            wm.startWave();
        }
    }
}
