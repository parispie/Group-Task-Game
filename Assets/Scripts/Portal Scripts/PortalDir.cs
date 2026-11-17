using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PortalDir : MonoBehaviour
{
    Collider2D col;

    [Range(-1,1)] public int horizontalDir;
    [Range(-1, 1)] public int verticalDir;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }
}
