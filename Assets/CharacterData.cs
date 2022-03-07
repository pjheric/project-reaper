using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
[CreateAssetMenu(fileName = "Character Data", menuName = "ScriptableObjects/CharacterData", order=1)]
public class CharacterData : ScriptableObject
{
    public Sprite charName; //Name of Reaper
    public string charSubname; //Subname of Reaper
    public Sprite charFullImage; //Full illustration
    public Sprite charIngameImage; //In game sprite
    public string charBio; //Biography
    public string[] charSkillDescription; //Skill description.
                                          //0 = passive, 1 = basic, 2 = special
    public GameObject charPrefab; //Prefab of the Reaper
}