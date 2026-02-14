using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableType", menuName = "ScriptableObjects/Collectable Type", order = 1)]
public class CollectableTypeSO : ScriptableObject
{
    //This should be unique between all CollectibleTypeSO
    public string type;
    //If you use collectible UI title will be it's displayed name if you use the name text box 
    public string title;
    //This will be the render Sprite for the UI
    public Sprite sprite;
}
