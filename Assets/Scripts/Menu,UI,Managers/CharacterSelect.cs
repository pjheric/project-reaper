using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
public class CharacterSelect : MonoBehaviour
{
    [SerializeField] CharacterData[] CharDataArray; //This contains all character data
    [SerializeField] TextMeshProUGUI characterSelectTitle; 
    [SerializeField] TextMeshProUGUI charName;
    [SerializeField] TextMeshProUGUI charSubname; 
    [SerializeField] TextMeshProUGUI charBio;
    [SerializeField] TextMeshProUGUI charPassive;
    [SerializeField] TextMeshProUGUI charBasic;
    [SerializeField] TextMeshProUGUI charSpecial;
    [SerializeField] Image charIngame;
    [SerializeField] Image charFullBody;

    [SerializeField] GameObject selectButton;

    private int currentIndex; //Index of chardataarray that the menu is currently on; 0-3
    private int currentPlayer; //Number of current player; either 1 or 2

    //Character number of the selected character for each player; either 0 or 1
    public int player1Selection;
    public int player2Selection; 
    void Start()
    {
        characterSelectTitle.text = "CHOOSE YOUR REAPER (P1)";
        currentIndex = 0;
        currentPlayer = 1;
        player1Selection = currentIndex;
        UpdateCharacter(); 
    }

    private void UpdateCharacter() //Updates the necessary UI elements.
    {
        CharacterData currentChar = CharDataArray[currentIndex];
        charName.text = currentChar.charName;
        charSubname.text = currentChar.charSubname; 
        charBio.text = currentChar.charBio;
        charPassive.text = currentChar.charSkillDescription[0];
        charBasic.text = currentChar.charSkillDescription[1];
        charSpecial.text = currentChar.charSkillDescription[2];
        charIngame.sprite = currentChar.charIngameImage;
        charFullBody.sprite = currentChar.charFullImage; 

        if(currentIndex == 2 || currentIndex == 3)
        {
            selectButton.SetActive(false); 
        }
        else if(currentPlayer == 2 && currentIndex == player1Selection) //Prevent player 2 from selecting duplicates
        {
            selectButton.SetActive(false); 
        }
        else
        {
            selectButton.SetActive(true); 
        }
    }

    public void OnPressPrev()
    {
        if(currentIndex == 0)
        {
            currentIndex = CharDataArray.Length - 1; 
        }
        else
        {
            currentIndex -= 1; 
        }
        UpdateCharacter(); 
    }

    public void OnPressNext()
    {
        if(currentIndex == CharDataArray.Length - 1)
        {
            currentIndex = 0; 
        }
        else
        {
            currentIndex += 1; 
        }
        UpdateCharacter(); 
    }

    public void OnClickSelect()
    {
        if(currentPlayer == 1)
        {
            player1Selection = currentIndex;
            currentPlayer = 2;
            currentIndex = 0;
            UpdateCharacter();
            characterSelectTitle.text = "CHOOSE YOUR REAPER (P2)"; 
        }
        else if (currentPlayer == 2)
        {
            player2Selection = currentIndex;
            //Start the game with the selected characters
        }
    }
}
