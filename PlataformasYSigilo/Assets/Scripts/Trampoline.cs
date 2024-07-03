using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float jumpForce = 10f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("Trampolin");
        if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        else
            collider.transform.parent.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
