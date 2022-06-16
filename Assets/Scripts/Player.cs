using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health = 10f;
    public GameObject bullet;
    public GameObject grenade;
    public Transform front;

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GameObject bullet = Instantiate(this.bullet);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bullet.transform.position = front.position;
        bulletScript.origin = this.gameObject;
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        Vector3 direction = ray.direction;
        rigidbody.AddForce(direction * 2000f);
    }

    void ShootGrenade()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GameObject grenade = Instantiate(this.grenade);
        Grenade grenadeScript = grenade.GetComponent<Grenade>();
        grenade.transform.position = front.position;
        grenadeScript.origin = this.gameObject;
        Rigidbody rigidbody = grenade.GetComponent<Rigidbody>();
        Vector3 direction = ray.direction;
        rigidbody.AddForce(direction * 2000f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootGrenade();
        }
    }

    public void Hit(float damage)
    {
        if (this.health <= 0)
        {
            return;
        }
        this.health -= damage;
        if (this.health <= 0)
        {
            UIManager.Instance.ToggleDiedScreen(true);
        }
    }
}
