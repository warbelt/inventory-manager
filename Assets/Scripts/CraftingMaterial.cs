using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CraftingMaterial : MonoBehaviour, IPickable
{
    [SerializeField] CraftingMaterialData _materialData;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Rigidbody2D _rb2d;

    private bool activated = false;

    private void Awake()
    {
        _renderer.sprite = _materialData.materialSprite;
        activated = true;
    }

    public CraftingMaterialID getMaterialId()
    {
        return _materialData.materialID;
    }

    public Transform PickUp()
    {
        Transform returnTransform = null;

        if (activated)
        {
            activated = false;
            _rb2d.isKinematic = true;
            returnTransform = transform;
        }
        return returnTransform;
    }

    public void Drop()
    {
        activated = true;
        _rb2d.isKinematic = false;
        transform.parent = null;
    }
}
