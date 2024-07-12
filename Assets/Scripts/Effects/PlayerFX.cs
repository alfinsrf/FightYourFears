using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFX : EntityFX
{
    [Header("Dust Trail")]
    public ParticleSystem dustTrailFX;

    [Header("Screen Shake FX")]
    [SerializeField] private float shakeMultiplier;
    public Vector3 shakeWhenShoot;
    private CinemachineImpulseSource screenShake;

    [Header("After Image Fx")]
    [SerializeField] private float afterImageCooldown;
    [SerializeField] private GameObject afterImagePrefab;
    [SerializeField] private float colorLooseRate;
    private float afterImageCooldownTimer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        
        screenShake = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        afterImageCooldownTimer -= Time.deltaTime;
    }

    public void ScreenShake(Vector3 _shakePower)
    {
        screenShake.m_DefaultVelocity = new Vector3(_shakePower.x * player.facingDir, _shakePower.y) * shakeMultiplier;
        screenShake.GenerateImpulse();
    }

    public void CreateAfterImage()
    {
        if(afterImageCooldownTimer < 0)
        {
            afterImageCooldownTimer = afterImageCooldown;
            GameObject newAfterImage = Instantiate(afterImagePrefab, transform.position, transform.rotation);
            newAfterImage.GetComponent<AfterImageFX>().SetupAfterImage(colorLooseRate, sr.sprite);
        }
    }
}
