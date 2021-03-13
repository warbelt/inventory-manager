using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting Recipe Data", menuName = "Crafting/Crafting Recipe Data", order = 2)]
public class CraftingRecipe : ScriptableObject
{
    public CraftingMaterialID material1;
    public CraftingMaterialID material2;
    public float craftingTime;
    public GameObject result;

    public bool CheckIfCraftable(CraftingMaterialID usedMat1, CraftingMaterialID usedMat2)
    {
        return (usedMat1 == material1 && usedMat2 == material2) || (usedMat2 == material1 && usedMat1 == material2);
    }
}
