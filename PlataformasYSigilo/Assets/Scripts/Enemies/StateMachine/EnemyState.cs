using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    //[HideInInspector]
    protected EnemyController myController;
    [SerializeField] protected string animAtribute;
    [SerializeField] private bool animIsBool = true;
    private bool triggerIsLaunched;

    [SerializeField] private Collider2D triggerForm;
    private bool triggerIsActive = false;

    public virtual void OnEnterState(EnemyController controller)
    {
        myController = controller;
        triggerIsLaunched = false;

        if(triggerForm!=null)
        {
            triggerForm.enabled = true;
            triggerIsActive = true;
        }
        StartAnimation();
    }

    public bool AnimAtributeIsValid()
    {
        return animAtribute != null && animAtribute != "";
    }

    public virtual void StartAnimation()
    {
        if (myController.anim == null || !AnimAtributeIsValid())
            return;

        if (animIsBool)
            myController.anim.SetBool(animAtribute, true);
        else if (!triggerIsLaunched)
        {
            triggerIsLaunched = true;
            myController.anim.SetTrigger(animAtribute);
        }
    }

    public virtual void EndAnimation()
    {
        if (myController.anim != null && AnimAtributeIsValid() && animIsBool)
            myController.anim.SetBool(animAtribute, false);

        triggerIsLaunched = false;
    }

    public abstract void OnUpdateState();

    public virtual void OnExitState()
    {
        if (triggerForm != null)
        {
            triggerIsActive = false;
            triggerForm.enabled = false;
        }
        EndAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggerIsActive) TriggerEnter(collision);
    }

    protected virtual void TriggerEnter(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerIsActive) TriggerExit(collision);
    }

    protected virtual void TriggerExit(Collider2D collision)
    {

    }
}
