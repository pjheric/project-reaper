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
    //Each wave = 1 spreadsheet CSV file; 
    //Each "box" of dialogue = 1 row 
    //Col 1 starts with either "M" or "G"- speaker name
    //Col 2 is where the actual dialogue is
    [SerializeField] public List<string> dataSheet = new List<string>();
    public int currentIndex = 0; 
    public void Start()
    {
    }

    public void StartDialogue()
    {
        DialoguePanel.SetActive(true); 
        //First, freeze time
        Time.timeScale = 0;
        //Read the appropriate CSV file based on the current wave number
        string currentWave = waveManager.currentWaveNum.ToString();
        string fileName = "Dialogue" + currentWave + ".csv";
        string readFromFilePath = Path.Combine(Application.streamingAssetsPath, fileName);
        dataSheet = File.ReadAllLines(readFromFilePath).ToList();
        DisplayDialogue(dataSheet[currentIndex].Split(','));
    }
    public void DisplayDialogue(string[] content)
    {
        string speakername = content[0];
        string speakerBody = content[1]; 
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
        if(currentIndex == dataSheet.Count - 1)
        {
            EndDialogue();
            return; 
        }
        currentIndex += 1;
        DisplayDialogue(dataSheet[currentIndex].Split(','));
    }

    public void EndDialogue()
    {
        currentIndex = 0;
        DialoguePanel.SetActive(false);
        Time.timeScale = 1f; 
    }
}
