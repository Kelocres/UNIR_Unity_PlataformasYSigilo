using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVictory : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<Player>() || collider.transform.parent.GetComponent<Player>())
            GameManager.instance.PlayerHasWon();

    }
}
