using UnityEngine;

public class Boss : Breakable
{
    public float specialAttackCooldown = 10f; // �S������N�o�ɶ�
    //private float specialAttackTimer = 0f;

    private void Start()
    {
        objName = "�]��";
        health = 500f;          // �]���֦���h�ͩR��
        
    }

    public void SpecialAttack()
    {
        Debug.Log($"{objName} ����S��ޯ�A�y�����q�ˮ`�I");
        // �K�[�S��ޯ��޿�A�Ҧp�d������αj�O�ޯ�
    }

    public override void LeaveBuff()
    {

    }
}
