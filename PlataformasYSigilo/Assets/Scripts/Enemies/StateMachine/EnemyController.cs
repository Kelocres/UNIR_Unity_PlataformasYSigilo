using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] protected float vidas;
    //[SerializeField] protected float puntosAtaque;

    [HideInInspector] public ES_Pursue pursue;
    [HideInInspector] public ES_Patrol patrol;
    [HideInInspector] public ES_Attack attack;
    [HideInInspector] public ES_Death death;

    EnemyState currentState;
    [HideInInspector] public Animator anim;
    [HideInInspector] public new Rigidbody2D rigidbody;
    //[HideInInspector] public EnemyMovement movement;

    private void Start()
    {
        pursue = GetComponent<ES_Pursue>();
        patrol = GetComponent<ES_Patrol>();
        attack = GetComponent<ES_Attack>();
        death = GetComponent<ES_Death>();

        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();

        if (TryGetComponent<HealthSystem>(out HealthSystem hs)
            && death !=null && death.AnimAtributeIsValid())
            hs.delIsDead += Death;

        ChangeState(patrol);
    }

    private void Update()
    {
        if (currentState != null)
            currentState.OnUpdateState();
    }

    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
            currentState.OnExitState();

        currentState = newState;
        currentState.OnEnterState(this);
    }

    public void Death()
    {

        ChangeState(death);
    }

    public  Vector2 GetDirection(Vector3 origin, Vector3 destiny)
    {
        return new Vector2(destiny.x - origin.x, destiny.y - origin.y).normalized;
    }

}
