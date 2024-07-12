using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill
{
    [Header("Dash")]
    [SerializeField] private UI_SkillTreeSlot dashUnlockButton;
    public bool dashUnlocked { get; private set; }
    public override void UseSkill()
    {
        base.UseSkill();        
    }

    protected override void Start()
    {
        base.Start();

        dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
    }

    protected override void CheckUnlock()
    {
        UnlockDash();        
    }

    private void UnlockDash()
    {
        if (dashUnlockButton.unlocked)
        {
            dashUnlocked = true;
        }
    }    
}
