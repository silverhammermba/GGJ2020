using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// set the sort order of the first found sprite based on the center of the first found collider
public class SpriteSorter : MonoBehaviour
{
    public SpriteRenderer shadow;

    SpriteRenderer[] sprites;
    Collider2D mainCollider;

    void Awake()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        mainCollider = GetComponentInChildren<Collider2D>();
    }

    void LateUpdate()
    {
        for (int i = 0; i < sprites.Length; ++i)
        {
            sprites[i].sortingOrder = -(int)(mainCollider.transform.TransformPoint(mainCollider.offset).y * 100);
        }
        if (shadow)
        {
            shadow.sortingOrder -= 1;
        }
    }
}
