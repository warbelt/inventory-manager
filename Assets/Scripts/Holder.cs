using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : MonoBehaviour, IPickable, IInteractableStation
{
    [SerializeField] private Transform _heldItem = null;
    public bool isHolding
    {
        get
        {
            return _heldItem != null;
        }
    }

    private void Awake()
    {
    }

    public void PlaceItem(Transform go)
    {
        go.transform.parent = transform;
        go.transform.localPosition = new Vector3(0, 0, 0);
        _heldItem = go;
    }

    public Transform PickUp()
    {
        Transform itemToPickUp = null;

        if (isHolding)
        {
            itemToPickUp = _heldItem;
            _heldItem = null;
        }

        return itemToPickUp;
    }

    public void Interact(PlayerController player)
    {
        if (!isHolding)
        {
            Transform item = player.GetHeldItem();
            if (item != null)
            {
                PlaceItem(item);
            }
        }
    }

    public void Drop() { }
}
