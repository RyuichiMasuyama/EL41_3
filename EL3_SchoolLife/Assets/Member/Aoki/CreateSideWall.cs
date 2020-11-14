using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSideWall : MonoBehaviour
{
    [SerializeField] private GameObject Wall;
    [SerializeField] private int CreateCount;
    [SerializeField] private float Between;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 originPos = transform.position;
        for (int i = 1; i < CreateCount + 1; i++) {
            originPos.z += Between * i;
            Instantiate(Wall, originPos, gameObject.transform.rotation);
        }
    }
}
