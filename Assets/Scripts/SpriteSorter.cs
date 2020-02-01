using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// set the sort order of the first found sprite based on the center of the first found collider
public class SpriteSorter : MonoBehaviour
{
    SpriteRenderer sprite;
    Collider2D mainCollider;

    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        mainCollider = GetComponentInChildren<Collider2D>();
    }

    void LateUpdate()
    {
        sprite.sortingOrder = -(int)(mainCollider.transform.TransformPoint(mainCollider.offset).y * 100);
    }
}
