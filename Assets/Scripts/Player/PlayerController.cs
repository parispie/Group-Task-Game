using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float horizontal;
    private readonly float speed = 8f;
    private readonly float jumpStrength = 8f;

    
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float acceleration = 40f;
    [SerializeField] private float deceleration = 25f;
    public Transform bulletTransform;
    public GameObject portalBall;
    public Shooting rotatePointShootScript;


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

        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet(PortalColour.Blue);

        }
        else if (Input.GetMouseButtonDown(1))
        {
            ShootBullet(PortalColour.Orange);
        }
        
    }

    private void FixedUpdate()
    {
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

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.2f, groundLayer);
    }

    private GameObject ShootBullet(PortalColour colour)
    {
        GameObject firedBall = Instantiate(portalBall, null);
        firedBall.transform.position = bulletTransform.transform.position;
        Rigidbody2D firedBallRig = firedBall.GetComponent<Rigidbody2D>();

        firedBall.GetComponent<Bullet>().bulletType = colour;

        firedBallRig.AddForce(rotatePointShootScript.DirectionToMouse() * 9, ForceMode2D.Impulse);

        return firedBall;
    }
}
