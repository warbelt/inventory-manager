using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CraftingSpot : MonoBehaviour, IInteractableStation
{
    // REFERENCES
    [SerializeField] ProgressBar progressBar = null;
    [SerializeField] ParticleSystem particles = null;
    [SerializeField] List<CraftingRecipe> craftingRecipes;
    public Action<float> OnProgressChanged;
    
    // STATE
    [SerializeField] private List<CraftingMaterialID> currentMaterials;
    private bool crafting;
    private float craftingStartTime;
    private float craftingEndTime;
    private float progressPct;
    public bool isFull
    {
        get { return progressPct == 1; }
    }


    private void Awake()
    {
        progressPct = 0;
        currentMaterials = new List<CraftingMaterialID>();
        crafting = false;        
    }

    private void Start()
    {
        if (progressBar != null)
        {
            HookProgressBar();
        }
    }

    private void Update()
    {
        if (crafting)
        {
            progressPct = Mathf.InverseLerp(craftingStartTime, craftingEndTime, Time.time);
            OnProgressChanged(progressPct);
        }
    }

    public void Interact(PlayerController player)
    {
        if (crafting) return;

        Transform playerHeldItem = player.GetHeldItem();

        if (playerHeldItem.TryGetComponent(out CraftingMaterial mat))
        {
            AddMaterial(mat);
            Destroy(mat.gameObject);
        }
        else if (playerHeldItem.TryGetComponent(out Vessel vessel))
        {
            if(vessel.fillAmount == 0)
            {
                vessel.fill(100);
                progressPct = 0;
                OnProgressChanged(progressPct);
            }
        }
    }

    private void AddMaterial(CraftingMaterial addedMat)
    {
        currentMaterials.Add(addedMat.getMaterialId());
        Destroy(addedMat);

        if(currentMaterials.Count >= 2)
        {
            MixMaterials();
            currentMaterials = new List<CraftingMaterialID>();
        }
    }

    private void MixMaterials()
    {
        foreach(CraftingRecipe recipe in craftingRecipes)
        {
            if (recipe.CheckIfCraftable(currentMaterials[0], currentMaterials[1]))
            {
                StartCoroutine(startCrafting(recipe));
                return;
            }
        }
    }

    private IEnumerator startCrafting(CraftingRecipe recipe)
    {
        crafting = true;
        progressPct = 0;
        craftingStartTime = Time.time;
        craftingEndTime = craftingStartTime + recipe.craftingTime;

        yield return new WaitForSeconds(recipe.craftingTime);
        finishCrafting(recipe);
    }

    private void finishCrafting(CraftingRecipe recipe)
    {
        //Instantiate(recipe.result);

        progressPct = 1;
        OnProgressChanged(progressPct);
        crafting = false;

        if (particles != null)
        {
            particles.Emit(25);
        }
    }

    private void HookProgressBar()
    {
        OnProgressChanged += progressBar.progressChanged;
    }

    private void empty()
    {
        progressPct = 0;
    }
}
