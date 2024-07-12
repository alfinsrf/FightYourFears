using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    
    public Dash_Skill dash { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        dash = GetComponent<Dash_Skill>();
    }    
}
