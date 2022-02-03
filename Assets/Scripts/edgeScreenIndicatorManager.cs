using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edgeScreenIndicatorManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject locus;
    [SerializeField] GameObject locusIndicator;
    [SerializeField] Camera playerCamera;
    [SerializeField] int playerNum;//1 or 2
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = playerCamera.WorldToScreenPoint(locus.transform.position).x;
        float y = playerCamera.WorldToScreenPoint(locus.transform.position).y;
        //print(Screen.width+", "+ Screen.height+" ?----? "+x.x+", "+ x.y);
        bool isOffScreen = false;
        if(y> Screen.height*1.018f || y<  -1*Screen.height*0.73f || (playerNum == 1 && (x> Screen.width*0.54f || x < -Screen.width*0.07938f)) || (playerNum == 2 && x>Screen.width*1.0282f || playerNum == 2 && x< Screen.width*0.46f))
        {
            locusIndicator.SetActive(true);
            if(playerNum == 1)
            {
                locusIndicator.transform.position = new Vector2(Mathf.Clamp(x, Screen.width * 0.03125f, (Screen.width/2)*0.9375f), Mathf.Clamp(y,Screen.height * 0.07407f,Screen.height * 0.9259f));
            }
            if(playerNum == 2)
            {
                locusIndicator.transform.position = new Vector2(Mathf.Clamp(x, Screen.width/2 + Screen.width * 0.03125f, Screen.width/2 + (Screen.width/2)*0.9375f), Mathf.Clamp(y,Screen.height * 0.07407f,Screen.height * 0.9259f));
            }
            isOffScreen = true;
        }
        else
        {      
            locusIndicator.SetActive(false);
        }
       


        // if ((locus.transform.position.x > player.transform.position.x + width / 4 || locus.transform.position.x < player.transform.position.x - width / 2) || (locus.transform.position.y > player.transform.position.y + height / 2 || locus.transform.position.y < player.transform.position.y - height / 2))
        // {
        //     float x = -1000;//pos on the screen edge
        //     float y = -1000;
        //     float angle = 0;// up 0, left 90, down, 180, right 270

        //     //spawn(only if new though) and move indicator
        //     if (Mathf.Abs((locus.transform.position.y - player.transform.position.y) / (locus.transform.position.x - player.transform.position.x)) > height / width)
        //     {
        //         if (locus.transform.position.y < player.transform.position.y)
        //         {
        //             y = player.transform.position.y - height / 2;
        //             angle = 180;

        //         }
        //         else
        //         {
        //             y = player.transform.position.y + height / 2;
        //             angle = 0;
        //         }
        //     }
        //     else
        //     {
        //         if (locus.transform.position.x < player.transform.position.x)
        //         {
        //             x = player.transform.position.x - width / 2;
        //             angle = 90;

        //         }
        //         else
        //         {
        //             x = player.transform.position.x + width / 2;
        //             angle = 270;
        //         }
        //     }
        //     if (x == -1000)
        //     {
        //         x = (((y - player.transform.position.y) * (locus.transform.position.x - player.transform.position.x)) / (locus.transform.position.y - player.transform.position.y)) + player.transform.position.x;
        //         //the y is locked x must be determined
        //     }
        //     else if (y == -1000)
        //     {
        //         y = ((locus.transform.position.y - player.transform.position.y) / (locus.transform.position.x - player.transform.position.x)) * (x - player.transform.position.x) + player.transform.position.y;
        //     }
        //     locusIndicator.GetComponent<Image>().color = new Color(locusIndicator.GetComponent<Image>().color.r, locusIndicator.GetComponent<Image>().color.g, locusIndicator.GetComponent<Image>().color.b, 0.5f);
        //     locusIndicator.transform.position = Camera.main.WorldToScreenPoint(new Vector2(x, y));
        //     locusIndicator.transform.rotation = Quaternion.Euler(0, 0, angle);
        //     locusIndicator.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, 0);
        //     locusIndicator.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Vector3.Distance(locus.transform.position, player.transform.position).ToString("F0");
        //     locusIndicator.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(locusIndicator.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color.r, locusIndicator.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color.g, locusIndicator.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color.b, 0.5f);
        //     locusIndicator.SetActive(true);
        // }
    }
}
