using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IPickable
{
    public Transform PickUp() {
        Destroy(gameObject);
        return transform;
    }

    public void Drop() { }
}
