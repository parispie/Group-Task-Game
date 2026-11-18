using System;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    public float maxSpeed = 100f;
    public float minSpeed = 4f;

    public Transform orangePortal;
    public Transform bluePortal;

    public void Teleport(GameObject portal, Rigidbody2D rig, PortalColour portalColour)
    {
        //orange portal
        if (portalColour == PortalColour.Orange)
        {
            TeleportPlayer(orangePortal.GetComponent<PortalDir>(),
                bluePortal.GetComponent<PortalDir>(),
                portal,
                rig);
        }

        //blue portal
        else if (portalColour == PortalColour.Blue)
        {
            TeleportPlayer(
                bluePortal.GetComponent<PortalDir>(),
                orangePortal.GetComponent<PortalDir>(),
                portal,
                rig);
        }
    }

    
    private void TeleportPlayer(PortalDir portalOne, PortalDir portalTwo, GameObject portal, Rigidbody2D rig)
    {
        float speedBoost = 1.05f;

        //direction that the exit wall/portal faces
        Vector2 exit = new Vector2(portalTwo.horizontalDir, portalTwo.verticalDir).normalized;

        //move the player/obj to the exit wall but slightly more so they dont spawn inside it
        portal.transform.position = (Vector2)portalTwo.transform.position + (exit * 1.05f);

        //take how fast the player was going, increase speed slightly, and clamp between min and max speed
        float speed =  Mathf.Clamp(speedBoost * rig.linearVelocity.magnitude, minSpeed, maxSpeed);
        rig.linearVelocity = exit * speed;

        //makes sure the player ignores portals for a small period for no rapid teleporting
        rig.GetComponent<PlayerController>().StartTeleportCooldown();

    }
    
}

