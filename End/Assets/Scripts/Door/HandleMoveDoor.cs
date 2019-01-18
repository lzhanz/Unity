using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HandleMoveDoor : MonoBehaviour
{

    Tweener t;

    private void Start()
    {

        t = transform.DOLocalMoveY(4.44f, 4.0f);
   
    }
    




}
