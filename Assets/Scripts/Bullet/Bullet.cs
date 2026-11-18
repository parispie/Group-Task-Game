using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Color orange;
    [SerializeField] private Color blue;

    [SerializeField] private Sprite bluePortalRight;
    [SerializeField] private Sprite bluePortalLeft;
    [SerializeField] private Sprite bluePortalUp;
    [SerializeField] private Sprite bluePortalDown;

    [SerializeField] private Sprite orangePortalRight;
    [SerializeField] private Sprite orangePortalLeft;
    [SerializeField] private Sprite orangePortalUp;
    [SerializeField] private Sprite orangePortalDown;
    
    [SerializeField] private Sprite defaultWall;

    private Color setColour;

    private Rigidbody2D rig;
    private Portal[] portals;

    private Camera mainCam;
    private Vector3 mousePos;

    [HideInInspector] public PortalColour bulletType;

    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (bulletType == PortalColour.Orange)
        {
            gameObject.GetComponent<SpriteRenderer>().color = orange;
            setColour = orange;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = blue;
            setColour = blue;
        }
    }

    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

    }

    //destroy bullet when hitting any object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //but spawn teleport/portal when hitting a wall which has PortalDir as a component
        if (collision.gameObject.GetComponent<PortalDir>())
        {
            Destroy(gameObject);
            SpawnTeleport(bulletType, setColour, collision.gameObject);
        }
        else if (!collision.gameObject.GetComponentInParent<PlayerController>()) 
        {
            Destroy(gameObject);
        }
    }

    private void SpawnTeleport(PortalColour telportType, Color BlockColour, GameObject block)
    {
        if (block.GetComponent<PortalDir>())
        {
                //get array of all portals on screen
                portals = FindObjectsByType<Portal>();

                for (int i = 0; i < portals.Length; i++)
                {
                    //make sure there's only ever two portals by destroying the oldest portal component and setting the wall that had it back to default
                    if (portals[i].portalType == (PortalColour)telportType && portals[i].gameObject != block)
                    {
                        Destroy(portals[i].gameObject.GetComponent<Portal>());

                        portals[i].gameObject.tag = "Wall";

                        portals[i].gameObject.GetComponent<Collider2D>().isTrigger = false;

                        portals[i].gameObject.GetComponent<SpriteRenderer>().sprite = defaultWall;
                        
                    }
                }

                // adding a portal to a fresh wall . relies on the horizontalDir and verticalDir values from portalDir.cs
                if (!block.GetComponent<Portal>())
                {
                    block.AddComponent<Portal>();

                    block.GetComponent<Portal>().portalType = (PortalColour)telportType;

                    int horizontalDir = block.GetComponent<PortalDir>().horizontalDir;
                    int verticalDir = block.GetComponent<PortalDir>().verticalDir;

                    SpriteRenderer wallSpriteRenderer = block.GetComponent<SpriteRenderer>();
                    if(bulletType == PortalColour.Orange)
                    {
                        if(horizontalDir == 1)
                        {
                            wallSpriteRenderer.sprite = orangePortalRight;
                        }
                        else if(horizontalDir == -1)
                        {
                            wallSpriteRenderer.sprite = orangePortalLeft;
                        }
                        else if(verticalDir == 1)
                        {
                            wallSpriteRenderer.sprite = orangePortalUp;
                        }
                        else if(verticalDir == -1)
                        {
                            wallSpriteRenderer.sprite = orangePortalDown;
                        }
                    }
                    else
                    {
                        if(horizontalDir == 1)
                        {
                            wallSpriteRenderer.sprite = bluePortalRight;
                        }
                        else if(horizontalDir == -1)
                        {
                            wallSpriteRenderer.sprite = bluePortalLeft;
                        }
                        else if(verticalDir == 1)
                        {
                            wallSpriteRenderer.sprite = bluePortalUp;
                        }
                        else if(verticalDir == -1)
                        {
                            wallSpriteRenderer.sprite = bluePortalDown;
                        }
                }

                block.tag = "Portal";

                GameObject[] ActiveTeleports = GameObject.FindGameObjectsWithTag("Portal"); 

                if (ActiveTeleports.Length > 1)
                {
                    for (int i = 0; i < ActiveTeleports.Length; i++)
                    {
                        ActiveTeleports[i].GetComponent<Collider2D>().isTrigger = true;
                    }
                }
            }
        }
    }

    public Vector3 DirectionToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        return direction;
    }

}
