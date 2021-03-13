using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : MonoBehaviour, IPickable
{
    [System.Serializable]
    struct fillProgressThreshold
    {
        [Range(0, 100)]
        public int fillAmount;
        public Sprite fillSprite;
    }

    // REFERENCES
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<fillProgressThreshold> fillProgressData = new List<fillProgressThreshold>();

    // STATE
    private bool activated = true;
    [Range(0, 100)]
    [SerializeField]
    private float _fillAmount;
    public float fillAmount
    {
        get { return _fillAmount; }
        protected set { _fillAmount = value; }
    }


    private void Awake()
    {
        activated = true;
        updateSprite();
        fillAmount = 0;
    }

    private void OnValidate()
    {
        fillProgressData.Sort(
            (fillProgressThreshold a, fillProgressThreshold b) => a.fillAmount.CompareTo(b.fillAmount)
            );
    }

    public Transform PickUp()
    {
        Transform returnTransform = null;

        if (activated)
        {
            activated = false;
            returnTransform = transform;
        }
        return returnTransform;
    }

    public void Drop()
    {
        transform.parent = null;
        activated = true;
    }

    public void fill(float fillValue)
    {
        fillAmount = Mathf.Clamp(fillAmount + fillValue, 0, 100);
        updateSprite();
    }

    private void updateSprite()
    {
        Sprite newSprite = null;

        foreach (fillProgressThreshold threshold in fillProgressData)
        {
            if (threshold.fillAmount <= fillAmount)
            {
                newSprite = threshold.fillSprite;
            }
            else
            {
                break;
            }
        }

        _spriteRenderer.sprite = newSprite;
    }
}
