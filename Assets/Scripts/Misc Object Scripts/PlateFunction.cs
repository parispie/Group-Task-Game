using System.Collections;
using UnityEngine;

public class PlateFunction : MonoBehaviour
{
    [SerializeField] private Sprite pressurePlate;
    [SerializeField] private Sprite pressurePlateOn;
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private GateController connectedGate;
    [SerializeField] private float delayTime = 0.7f;
    private bool isPlateActivated = false;

    void Start()
    {
        spr.sprite = pressurePlate;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("enter");
        OnPlateShift(col);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("exit");
        StartCoroutine(PlateDelay(col));
    }

    void OnPlateShift(Collider2D col)
    {
        if (col.gameObject.name == "Cube")
        {
            isPlateActivated = !isPlateActivated;
            spr.sprite = isPlateActivated ? pressurePlateOn : pressurePlate;
        }
        connectedGate.SwitchGate();
    }

    private IEnumerator PlateDelay(Collider2D col)
    {
        yield return new WaitForSeconds(delayTime);
        OnPlateShift(col);
    }
}
