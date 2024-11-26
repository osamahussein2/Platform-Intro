using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }

    FacingDirection facingDirection;
    public float playerSpeed = 5.0f;
    bool isPlayerWalking = false;
    bool isPlayerGrounded = false;

    private new Rigidbody2D rigidbody;

    public float apexHeight;
    public float apexTime;

    public float terminalSpeed = 2.5f;
    public float fallVelocity;

    private float fallingTime;

    private float gravity;
    private float jumpVelocity;

    // Start is called before the first frame update
    void Start()
    {
        facingDirection = new FacingDirection();
        rigidbody = GetComponent<Rigidbody2D>();

        gravity = 0.0f;
        jumpVelocity = 0.0f;

        fallingTime = 0.0f;

        fallVelocity = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.
        Vector2 playerInput = new Vector2();
        MovementUpdate(playerInput);
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        playerInput = Vector2.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            playerInput = new Vector2(-1.0f, 0.0f);

            facingDirection = FacingDirection.left;

            isPlayerWalking = true;
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            playerInput = new Vector2(1.0f, 0.0f);

            facingDirection = FacingDirection.right;

            isPlayerWalking = true;
        }

        else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftArrow) &&
            !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.RightArrow))
        {
            playerInput = Vector2.zero;

            isPlayerWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded)
        {
            gravity = 1.0f;
            jumpVelocity = 2.0f * apexHeight / apexTime * Time.deltaTime;
        }

        if (rigidbody.velocity.y >= apexHeight && !isPlayerGrounded)
        {
            gravity = -2.0f * apexHeight / Mathf.Pow(apexTime, 2);
            jumpVelocity = 0.0f;
        }

        if (rigidbody.velocity.y <= 0.0f && isPlayerGrounded)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
            fallVelocity = 1.0f;
        }

        else if (rigidbody.velocity.y <= 0.0f && !isPlayerGrounded)
        {
            fallVelocity += Time.deltaTime;
        }

        if (rigidbody.velocity.y > 0.0f && !isPlayerGrounded)
        {
            fallVelocity = 1.0f;
        }

        if (fallVelocity > terminalSpeed)
        {
            fallVelocity = terminalSpeed;
        }

        else if (fallVelocity <= 1.0f)
        {
            fallVelocity = 1.0f;
        }

        rigidbody.position += playerSpeed * Time.deltaTime * playerInput;
        rigidbody.velocity += new Vector2(0.0f, gravity * fallVelocity * Time.deltaTime + jumpVelocity);
    }

    public bool IsWalking()
    {
        return isPlayerWalking;
    }
    public bool IsGrounded()
    {
        return isPlayerGrounded;
    }

    public FacingDirection GetFacingDirection()
    {
        return facingDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GroundTilemap")
        {
            isPlayerGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GroundTilemap")
        {
            isPlayerGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GroundTilemap")
        {
            isPlayerGrounded = false;
        }
    }
}
