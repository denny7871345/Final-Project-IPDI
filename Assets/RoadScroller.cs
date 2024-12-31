using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScroller : MonoBehaviour
{
    private bool work;
    // Start is called before the first frame update
    void Start()
    {
        work = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (work)
        {
            gameObject.transform.position += Vector3.back * 0.02f;
            if (gameObject.transform.position.z <= -10)
            {
                gameObject.transform.position += Vector3.forward * 50;
            }
        }
        
    }

    public void Work(bool _work)
    {
        work = _work;
    }
}
