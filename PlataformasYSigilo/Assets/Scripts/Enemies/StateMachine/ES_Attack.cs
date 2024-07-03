using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Attack : EnemyState
{

    [SerializeField] private Transform overlapCenter;
    [SerializeField] private float overlapRadius = 0.5f;
    [SerializeField] private int damagePoints = 10;

    //private Vector2 attackPosition;

    // Layer para detectar los triggers afectables
    [SerializeField] private LayerMask whatCanBeHit;

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        myController.rigidbody.velocity = Vector2.zero;
        

    }

    /*public override void StartAnimation()
    {
        if (myController.anim != null)
            myController.anim.SetTrigger(animAtribute);
    }*/

    public virtual void OverlapAttack()
    {
        if (overlapCenter == null)
        {
            Debug.Log("overlapCenter == null");
            return;
        }

        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(overlapCenter.position, overlapRadius, whatCanBeHit);
        foreach (Collider2D collider in collidersTocados)
        {
            if (collider.gameObject!= gameObject 
                && collider.gameObject.TryGetComponent<HealthSystem>(out HealthSystem healthSystem))
            {
                healthSystem.GetDamage(damagePoints);
            }
        }

    }

    public void AttackAnimationIsFinished()
    {
        myController.ChangeState(myController.pursue);
    }

    public override void OnUpdateState()
    {
        //transform.position = attackPosition;
    }

    /*public override void OnExitState()
    {

    }*/

    protected override void TriggerExit(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDeteccion"))
        {
            Debug.Log("Player Fuera de la vista");
            //objective = null;
            myController.ChangeState(myController.patrol);
        }
    }
}
