using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class mainManager : MonoBehaviour
{
    [SerializeField] DialogueManager dm; 
    [SerializeField] waveManager WM;
    [SerializeField] GameObject locus;
    [SerializeField] TextMeshProUGUI locusHealthText;
    [SerializeField] TextMeshProUGUI waveEnemyText;
    [SerializeField] Slider gangHealth;
    [SerializeField] Slider morHealth;
    [SerializeField] GameObject failureScreen;
    [SerializeField] GameObject inGameUI;
    [SerializeField] TextMeshProUGUI failureText;
    [SerializeField] Image failureMainMenuButtonImage;
    [SerializeField] TextMeshProUGUI failureMainMenuButtonText;
    [SerializeField] PlayerInputManager pim;
    [SerializeField] GameObject gang;
    [SerializeField] GameObject mor;
    [SerializeField] GameObject locusEdgeOfScreen1;
    [SerializeField] GameObject locusEdgeOfScreen2;
    [SerializeField] GameObject playerEdgeOfScreen1;
    [SerializeField] GameObject playerEdgeOfScreen2;

    
    //public static bool gameStart = false;
    public static bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        dm.StartDialogue();
    }
    // Update is called once per frame
    void Update()
    {
        if(WM.waveRunning)
        {
            waveEnemyText.text = "Wave: "+(waveManager.currentWaveNum+1)+" | Enemies: "+ waveManager.enemyCount;
        }else
        {
            waveEnemyText.text = "Wave Incoming...";
        }
        locusHealthText.text = locus.GetComponent<Entity>().currentHealth+"/"+locus.GetComponent<Entity>().maxHealth;
        if(locus.GetComponent<Entity>().currentHealth<50)
        {
            locusHealthText.color = Color.red;
            locus.transform.GetChild(0).gameObject.SetActive(false);
            locus.transform.GetChild(1).gameObject.SetActive(true);
            locus.GetComponent<Entity>().spriteObjSprite =  locus.transform.GetChild(1).GetComponent<SpriteRenderer>();
        }
        else
        {
            locusHealthText.color = Color.white;
            locus.transform.GetChild(0).gameObject.SetActive(true);
            locus.transform.GetChild(1).gameObject.SetActive(false);
            locus.GetComponent<Entity>().spriteObjSprite = locus.transform.GetChild(0).GetComponent<SpriteRenderer>();

        }
        if(locus.GetComponent<Entity>().currentHealth<=0 && gameOver == false)
        {
            gameOverFunc();
        }
    }

    public void gameStartFunc()
    {
        if (DontDestroyInput.player1.GetComponent<GLkit>() != null)
        {
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen1;
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().locus = locus;
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().otherPlayerIndicator = playerEdgeOfScreen1;
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().otherPlayer = DontDestroyInput.player2;
            DontDestroyInput.player1.GetComponent<Player>().healthBar = gangHealth;
            WM.player1 = DontDestroyInput.player1;

            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen2;
            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().locus = locus;
            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().otherPlayerIndicator = playerEdgeOfScreen2;
            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().otherPlayer = DontDestroyInput.player1;
            DontDestroyInput.player2.GetComponent<Player>().healthBar = morHealth;
            WM.player2 = DontDestroyInput.player2;
        }
        else
        {
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen2;
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().locus = locus;
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().otherPlayerIndicator = playerEdgeOfScreen2;
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().otherPlayer = DontDestroyInput.player2;
            DontDestroyInput.player1.GetComponent<Player>().healthBar = morHealth;
            WM.player1 = DontDestroyInput.player1;

            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen1;
            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().locus = locus;
            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().otherPlayerIndicator = playerEdgeOfScreen1;
            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().otherPlayer = DontDestroyInput.player1;
            DontDestroyInput.player2.GetComponent<Player>().healthBar = gangHealth;
            WM.player2 = DontDestroyInput.player2;
        }
        PersistentData.isGameStarted = true; 
        inGameUI.SetActive(true);

    }
    public void gameOverFunc()
    {
        gameOver = true;
        Time.timeScale = 0.1f;
        failureScreen.SetActive(true);
        inGameUI.SetActive(false);
        failureText.gameObject.SetActive(true);
        failureMainMenuButtonImage.gameObject.SetActive(true);
        locusHealthText.gameObject.SetActive(false);
        PersistentData.isGameStarted = false;
        StartCoroutine(fadeInFailText());
    }
    IEnumerator fadeInFailText()
    {
        float t = 0.0f;
        while(t < 3.0f)
        {
            yield return new WaitForEndOfFrame();
            t += Time.unscaledDeltaTime;
            failureText.color = new Color(failureText.color.r,failureText.color.g,failureText.color.b,t/3);  
            failureMainMenuButtonImage.color = new Color(failureMainMenuButtonImage.color.r,failureMainMenuButtonImage.color.g,failureMainMenuButtonImage.color.b,t/3);     
            failureMainMenuButtonText.color = new Color(failureMainMenuButtonText.color.r,failureMainMenuButtonText.color.g,failureMainMenuButtonText.color.b,t/3);     

        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }

    public void returnToMainMenu() 
    {
        SceneManager.LoadScene("Start Menu");
    }
}
