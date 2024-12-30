using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class InputManager : MonoBehaviour
{
    public UnityEvent<Vector3> evtDpadAxis;
    public UnityEvent<bool> evtTrigger;

    protected abstract void CalculateDpadAxis();
    protected abstract void PostProcessDpadAxis();
    protected abstract void CalculateTrigger();
    // Update is called once per frame
    void Update()
    {
        CalculateDpadAxis();
        CalculateTrigger();
        PostProcessDpadAxis();
    }
}
