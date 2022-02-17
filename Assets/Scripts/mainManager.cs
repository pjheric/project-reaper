using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mainManager : MonoBehaviour
{
    [SerializeField]
    GameObject locus;
    [SerializeField]
    TextMeshProUGUI locusHealthText;
    [SerializeField]
    GameObject failureScreen;
    [SerializeField]
    TextMeshProUGUI failureText;

    public static bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        locusHealthText.text = locus.GetComponent<Entity>().currentHealth+"/"+locus.GetComponent<Entity>().maxHealth;
        if(locus.GetComponent<Entity>().currentHealth<50)
        {
            locusHealthText.color = Color.red;
            locus.transform.GetChild(0).gameObject.SetActive(false);
            locus.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            locusHealthText.color = Color.white;
            locus.transform.GetChild(0).gameObject.SetActive(true);
            locus.transform.GetChild(1).gameObject.SetActive(false);
        }
        if(locus.GetComponent<Entity>().currentHealth<=0 && gameOver == false)
        {
            gameOver = true;
            Time.timeScale = 0.1f;
            failureScreen.SetActive(true);
            failureText.gameObject.SetActive(true);
            StartCoroutine(fadeInFailText());
            locusHealthText.gameObject.SetActive(false);
        }
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
    }
}
