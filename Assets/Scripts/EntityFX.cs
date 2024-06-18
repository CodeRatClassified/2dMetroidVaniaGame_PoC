using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [Header("Flash FX")]
    [SerializeField] private float flashFXDuration;
    [SerializeField] private Material hitEffectMaterial;
    private Material originalMaterial;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    private IEnumerator FlashFX()
    {
        spriteRenderer.material = hitEffectMaterial;

        yield return new WaitForSeconds(flashFXDuration);

        spriteRenderer.material = originalMaterial;

    }

    private void ColorBlink()
    {
        if (spriteRenderer.color != Color.white)
            spriteRenderer.color = Color.white;
        else
            spriteRenderer.color = Color.black;
    }

    private void CancelBlink()
    {
        CancelInvoke();
        spriteRenderer.color = Color.white;
    }
}
