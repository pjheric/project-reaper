using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SFXRunner : MonoBehaviour
{
    [SerializeField] public AudioSource source;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        source.clip = clip;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(source.isPlaying == false)
        {
            kill();
        }
    }
    public void kill()
    {
        Destroy(this.gameObject);
    }
    public void killInX(float time)
    {
        Invoke("kill", time);
    }
}
