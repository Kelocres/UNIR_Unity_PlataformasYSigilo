using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Sprite solidSprite;
    [SerializeField] private Color solidColor;
    [SerializeField] private Sprite unsolidSprite;
    [SerializeField] private Color unsolidColor;

    private new Collider2D collider;
    //private bool isSolid = true;

    //public delegate void DelChangeState();
    //public event DelChangeState delChangeState;

    private SpriteEffects spriteEffects;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        spriteEffects = GetComponent<SpriteEffects>();
    }

    // Update is called once per frame
    public void ChangeState(bool newState)
    { 
        if(collider != null)
            collider.enabled = newState;

        if (spriteEffects == null) return;
        if (newState)
        {
            spriteEffects.SetNewColor(solidColor);
            spriteEffects.SetNewSprite(solidSprite);
        }
        else
        {
            spriteEffects.SetNewColor(unsolidColor);
            spriteEffects.SetNewSprite(unsolidSprite);
        }
        
    }

}
