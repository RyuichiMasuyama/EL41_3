using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutGround : MonoBehaviour
{
    [SerializeField]
    private GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++) {
            GameObject.Instantiate(ground, transform.position + new Vector3(0, 0, i * 10-150), Quaternion.identity,transform);
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
