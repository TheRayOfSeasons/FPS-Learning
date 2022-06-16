using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float damage = 3f;
    public GameObject origin;
    private bool explode = false;

    bool IsTarget(Collider collider, string tag)
    {
        return collider.gameObject.tag == tag && tag != origin.tag;
    }

    void Detonate()
    {
        this.explode = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            this.Detonate();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (this.explode)
        {
            if (this.IsTarget(collider, "Enemy"))
            {
                Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Hit(this.damage);
                }
            }
            if (this.IsTarget(collider, "Player"))
            {
                Player player = collider.gameObject.GetComponent<Player>();
                if (player != null)
                {
                    player.Hit(this.damage);
                }
            }
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5);
    }
}
