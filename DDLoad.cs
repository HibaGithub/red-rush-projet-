using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDLoad : MonoBehaviour
{
    public static DDLoad underground;
    // Start is called before the first frame update
    void Start()
    {
        if (underground == null)
        {
            underground = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

}
