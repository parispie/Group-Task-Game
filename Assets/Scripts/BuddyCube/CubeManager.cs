using System;
using UnityEngine;

[RequireComponent(typeof(Shooting))]
public class CubeManager : MonoBehaviour
{
    public GameObject grabRange;
    public GameObject cubePrefab;
    public Transform spawnPoint;
    public bool isHoldingCube = false;

    private GameObject nearbyCube;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log("OnTriggerEnter2D");
        if (other.CompareTag("BuddyCube"))
        {
            nearbyCube = other.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        if (nearbyCube == other.gameObject)
        {
            nearbyCube = null;
        }
    }

    void PickupCube()
    {
        Destroy(nearbyCube);
        nearbyCube = null;
        isHoldingCube = true;
        
        Debug.Log("pickuped");
    }

    void DropCube()
    {
        GameObject droppedCube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);
        droppedCube.transform.position = transform.position;
        isHoldingCube = false;
        Rigidbody2D droppedCubeRig = droppedCube.GetComponent<Rigidbody2D>();
        droppedCubeRig.AddForce(GetComponent<Shooting>().DirectionToMouse() * 5, ForceMode2D.Impulse);
        Debug.Log("dropped");
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoldingCube)
            {
                DropCube();
            }
            else if (nearbyCube != null)
            {
                PickupCube();
            }
        }
    }
    
}
