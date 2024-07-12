using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{    
    public bool isBusy {  get; private set; }

    [Header("Movement Info")]
    public float moveSpeed;
    private float defaultMoveSpeed;

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDuration;
    private float defaultDashSpeed;
    public float dashDir { get; private set; }

    [Header("Guns Info")]
    public GameObject gunImage;
    public Transform gun;
    public float mouseOffset;
    public Transform shootPoint;
    public GameObject bulletPrefab;
    public float bulletForce;
    public Transform blastTransform;
    public GameObject blastEffect;

    public float timeBetweenShots;
    float nextShotTime;
    
    public SkillManager skill { get; private set; }    
    public PlayerFX fx { get; private set; }


    #region States    
    public PlayerStateMachine stateMachine { get; private set; }    

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        dashState = new PlayerDashState(this, stateMachine, "Move");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        fx = GetComponent<PlayerFX>();
        
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);
        
        defaultMoveSpeed = moveSpeed;
        defaultDashSpeed = dashSpeed;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        base.Update();

        stateMachine.currentState.Update();
        
        CheckForDashInput();        
        GunAndShoot();
    }


    public override void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1 - _slowPercentage);
        dashSpeed = dashSpeed * (1 - _slowDuration);
        anim.speed = anim.speed * (1 - _slowPercentage);

        Invoke("ReturnDefaultSpeed", _slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        dashSpeed = defaultDashSpeed;
    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (skill.dash.dashUnlocked == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if(dashDir == 0)
            {
                dashDir = facingDir;
            }
            
            stateMachine.ChangeState(dashState);
        }
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }    
    private void GunAndShoot()
    {        
        Vector3 displacement = gun.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(0f, 0f, angle + mouseOffset);
        SpriteRenderer gsr = gun.GetComponent<SpriteRenderer>();

        if (angle < 89 && angle > -89)
        {
            gsr.flipY = true;
        }
        else
        {
            gsr.flipY = false;
        }

        
        if (Input.GetButton("Fire"))
        {
            if(gunImage.activeInHierarchy == false)
            {
                return;
            }

            if (Time.time > nextShotTime)
            {
                AudioManager.instance.PlaySFX(1, transform);
                Instantiate(blastEffect, blastTransform.position, blastTransform.rotation);

                nextShotTime = Time.time + timeBetweenShots;
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.up * bulletForce, ForceMode2D.Impulse);
                fx.ScreenShake(fx.shakeWhenShoot);
            }
        }
    }
}
