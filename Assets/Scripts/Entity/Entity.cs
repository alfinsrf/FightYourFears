using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    public CharacterStats stats { get; private set; }
    #endregion
    
    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackPower;
    [SerializeField] protected Vector2 knockbackOffset;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;

    public int knockbackDir {  get; private set; }
    
    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;
    public System.Action onFlipped;

    protected virtual void Awake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        cd = GetComponent<CapsuleCollider2D>();
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void SlowEntityBy(float _slowPercentage, float _slowDuration)
    {

    }

    protected virtual void ReturnDefaultSpeed() 
    {
        anim.speed = 1;
    }

    public virtual void DamageImpact() => StartCoroutine("HitKnockBack");

    public virtual void SetupKnockbackDir(Transform _damageDirection)
    {
        if(_damageDirection.position.x > transform.position.x)
        {
            knockbackDir = -1;
        }
        else if (_damageDirection.position.x < transform.position.x)
        {
            knockbackDir = 1;
        }
    }

    public void SetupKnockbackPower(Vector2 _knockbackPower)
    {
        knockbackPower = _knockbackPower;
    }

    protected virtual IEnumerator HitKnockBack()
    {
        isKnocked = true;

        float xOffset = Random.Range(knockbackOffset.x, knockbackOffset.y);

        if(knockbackPower.x > 0 || knockbackPower.y > 0)
        {
            rb.velocity = new Vector2((knockbackPower.x + xOffset) * knockbackDir, (knockbackPower.y + xOffset) * knockbackDir);
        }

        yield return new WaitForSeconds(knockbackDuration);

        isKnocked = false;
        SetupZeroKnockbackPower();
    }

    protected virtual void SetupZeroKnockbackPower()
    {

    }


    #region Velocity
    public virtual void SetZeroVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);        
        FlipController(_xVelocity);
    }
    #endregion    

    #region Flip

    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(onFlipped != null)
        {
            onFlipped();
        }
    }

    public virtual void FlipController(float _x)
    {
        if(_x > 0 && !facingRight)
        {
            Flip();
        }
        else if(_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion

    public virtual void Die()
    {

    }
}
