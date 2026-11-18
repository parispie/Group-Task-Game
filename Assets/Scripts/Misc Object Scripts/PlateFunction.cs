using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    [SerializeField] private Sprite pressurePlate;
    [SerializeField] private Sprite pressurePlateOn;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private List<GateController> connectedGates;
    [SerializeField] private float delayTime = 0.7f;
    private bool isPlateActivated = false;

    void Start()
    {
        spr.sprite = pressurePlate;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("BuddyCube"))
        {
            Debug.Log("enter");
            OnPlateShift(col);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("BuddyCube"))
        {
            Debug.Log("exit");
            StartCoroutine(PlateDelay(col));
        }
    }

    void OnPlateShift(Collider2D col)
    {
        if (col.gameObject.name == "Cube")
        {
            isPlateActivated = !isPlateActivated;
            spr.sprite = isPlateActivated ? pressurePlateOn : pressurePlate;
        }
        
        foreach (GateController gate in connectedGates)
        {
            gate.SwitchGate();
        }
    }

    //this is for when the box bounces a little on the plate, so that it doesn't rapidly make the gate flash visible and invisible
    private IEnumerator PlateDelay(Collider2D col)
    {
        yield return new WaitForSeconds(delayTime);
        OnPlateShift(col);
    }
}
