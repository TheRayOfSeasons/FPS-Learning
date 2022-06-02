using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;

    void Start()
    {
        Destroy(this.gameObject, 5f);
    }
}
