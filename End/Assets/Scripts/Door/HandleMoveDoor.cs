using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandleMoveDoor : MonoBehaviour
{

    Tweener t;
    private BoxCollider2D box;
    private void Start()
    {
        box = GetComponentInChildren<BoxCollider2D>();

        t = transform.DOLocalMoveY(4.44f, 4.0f).OnComplete(
            () => { box.enabled = true; });
   
    }
    




}
