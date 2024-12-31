using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadControll : MonoBehaviour
{
    [SerializeField]
    List<RoadScroller> roadScrollers;

    //.
    public void RoadWork(bool _work)
    {
        Debug.Log($"Road is being work:{_work}");
        foreach(RoadScroller token in roadScrollers)
        {
            token.Work(_work);
        }
    }

}
