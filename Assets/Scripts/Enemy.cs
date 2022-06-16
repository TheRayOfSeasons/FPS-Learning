using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 3f;
    public GameObject player;
    public GameObject front;
    public GameObject bulletPrefab;
    public float attackSpeed = 2f;
    public float interval = 2f;
    public float timer = 2f;
    public NavMeshAgent agent;

    void Start()
    {
        this.agent = this.GetComponent<NavMeshAgent>();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        bullet.transform.position = this.front.transform.position;
        bulletScript.origin = this.gameObject;
        Vector3 direction = (this.player.transform.position - this.front.transform.position).normalized;
        rigidbody.AddForce(direction * 2000f);
    }

    void Update()
    {
        this.transform.LookAt(this.player.transform);
        float distance = Vector3.Distance(this.player.transform.position, this.transform.position);
        if (distance >= 5f)
        {
            this.agent.destination = this.player.transform.position;
        }
        else
        {
            this.agent.destination = this.transform.position;
        }
        this.timer -= Time.deltaTime;
        if (this.timer <= 0f)
        {
            this.Shoot();
            this.timer = this.interval;
        }
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
