using UnityEngine;

public class Boss : Breakable
{
    public float specialAttackCooldown = 10f; // 特殊攻擊冷卻時間
    //private float specialAttackTimer = 0f;

    private void Start()
    {
        objName = "魔王";
        health = 500f;          // 魔王擁有更多生命值
        
    }

    public void SpecialAttack()
    {
        Debug.Log($"{objName} 釋放特殊技能，造成巨量傷害！");
        // 添加特殊技能邏輯，例如範圍攻擊或強力技能
    }

    public override void LeaveBuff()
    {

    }
}
