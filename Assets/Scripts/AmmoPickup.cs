using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AmmoPickup : MonoBehaviour
{
    public Gun gun = Gun.PISTOL;
    public int amount = 5;
    public float speed = 25f;

    void Update()
    {
        this.transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * this.speed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Player player = collider.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.ammoCount[this.gun] += this.amount;
                player.UpdateAmmo();
                Destroy(this.gameObject);
            }
        }
    }
}
