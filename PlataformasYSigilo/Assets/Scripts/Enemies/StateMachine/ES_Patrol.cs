using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Patrol : EnemyState
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float patrolVelocity;

    private Vector3 destinyPoint;
    private int currentIndex = 0;
    private Vector2 directionToDestinyPoint;
    

    public override void OnEnterState(EnemyController controller)
    {
        base.OnEnterState(controller);
        

        destinyPoint = waypoints[currentIndex].position;
        directionToDestinyPoint = myController.GetDirection(transform.position, destinyPoint);
        //Debug.Log(directionToDestinyPoint);
        //StartCoroutine(WalkSideToSide());
        FaceToDestiny();
    }

    public override void OnUpdateState()
    {
        //Debug.Log(Vector3.Distance(transform.position, destinyPoint));
        if (Vector3.Distance(transform.position, destinyPoint) > 0.3)
        {
            //transform.position = Vector3.MoveTowards(transform.position, destinyPoint, patrolVelocity * Time.deltaTime);
            myController.rigidbody.velocity = directionToDestinyPoint * patrolVelocity;


        }
        else
        {
            //Debug.Log("LLegó");
            SetNewDestiny();
        }
    }


    public override void OnExitState()
    {
        base.OnExitState();
        //StopAllCoroutines();
        
    }

    /*private IEnumerator WalkSideToSide()
    {
        FaceToDestiny();
        while (true)
        {
            while (transform.position != destinyPoint)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinyPoint, patrolVelocity * Time.deltaTime);
                yield return null;
            }

            SetNewDestiny();
        }
    }*/

    private void SetNewDestiny()
    {
        currentIndex++;
        if (currentIndex >= waypoints.Length) currentIndex = 0;

        destinyPoint = waypoints[currentIndex].position;
        directionToDestinyPoint = myController.GetDirection(transform.position, destinyPoint);
        FaceToDestiny();
    }

    private void FaceToDestiny()
    {
        if (destinyPoint.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void TriggerEnter(Collider2D collision)
    {
        if (collision.CompareTag("PlayerDeteccion"))
        {
            
            if (!collision.transform.parent.gameObject.TryGetComponent(out HidePlayer hidePlayer1)
                || hidePlayer1.hideState != HideState.Hidden)
            {
                myController.pursue.objective = collision.transform;
                myController.ChangeState(myController.pursue);
            }
        }
    }


}
