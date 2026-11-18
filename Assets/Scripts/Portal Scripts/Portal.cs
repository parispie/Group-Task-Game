using UnityEngine;

public class Portal : MonoBehaviour
{
    public PortalColour portalType;

    [SerializeField] private TeleportHandler teleportHandler;
    

    // teleports user to other portal. special check for the cube
    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController player = col.gameObject.GetComponentInParent<PlayerController>();

        if (col.gameObject.GetComponentInParent<PlayerController>())
        {
            teleportHandler.Teleport
            (
                player.gameObject, 
                player.GetComponent<Rigidbody2D>(), 
                portalType
            );
        }

        if (col.gameObject.CompareTag("BuddyCube"))
        {
            GameObject cube = col.gameObject;
            teleportHandler.Teleport
            (
                cube, 
                cube.GetComponent<Rigidbody2D>(), 
                portalType
            );
        }

    }
    void Start()
    {
        teleportHandler = FindAnyObjectByType<TeleportHandler>();

        if(portalType == PortalColour.Orange)
        {
            teleportHandler.orangePortal = gameObject.transform;
        }
        if(portalType == PortalColour.Blue)
        {
            teleportHandler.bluePortal = gameObject.transform;
        }
    }
}
