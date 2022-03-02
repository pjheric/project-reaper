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
    //Dialogue is triggered at the beginning of every wave
    [SerializeField] Image Speaker1Image;
    [SerializeField] Image Speaker2Image;
    [SerializeField] TextMeshProUGUI SpeakerName;
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
    public int currentWave = waveManager.currentWaveNum - 1; 
    public void Start()
    {
        StartDialogue(); 
    }

    public void StartDialogue()
    {
        DialoguePanel.SetActive(true); 
        //First, freeze time
        Time.timeScale = 0;
    }
    public void DisplayDialogue(string content)
    {
        string speakername = content.Substring(0, 1);
        string speakerBody = content.Substring(2); 
        if(speakername == "M")
        {
            SpeakerName.text = "Morrigan";
            //Set alpha of "opposite speaker image" to 150; blur out the other side
            Image OppositeImage = Speaker1Image.GetComponent<Image>();
            OppositeImage.color = new Color(OppositeImage.color.r, OppositeImage.color.g, OppositeImage.color.b, 150f);
        }
        else
        {
            SpeakerName.text = "Gang-lim";
            Image OppositeImage = Speaker2Image.GetComponent<Image>();
            OppositeImage.color = new Color(OppositeImage.color.r, OppositeImage.color.g, OppositeImage.color.b, 150f);
        }
        DialogueLine.text = speakerBody; 
    }
    public void OnPressNextButton()
    {
        if(currentIndex == dialogue[currentWave].lines.Count - 1)
        {
            EndDialogue();
            return; 
        }
        currentIndex += 1;
        DisplayDialogue(dialogue[currentWave].lines[currentIndex]);
    }

    public void EndDialogue()
    {
        currentIndex = 0;
        DialoguePanel.SetActive(false);
        Time.timeScale = 1f; 
    }
}
