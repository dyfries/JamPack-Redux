using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CollectableType", menuName = "ScriptableObjects/Collectable Type", order = 1)]
public class CollectableTypeSO : ScriptableObject
{
    public string type;
    public string name;
    public Sprite sprite;
}
