using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickable
{
    Transform PickUp();
    void Drop();
}
