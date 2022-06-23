using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float health = 10f;
    public GameObject bullet;
    public GameObject grenade;
    public Transform front;
    public Gun currentGun = Gun.PISTOL;
    public Dictionary<Gun, int> ammoCount = new Dictionary<Gun, int>() {
        {Gun.PISTOL, 10},
        {Gun.GRENADE_LAUNCHER, 5},
    };

    void Start()
    {
        this.UpdateAmmo();
        this.UpdateHealth();
    }

    void ShootPistol()
    {
        if (this.ammoCount[Gun.PISTOL] <= 0)
            return;
        this.ammoCount[Gun.PISTOL]--;
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
        if (this.ammoCount[Gun.GRENADE_LAUNCHER] <= 0)
            return;
        this.ammoCount[Gun.GRENADE_LAUNCHER]--;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GameObject grenade = Instantiate(this.grenade);
        Grenade grenadeScript = grenade.GetComponent<Grenade>();
        grenade.transform.position = front.position;
        grenadeScript.origin = this.gameObject;
        Rigidbody rigidbody = grenade.GetComponent<Rigidbody>();
        Vector3 direction = ray.direction;
        rigidbody.AddForce(direction * 2000f);
    }

    public void UpdateAmmo()
    {
        int ammo = this.ammoCount[this.currentGun];
        UIManager.Instance.SetAmmo(ammo);
    }

    public void UpdateHealth()
    {
        UIManager.Instance.SetPlayerHealth(this.health);
    }

    void Shoot()
    {
        switch(this.currentGun)
        {
            case Gun.PISTOL:
                this.ShootPistol();
                break;
            case Gun.GRENADE_LAUNCHER:
                this.ShootGrenade();
                break;
        }
        this.UpdateAmmo();
    }

    void SwitchGun()
    {
        Gun[] guns = (Gun[])System.Enum.GetValues(typeof(Gun));
        int currentIndex = (int)this.currentGun;
        int nextIndex = currentIndex + 1;
        try
        {
            this.currentGun = guns[nextIndex];
        }
        catch(IndexOutOfRangeException)
        {
            this.currentGun = guns[0];
        }
        this.UpdateAmmo();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.SwitchGun();
            Debug.Log(this.currentGun);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    public void Hit(float damage)
    {
        if (this.health <= 0)
        {
            return;
        }
        this.health -= damage;
        this.UpdateHealth();
        if (this.health <= 0)
        {
            UIManager.Instance.ToggleDiedScreen(true);
        }
    }
}
