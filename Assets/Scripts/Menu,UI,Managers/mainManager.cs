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
    [SerializeField] TextMeshProUGUI failureText;
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] GameObject gang;
    [SerializeField] GameObject mor;
    [SerializeField] GameObject locusEdgeOfScreen1;
    [SerializeField] GameObject locusEdgeOfScreen2;

    //public static bool gameStart = false;
    public static bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        dm.StartDialogue(); 
    }
    //void OnPlayerJoined(PlayerInput input)
    //{
    //    input.gameObject.GetComponent<edgeScreenIndicatorManager>().locus = locus;
    //    if(playerInputManager.playerCount == 1)
    //    {
    //        input.gameObject.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen1;
    //        input.gameObject.GetComponent<Player>().healthBar = gangHealth;
    //        WM.player1 = input.gameObject;
    //        playerInputManager.playerPrefab = mor;//so that the next join is morrigan
    //    }
    //    else
    //    {
    //        input.gameObject.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen2;
    //        input.gameObject.GetComponent<Player>().healthBar = morHealth;
    //        WM.player2 = input.gameObject;
    //        //gameStart = true;//both chars connected begin the game
    //    }
    //}
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
    public void gameOverFunc()
    {
        gameOver = true;
        Time.timeScale = 0.1f;
        failureScreen.SetActive(true);
        failureText.gameObject.SetActive(true);
        StartCoroutine(fadeInFailText());
        locusHealthText.gameObject.SetActive(false);
    }
    public void gameStartFunc()
    {
        if (DontDestroyInput.player1.GetComponent<GLkit>() != null)
        {
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen1;
            DontDestroyInput.player1.GetComponent<Player>().healthBar = gangHealth;
            WM.player1 = DontDestroyInput.player1;

            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen2;
            DontDestroyInput.player2.GetComponent<Player>().healthBar = morHealth;
            WM.player2 = DontDestroyInput.player2;
        }
        else
        {
            DontDestroyInput.player1.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen2;
            DontDestroyInput.player1.GetComponent<Player>().healthBar = morHealth;
            WM.player1 = DontDestroyInput.player1;

            DontDestroyInput.player2.GetComponent<edgeScreenIndicatorManager>().locusIndicator = locusEdgeOfScreen1;
            DontDestroyInput.player2.GetComponent<Player>().healthBar = gangHealth;
            WM.player2 = DontDestroyInput.player2;
        }
        PersistentData.isGameStarted = true; 

    }
    IEnumerator fadeInFailText()
    {
        float t = 0.0f;
        while(t < 3.0f)
        {
            yield return new WaitForEndOfFrame();
            t += Time.unscaledDeltaTime;
            failureText.color = new Color(failureText.color.r,failureText.color.g,failureText.color.b,t/3);        
        }
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }
}
