using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // REFERENCES
    [SerializeField] private float _movementForce;
    [SerializeField] private float _boostMovementForce;
    [SerializeField] private Rigidbody2D _rb;

    // INPUT
    private float hMove, vMove;
    private bool use, drop;
    private bool boost;

    // STATE
    [SerializeField] private float boostCooldown;
    private float boostReady;
    [SerializeField] Transform _heldItem = null;
    public bool isHolding
    {
        get
        {
            return _heldItem != null;
        }
    }
    private int pocketCoins;

    private void Awake()
    {
        _heldItem = null;
        boost = false;
        pocketCoins = 0;
        boostReady = Time.time;
    }

    private void Update()
    {
        hMove = Input.GetAxisRaw("Horizontal");
        vMove = Input.GetAxisRaw("Vertical");

        boost = boost | (Input.GetKeyDown(KeyCode.LeftShift) & (Time.time >= boostReady));

        use = Input.GetKeyDown(KeyCode.Space);
        drop = Input.GetKeyDown(KeyCode.F);

        if (use)
        {
            useItem();
        }
        else if(drop)
        {
            dropItem();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 movementVector = new Vector2(hMove, vMove);

        if(movementVector.magnitude > 1)
        {
            movementVector.Normalize();
        }

        var appliedForce = _movementForce;
        if (boost)
        {
            appliedForce += _boostMovementForce;
            boostReady = Time.time + boostCooldown;
            boost = false;
        }

        _rb.AddForce(movementVector * appliedForce);
    }

    private void useItem()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, 1);

        System.Array.Sort(collidersInRange,
            delegate (Collider2D col1, Collider2D col2) { return (col1.transform.position - transform.position).magnitude.CompareTo((col2.transform.position - transform.position).magnitude); }
            );

        foreach (Collider2D col in collidersInRange)
        {
            if(!isHolding && col.TryGetComponent<IPickable>(out var pickable))
            {
                print(pickable);

                Coin coin;
                if (col.TryGetComponent<Coin>(out coin))
                {
                    coin.PickUp();
                    pocketCoins += 1;
                    return;
                }

                var item = pickable.PickUp();

                if (item != null)
                {
                    item.parent = transform;
                    item.localPosition = new Vector3(0, 1f);
                    _heldItem = item;
                    return;
                }
            }

            else if (col.TryGetComponent<IInteractableStation>(out var station)) {
                print(station);

                station.Interact(this);

                if (_heldItem != null && _heldItem.parent != transform)
                {
                    _heldItem = null;
                    return;
                }
            }
        }
    }

    // TODO refactor
    void tryPickUpObject()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D col in collidersInRange)
        {
            IPickable pickable = col.GetComponent<IPickable>();
            if (pickable != null)
            {
                Coin coin;
                if (col.TryGetComponent<Coin>(out coin))
                {
                    coin.PickUp();
                    pocketCoins += 1;
                    return;
                }

                var item = pickable.PickUp();

                if (item != null)
                {
                    item.parent = transform;
                    item.localPosition = new Vector3(0, 1f);
                    _heldItem = item;
                    return;
                }
            }
        }
    }

    void useObject()
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, 1);
        foreach (Collider2D col in collidersInRange)
        {
            IInteractableStation usable = col.GetComponent<IInteractableStation>();
            if (usable != null)
            {
                usable.Interact(this);


                if (_heldItem != null && _heldItem.parent != transform)
                {
                    _heldItem = null;
                    return;
                }
            }
        }
    }

    void dropItem()
    {
        if (isHolding)
        {
            IPickable pickable = _heldItem.GetComponent<IPickable>();
            if (pickable != null)
            {
                pickable.Drop();
            }

            _heldItem = null;
        }
    }

    public int GetCoins()
    {
        return pocketCoins;
    }

    public void ResetCoins()
    {
        pocketCoins = 0;
    }

    public Transform GetHeldItem()
    {
        if (_heldItem != null)
        {
            return _heldItem;
        }
        else
        {
            return null;
        }
    }
}
