using System;
using UnityEngine;

public class TeleportHandler : MonoBehaviour
{
    public float maxSpeed = 20f;
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
        else if (portalColour ==  PortalColour.Blue)
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
        float speedBoost = 1.1f;
        Vector2 exit = new(portalTwo.horizontalDir, portalTwo.verticalDir);

        portal.transform.position = (Vector2)portalTwo.transform.position + (exit * 1f);

        rig.linearVelocity = Mathf.Clamp(speedBoost * rig.linearVelocity.magnitude, minSpeed, maxSpeed) * exit;
        
    }
}

