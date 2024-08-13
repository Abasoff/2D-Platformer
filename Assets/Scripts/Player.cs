using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    float inputX;
    Animator anim;
    bool facingRight = true;

    public Transform groundCheck;
    bool isGrounded;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    public float jumpForce;

    public GameObject winPanel;

    GameManager gm;

    public Transform weapon;
    public Camera cam;

    public float offset;


    public GameObject bullet;
    public Transform shotPoint;

    public float timeBetweenShots;
    float nextShotTime;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        transform.position += Vector3.right * inputX * speed * Time.deltaTime;

        if (inputX == 0)
        {
            anim.SetBool("isRunning", false);
        } else
        {
            anim.SetBool("isRunning", true);
        }

        if (inputX == -1 && facingRight == true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        } else if (inputX == 1 && facingRight == false)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        } else
        {
            anim.SetBool("isJumping", true);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        Vector3 displacement = cam.ScreenToWorldPoint(Input.mousePosition) - weapon.position;
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        if (Input.GetMouseButton(0))
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(bullet, shotPoint.position, shotPoint.rotation);
                nextShotTime = Time.time + timeBetweenShots;
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Flag")
        {
            winPanel.SetActive(true);
        }

        if (other.tag == "Spike")
        {
            gm.RestartGame();
        }
    }
}
