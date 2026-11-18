using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private float horizontal;
    private readonly float speed = 8f;
    private readonly float jumpStrength = 5f;

    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask buddyLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float deceleration = 25f;
    [SerializeField] private float fireStrength = 13f;
    public bool canShootBlue = true;
    public bool canShootOrange = true;

    private float teleportTimer = 0f;
    private CubeManager cubeManager;
    public Transform bulletTransform;
    public GameObject portalBall;
    public Shooting rotatePointShootScript;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioSource audioSource;

    void Start()
    {
        cubeManager = GetComponentInChildren<CubeManager>();
    }
    
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpStrength);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (!cubeManager.isHoldingCube)
        {
            if (Input.GetMouseButtonDown(0) && canShootBlue)
            {
                ShootBullet(PortalColour.Blue);
                audioSource.PlayOneShot(shootSound);
            }
            else if (Input.GetMouseButtonDown(1) && canShootOrange)
            {
                ShootBullet(PortalColour.Orange);
                audioSource.PlayOneShot(shootSound);
            }
        }
    }

    private void FixedUpdate()
    {
        if (teleportTimer > 0)
        {
            teleportTimer -= Time.fixedDeltaTime;
            return;
        }
        
        float targetSpeed = horizontal * speed;
        float rate;

        if (horizontal != 0)
        {
            rate = acceleration;
        }
        else
        {
            rate = deceleration;
        }
        
        float newVelocityX = Mathf.MoveTowards(rb.linearVelocityX, targetSpeed, rate*Time.deltaTime);
        
        rb.linearVelocity = new  Vector2(newVelocityX, rb.linearVelocityY);
    }
    
    public void StartTeleportCooldown()
    {
        teleportTimer = 0.2f;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer) || Physics2D.OverlapCircle(groundCheck.position, 0.1f, buddyLayer);
    }

    // fires ball in direction towards mouse
    private GameObject ShootBullet(PortalColour colour)
    {
        GameObject firedBall = Instantiate(portalBall, null);
        firedBall.transform.position = bulletTransform.transform.position;
        Rigidbody2D firedBallRig = firedBall.GetComponent<Rigidbody2D>();

        firedBall.GetComponent<Bullet>().bulletType = colour;

        firedBallRig.AddForce(rotatePointShootScript.DirectionToMouse() * fireStrength, ForceMode2D.Impulse);

        return firedBall;
    }
}
