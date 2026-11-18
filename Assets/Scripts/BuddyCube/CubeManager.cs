using System;
using UnityEngine;

[RequireComponent(typeof(Shooting))]
public class CubeManager : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform spawnPoint;
    public bool isHoldingCube = false;
    [SerializeField] ColliderInfo colInfo;

    private GameObject nearbyCube;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Detects when player enters another objects trigger area
    void OnTriggerEnter2D(Collider2D other)
    {   
        Debug.Log("OnTriggerEnter2D");
        
        //check if object entering the trigger has the BudddyCubet tag
        if (other.CompareTag("BuddyCube"))
        {
            //stores the cube so can be picked up by player
            nearbyCube = other.gameObject;
        }
    }
    
    // Detects when player leaves the trigger range of the nearby cube
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        
        //Check if the object leaving the trigger is the current nearby cube
        if (nearbyCube == other.gameObject)
        {
            // Clear the refference so no more nearby cube
            nearbyCube = null;
        }
    }

    //Handles when picking up the cube
    void PickupCube()
    {
        //Destroy the original cube after picking up
        Destroy(nearbyCube);
        nearbyCube = null;
        
        //Update holding status
        isHoldingCube = true;
        
        Debug.Log("pickuped");
    }

    // Handles when dropping the cube
    void DropCube()
    {
        //checks the ColliderInfo script to see if it's returning true on triggers or collisions so you can't throw cubes through walls or gates
        if (colInfo.isTrig == true || colInfo.isCol == true)
        {
            return;
        }
        GameObject droppedCube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);

        droppedCube.transform.position = transform.position;
        isHoldingCube = false;

        Rigidbody2D droppedCubeRig = droppedCube.GetComponent<Rigidbody2D>();
        
        // fire towards mouse
        droppedCubeRig.AddForce(GetComponent<Shooting>().DirectionToMouse() * 5, ForceMode2D.Impulse);
        Debug.Log("dropped");
    }
    
    void Update()
    {
        //Checks if the player presses E
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
