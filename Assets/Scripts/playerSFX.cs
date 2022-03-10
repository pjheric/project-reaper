using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "playerSFX", menuName = "ScriptableObjects/playerSFX", order = 1)]
public class playerSFX : ScriptableObject
{
    public AudioClip attack;
    public AudioClip special;
    public AudioClip respawn;
    public GameObject audioPrefab;
}
