using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour
{
    // Start is called before the first frame update
    MaterialPropertyBlock mpb;
    MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null) mpb = new MaterialPropertyBlock();
            return mpb;
        }
    }

    readonly int shPropColor = Shader.PropertyToID("_Color");
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float timeForTemporalEffects = 0.2f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetNormalColor()
    {
        Mpb.SetColor(shPropColor, Color.white);
        spriteRenderer.SetPropertyBlock(Mpb);
    }

    public void SetNewColor(Color newColor)
    {
        Mpb.SetColor(shPropColor, newColor);
        spriteRenderer.SetPropertyBlock(Mpb);
    }

    public void SetNewSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }

    public void SetTemporalColor(Color newColor)
    {
        StartCoroutine(TemporalColor(newColor));
    }

    IEnumerator TemporalColor( Color newColor)
    {
        SetNewColor(newColor);
        yield return new WaitForSeconds(timeForTemporalEffects);
        SetNormalColor();
    }
}
