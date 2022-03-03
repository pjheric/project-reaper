using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 
using TMPro; 
public class CharacterSelect : MonoBehaviour
{
    [SerializeField] PlayerInputManager pim; 
    [SerializeField] CharacterData[] CharDataArray; //This contains all character data
    //All the UI stuff
    [SerializeField] TextMeshProUGUI characterSelectTitle; 
    [SerializeField] TextMeshProUGUI charName;
    [SerializeField] TextMeshProUGUI charSubname; 
    [SerializeField] TextMeshProUGUI charBio;
    [SerializeField] TextMeshProUGUI charPassive;
    [SerializeField] TextMeshProUGUI charBasic;
    [SerializeField] TextMeshProUGUI charSpecial;
    [SerializeField] Image charIngame;
    [SerializeField] Image charFullBody;


    [SerializeField] GameObject jointext; 
    [SerializeField] Button selectButton;
    [SerializeField] GameObject nextButton; 
    [SerializeField] UnityEngine.EventSystems.EventSystem eventsystem; 
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
        //player1Selection = currentIndex;
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
            selectButton.interactable = false; 
        }
        else if(currentPlayer == 2 && currentIndex == player1Selection) //Prevent player 2 from selecting duplicates
        {
            selectButton.interactable = false ;
            eventsystem.SetSelectedGameObject(nextButton); 
        }
        else
        {
            selectButton.interactable = true; 
        }

        //Debug.Log(player1Selection);
        //Debug.Log(player2Selection);
    }

    public void OnPressPrev()
    {
        if(currentIndex == 0)
        {
            currentIndex = CharDataArray.Length - 1;
            Debug.Log(currentIndex);
        }
        else
        {
            currentIndex -= 1;
            Debug.Log(currentIndex);
        }
        UpdateCharacter(); 
    }

    public void OnPressNext()
    {
        if(currentIndex == CharDataArray.Length - 1)
        {
            currentIndex = 0;
            Debug.Log(currentIndex);
        }
        else
        {
            currentIndex += 1;
            Debug.Log(currentIndex);
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
            Debug.Log("Called"); 
        }
        else if (currentPlayer == 2)
        {
            player2Selection = currentIndex;
            Debug.Log(player1Selection);
            Debug.Log(player2Selection);
            //Start the game with the selected characters
        }
    }
    void OnPlayerJoined(PlayerInput input)
    {
        //input.gameObject.GetComponent<edgeScreenIndicatorManager>().locus = locus;
        if (pim.playerCount == 1)
        {
            Debug.Log("1 player");
            //input.gameObject.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen1;
            //input.gameObject.GetComponent<Player>().healthBar = gangHealth;
            //WM.player1 = input.gameObject;
            pim.playerPrefab = CharDataArray[player2Selection].charPrefab; //so that the next join is morrigan
        }
        else if (pim.playerCount == 2)
        {
            Debug.Log("Success");
            SceneManager.LoadScene("MainScene"); 
            //input.gameObject.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen2;
            //input.gameObject.GetComponent<Player>().healthBar = morHealth;
            //WM.player2 = input.gameObject;
            //gameStart = true;//both chars connected begin the game
        }
        else
        {
            Debug.Log("Uh oh");
        }
    }
}
