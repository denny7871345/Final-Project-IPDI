using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : InputManager
{
    private Vector3 axis;
    private bool trigger;
    
    protected override void CalculateDpadAxis()
    {
        axis = Vector3.zero;
        
        if (Input.GetKey("d"))
        {
            axis += transform.right * 1.0f;
        }
        if (Input.GetKey("a"))
        {
            axis += transform.right * -1.0f;
        }
        
        evtDpadAxis?.Invoke(axis);
    }

    protected override void PostProcessDpadAxis()
    {
        
    }

    protected override void CalculateTrigger()
    {
        trigger = Input.GetKeyDown("space");
        evtTrigger?.Invoke(trigger);
    }

}
