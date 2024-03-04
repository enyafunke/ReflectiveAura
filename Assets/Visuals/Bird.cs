using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    bool fly;

    public void StartFly()
    {
        fly = true;
    }

    public void StopFly()
    {
        fly = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(fly)
        {
            gameObject.transform.position += new Vector3(0, 2, 0);
        }
    }
}
