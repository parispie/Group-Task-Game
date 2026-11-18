using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
    public bool isCol;
    public bool isTrig;


    void OnTriggerStay2D(Collider2D collision)
    {
        if (CheckForIgnorables(collision.gameObject))
        {
            isTrig = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (CheckForIgnorables(collision.gameObject))
        {
            isTrig = false;
        }        
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (CheckForIgnorables(collision.gameObject))
        {
            isCol = true;
        }        
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckForIgnorables(collision.gameObject))
        {
            isCol = false;
        }
    }

    private bool CheckForIgnorables(GameObject obj)
    {
        return !obj.CompareTag("Player") && !obj.transform.root.CompareTag("BuddyCube") && !obj.transform.root.CompareTag("PressurePlate") && obj.layer != 9;
    }
}
