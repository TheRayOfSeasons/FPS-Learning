using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPointer : MonoBehaviour
{
    public GameObject bullet;
    public Transform front;

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject bullet = Instantiate(this.bullet);
            bullet.transform.position = front.position;
            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 direction = (hit.point - front.position).normalized;
            rigidbody.AddForce(direction * 2000f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
}
