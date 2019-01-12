using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerBaseState
{
    void HandleInput();
    void Update();
    void FixedUpdate();
}