using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edgeScreenIndicatorManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    public GameObject locus;
    public GameObject locusIndicator;
    public GameObject otherPlayer;
    public GameObject otherPlayerIndicator;
    [SerializeField] Camera playerCamera;
    [SerializeField] int playerNum;//1 or 2
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistentData.isGameStarted)
        {
            
            //print(Screen.width+", "+ Screen.height+" ?----? "+x.x+", "+ x.y);



            //LOCUS INDICATOR
            updateIndicator(locus,locusIndicator);

            //PLAYER INDICATOR
            updateIndicator(otherPlayer,otherPlayerIndicator);

        }
    }
    void updateIndicator(GameObject target, GameObject indicator)
    {
        float x = playerCamera.WorldToScreenPoint(target.transform.position).x;
        float y = playerCamera.WorldToScreenPoint(target.transform.position).y;
        if(Vector3.Distance(target.transform.position, player.transform.position)> 60 && target.transform.position.x > player.transform.position.x)
        {
            x = 100000;
        }
        if(Vector3.Distance(target.transform.position, player.transform.position)> 60 && target.transform.position.x < player.transform.position.x)
        {
            x = -100000;
        }
        if(Vector3.Distance(target.transform.position, player.transform.position)> 60 && target.transform.position.y > player.transform.position.y)
        {
            y = 100000;
        }
        if(Vector3.Distance(target.transform.position, player.transform.position)> 60 && target.transform.position.y < player.transform.position.y)
        {
            y = -100000;
        }

        if (y > Screen.height * 1.018f || y < -1 * Screen.height * 0.73f || (playerNum == 1 && (x > Screen.width * 0.54f || x < -Screen.width * 0.07938f)) || (playerNum == 2 && x > Screen.width * 1.0282f || playerNum == 2 && x < Screen.width * 0.46f))
        {
            indicator.SetActive(true);
            if (playerNum == 1)
            {
                indicator.transform.position = new Vector2(Mathf.Clamp(x, Screen.width * 0.03125f, (Screen.width / 2) * 0.9375f), Mathf.Clamp(y, Screen.height * 0.07407f, Screen.height * 0.9259f));
            }
            if (playerNum == 2)
            {
                
                indicator.transform.position = new Vector2(Mathf.Clamp(x, Screen.width / 2 + Screen.width * 0.03125f, Screen.width / 2 + (Screen.width / 2) * 0.9375f), Mathf.Clamp(y, Screen.height * 0.07407f, Screen.height * 0.9259f));
            }
        }
        else
        {
            indicator.SetActive(false);
        }
    }
}
