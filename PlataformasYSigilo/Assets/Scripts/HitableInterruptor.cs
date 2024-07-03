using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitableInterruptor : MonoBehaviour, IHitableElement
{
    [SerializeField] private Sprite stateTrue;
    [SerializeField] private Sprite stateFalse;
    [SerializeField] private Block[] blocks;

    private bool currentState = true;
    private SpriteEffects spriteEffects;

    // Start is called before the first frame update
    void Start()
    {
        spriteEffects = GetComponent<SpriteEffects>();
    }

    public void GetDamage(float damage)
    {
        currentState = !currentState;

        if (currentState) spriteEffects?.SetNewSprite(stateTrue);  
        else spriteEffects?.SetNewSprite(stateFalse);

        if (blocks.Length != 0)
            foreach (Block block in blocks)
                block.ChangeState(currentState);

    }

}
