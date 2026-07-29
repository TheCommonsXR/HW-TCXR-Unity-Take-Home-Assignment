using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public int damage = 1;

    // spriteRenderer.flipX
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        // Make flipX random bool to avoid repetition
        sr.flipX = Random.value < 0.5f;
    }
}
