using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Pursue : EnemyState
{
    [HideInInspector] public Transform objective;
    [SerializeField] private float pursueVelocity = 6f;
    [SerializeField] private float distanceToAttack = 0.5f;

   
    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
              
        if (objective == null)
            Debug.Log("objective == NULL!!");
        //else
          //  StartCoroutine(PursueObjective());
    }

    /*public override void OnExitState()
    {
        
    }*/

    public override void OnUpdateState()
    {
        if (objective == null)
        {
            Debug.Log("Player is dead for some reason");
            myController.ChangeState(myController.patrol);
        }

        FaceToDestiny();
        float distance = Vector2.Distance(transform.position, objective.position);
        if (distance > distanceToAttack)
        {
            //transform.position = Vector3.MoveTowards(transform.position, objective.position, pursueVelocity * Time.deltaTime);
            myController.rigidbody.velocity = myController.GetDirection(transform.position, objective.position) * pursueVelocity;

        }
        else
        {
            Debug.Log("Ready to Attack");
            myController.ChangeState(myController.attack);
        }
    }

    private void FaceToDestiny()
    {
        if (objective.position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    /*private IEnumerator PursueObjective()
    {
        Debug.Log("Pursue objective");
        float distance = Vector2.Distance(transform.position, objective.position);
        
        while (true)
        {
            while (distance > distanceToAttack)
            {
                transform.position = Vector3.MoveTowards(transform.position, objective.position, pursueVelocity * Time.deltaTime);
                yield return null;
                distance = Vector2.Distance(transform.position, objective.position);
                //Debug.Log("Distance = " + distance);
            }

            Debug.Log("Ready to Attack");
        }
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
