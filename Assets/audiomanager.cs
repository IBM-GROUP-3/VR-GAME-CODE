using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public static audiomanager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject); 
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

    }

}
