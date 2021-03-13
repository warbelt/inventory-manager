using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting Material Data", menuName = "Crafting/Crafting Material Data", order = 1)]
public class CraftingMaterialData : ScriptableObject
{
    public Sprite materialSprite;
    public CraftingMaterialID materialID;
}
