using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Death : EnemyState
{
    //[SerializeField] private int collisionDamage = 20;

    public override void StartAnimation()
    {
        if (myController.anim != null)
            myController.anim.SetTrigger(animAtribute);
    }

    public override void OnUpdateState()
    {
        
    }

    public void DeathAnimationIsFinished()
    {
        Destroy(gameObject);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si la animaci�n de muerte hace da�o
        if(collisionDamage >0 
            && collision.CompareTag("PlayerHitbox") 
            && collision.gameObject.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
        {
            healthSystem.GetDamage(collisionDamage);
        }
    }*/


}
