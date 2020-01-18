using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mallet : MonoBehaviour {
    private float t = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        transform.eulerAngles = new Vector3(Mathf.Sin(t) * 90, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
