using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public BuffDoor leftBuff;
    public BuffDoor rightBuff;

    [SerializeField]
    private Collider leftRigid;
    [SerializeField]
    private Collider rightRigid;

    private void Start()
    {

        if(leftRigid == null)
        {
            Debug.Log("LeftRigid Error");
        }

        if (rightRigid == null)
        {
            Debug.Log("rightRigid Error");
        }

    }

    private void Update()
    {
        if (leftRigid == null)
        {
            rightBuff.Die();
            Destroy(gameObject);
        }

        if (rightRigid == null)
        {
            leftBuff.Die();
            Destroy(gameObject);
        }

    }



}
