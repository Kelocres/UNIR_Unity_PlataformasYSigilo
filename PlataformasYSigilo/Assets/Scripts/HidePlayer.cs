using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HideState
{
    Visible,
    HideOutFound,
    Hidden
}

public class HidePlayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    //Para comprobar si el jugador está visible, escondido o se puede esconder
    public HideState hideState;

    private float inputV;
    private float inputH;

    [SerializeField] private Color colorForHidden;
    private SpriteEffects spriteEffects;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hideState = HideState.Visible;
        spriteEffects = GetComponent<SpriteEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        inputV = Input.GetAxisRaw("Vertical");
        inputH = Input.GetAxisRaw("Horizontal");

        //Para salir del escondite
        if (inputH != 0 && hideState == HideState.Hidden)
        {
            hideState = HideState.HideOutFound;
            spriteRenderer.sortingOrder = 3;

            if (spriteEffects != null) spriteEffects.SetNormalColor();
        }
        else if (inputV == -1 && hideState == HideState.HideOutFound)
        {
            hideState = HideState.Hidden;
            spriteRenderer.sortingOrder = 1;

            if (spriteEffects != null) spriteEffects.SetNewColor(colorForHidden);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escondite"))
        {
            hideState = HideState.HideOutFound;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escondite"))
        {
            hideState = HideState.Visible;
        }
    }

}
