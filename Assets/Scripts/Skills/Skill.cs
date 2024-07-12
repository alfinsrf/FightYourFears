using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldown;
    [HideInInspector] public float cooldownTimer;

    protected Player player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = PlayerManager.instance.player;

        CheckUnlock();

        cooldownTimer = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;

        CheckUnlock();
    }

    protected virtual void CheckUnlock()
    {

    }

    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;

            return true;
        }

        player.fx.CreatePopUpText("Skill on cooldown");
        
        return false;
    }

    public virtual void UseSkill()
    {

    }

    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

        float closesDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);

                if (distanceToEnemy < closesDistance)
                {
                    closesDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        return closestEnemy;
    }
}
