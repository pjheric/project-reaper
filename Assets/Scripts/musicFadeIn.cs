using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class musicFadeIn : MonoBehaviour
{
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source.volume = 0.0f;
        StartCoroutine(musicFade());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator musicFade()
    {
        source.volume = 0.0f;
        float t = 0.0f;
        while(true)
        {
            yield return new WaitForFixedUpdate();
            t += Time.fixedUnscaledDeltaTime;
            source.volume = t;
            if(t > 0.40f)
            {
                break;
            }
        }
    }
}
