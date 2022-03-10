using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "enemySFX", menuName = "ScriptableObjects/enemySFX", order = 1)]
public class enemySFX : ScriptableObject
{
    public AudioClip Attack;
    public AudioClip death;
    public GameObject audioPrefab;
}
