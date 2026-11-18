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
        Vector2 exit = new Vector2(portalTwo.horizontalDir, portalTwo.verticalDir).normalized;

        portal.transform.position = (Vector2)portalTwo.transform.position + (exit * 1.15f);

        float speed =  Mathf.Clamp(speedBoost * rig.linearVelocity.magnitude, minSpeed, maxSpeed);
        rig.linearVelocity = exit * speed;

    }
}

