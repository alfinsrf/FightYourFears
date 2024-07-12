using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Bullets : MonoBehaviour
{  
    private Player player => GameObject.Find("Player").GetComponent<Player>();
    public float distance;

    public GameObject bulletExplode;

    [SerializeField] private LayerMask whatIsEnemy;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    private void Update()
    {        
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsEnemy);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hitInfo.collider.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);
                    AudioManager.instance.PlaySFX(8, null);
                }
            }

            Instantiate(bulletExplode, transform.position, Quaternion.identity);
            Destroy(gameObject);            
        }
    }       
}
