using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IHitableElement
{
    
    [SerializeField] private float healthPoints;

    //Delegate del script principal para cuando se muera
    public delegate void DelIsDead();
    public event DelIsDead delIsDead;

    //Delegate para visualizar la vida
    public delegate void DelSetLife(float health);
    public event DelSetLife delSetLife;

    private SpriteEffects spriteEffects;

    private void Start()
    {
        spriteEffects = GetComponent<SpriteEffects>();
    }

    public void GetDamage(float damage)
    {
        Debug.Log("Damage: " + damage);
        healthPoints -= damage;

        // Para visualizar la vida
        if (delSetLife != null) delSetLife(healthPoints);

        Debug.Log("Después del SetLife()");

        if (spriteEffects != null) spriteEffects.SetTemporalColor(Color.red);
        if (healthPoints <= 0)
        {
            Debug.Log("Muerte");
            //Si el delegate está asignado, el script principal manejará la muerte
            if (delIsDead != null) delIsDead();

            //Si no está asignado, se destruye el objeto
            else
                //Destroy(this.gameObject);
                gameObject.SetActive(false);
        }
    }

    public void InstaDeath()
    {
        GetDamage(healthPoints);
    }
}
