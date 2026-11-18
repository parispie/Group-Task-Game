using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    [SerializeField] private SpriteRenderer gunSpriteRenderer;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject cubePrefab;
    
    [SerializeField] private CubeManager cubeManager;
    
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        defaultSprite = gunSpriteRenderer.sprite;
    }

    void Update()
    {
        if (cubeManager.isHoldingCube)
        {
            gunSpriteRenderer.sprite = cubePrefab.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            gunSpriteRenderer.sprite = defaultSprite;
        }

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotateZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotateZ);
    }

    public Vector3 DirectionToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        return direction;
    }
}
