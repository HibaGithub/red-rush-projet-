using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDLoad3 : MonoBehaviour
{
    public static DDLoad3 camBoundry;
    // Start is called before the first frame update
    void Start()
    {
        if (camBoundry == null)
        {
            camBoundry = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }
}
