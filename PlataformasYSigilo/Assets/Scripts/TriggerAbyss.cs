using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAbyss : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out HealthSystem character))
            character.InstaDeath();
        else if (collider.transform.parent.TryGetComponent(out HealthSystem character2))
            character2.InstaDeath();

    }
}
