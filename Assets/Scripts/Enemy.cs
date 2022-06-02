using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 3f;
    public GameObject player;
    public GameObject front;
    public GameObject bulletPrefab;
    public float attackSpeed = 2f;
    public float interval = 2f;
    public float timer = 2f;

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        bullet.transform.position = this.front.transform.position;
        Vector3 direction = (this.player.transform.position - this.front.transform.position).normalized;
        rigidbody.AddForce(direction * 2000f);
    }

    void Update()
    {
        this.transform.LookAt(this.player.transform);

        this.timer -= Time.deltaTime;
        if (this.timer <= 0f)
        {
            this.Shoot();
            this.timer = this.interval;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameObject bullet = collision.gameObject;
            float damage = bullet.GetComponent<Bullet>().damage;
            health -= damage;
            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
