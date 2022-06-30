using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum Movement
{
    IDLE,
    PATROLLING,
    HOSTILE
}


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
    public Movement movement = Movement.IDLE;
    public float movementRange = 5f;
    private Vector3 patrolLocation;


    private void AssignNextPatrolLocation()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;
        float z = this.transform.position.z;
        this.patrolLocation = new Vector3(
            Random.Range(x - this.movementRange, x + this.movementRange),
            y,
            Random.Range(z - this.movementRange, z + this.movementRange)
        );
    }

    void Start()
    {
        GameManager.Instance.IncrementEnemyCount();
        this.agent = this.GetComponent<NavMeshAgent>();
        this.AssignNextPatrolLocation();
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

    void DoIdle()
    {
        this.agent.isStopped = true;
        this.agent.destination = this.transform.position;
    }

    void DoPatrol()
    {
        this.agent.destination = this.patrolLocation;
        float distanceFromDestination = Vector3.Distance(
            this.transform.position,
            this.patrolLocation
        );
        Debug.Log(distanceFromDestination);
        if (distanceFromDestination < 1f)
        {
            this.AssignNextPatrolLocation();
        }
    }

    void DoHostile()
    {
        float distance = Vector3.Distance(this.player.transform.position, this.transform.position);
        if (distance >= 5f)
        {
            this.agent.destination = this.player.transform.position;
        }
        else
        {
            this.agent.destination = this.transform.position;
        }
    }

    void DoAction()
    {
        switch(this.movement)
        {
            case Movement.IDLE:
                this.DoIdle();
                break;
            case Movement.PATROLLING:
                this.DoPatrol();
                break;
            case Movement.HOSTILE:
                this.DoHostile();
                this.Attack();
                break;
        }
    }

    void Attack()
    {
        if (this.timer <= 0f)
        {
            this.Shoot();
            this.timer = this.interval;
        }
    }

    void Update()
    {
        this.timer -= Time.deltaTime;
        this.DoAction();
        this.transform.LookAt(
            new Vector3(
                this.agent.destination.x,
                this.transform.position.y,
                this.transform.position.z
            )
        );
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
        GameManager.Instance.DecrementEnemyCount();
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.movement = Movement.HOSTILE;
        }
    }
}
