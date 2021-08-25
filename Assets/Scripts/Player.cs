using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Rigidbody projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Move();
        if(Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Rigidbody rb = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
        rb.velocity = projectileSpeed * transform.forward;
    }

    void Move()
    {
        // Converting the mouse position to a point in 3D-space
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        if(point != null)
        {
            // Using some math to calculate the point of intersection between the line going through the camera and the mouse position with the XZ-Plane
            float t = Camera.main.transform.position.y / (Camera.main.transform.position.y - point.y);
            Vector3 finalPoint = new Vector3(t * (point.x - Camera.main.transform.position.x) + Camera.main.transform.position.x, transform.position.y, t * (point.z - Camera.main.transform.position.z) + Camera.main.transform.position.z);
            // Debug.Log(finalPoint);
            // Error check
            if(Vector3.Distance(transform.position, finalPoint) > 2f && finalPoint.magnitude < 100f)
            {
                //Rotating the object to that point
                transform.LookAt(finalPoint, Vector3.up);

                transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime));
            }
            
        }
        
    }
}
