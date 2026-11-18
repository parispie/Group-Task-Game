using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] Collider2D collider;
    [SerializeField] SpriteRenderer spr;

    public void Start()
    {
        spr.color = new Color(1,1,1,1);
        collider.enabled = true;
    }

    //makes gates visible and invisible whilst disabling and enabling collider
    public void SwitchGate()
    {
        if(collider.enabled)
        {
            collider.enabled = false;
            spr.color = new Color(1,1,1,0);
        }
        else
        {
            collider.enabled = true;
            spr.color = new Color(1,1,1,1);
        }
    }
}
