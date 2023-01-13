using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDLoad2 : MonoBehaviour
{
    public static DDLoad2 cam;
    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
        {
            cam = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
