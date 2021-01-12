using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionCounter : MonoBehaviour
{

    int framesExpanding;
    // Start is called before the first frame update
    void Start()
    {
        framesExpanding = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (framesExpanding > 0)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 1.5f, Time.deltaTime * 10);
            framesExpanding--;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
