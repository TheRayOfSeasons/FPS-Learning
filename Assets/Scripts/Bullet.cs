using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 1f;
    public GameObject origin;

    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    bool IsTarget(Collision collision, string tag)
    {
        return collision.gameObject.tag == tag && tag != origin.tag;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (this.IsTarget(collision, "Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Hit(this.damage);
        }
        if (this.IsTarget(collision, "Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.Hit(this.damage);
            }
        }
    }
}
