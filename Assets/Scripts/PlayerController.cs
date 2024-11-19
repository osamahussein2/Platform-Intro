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

    private new Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        facingDirection = new FacingDirection();
        rigidbody = GetComponent<Rigidbody2D>();
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

        rigidbody.position += playerSpeed * Time.deltaTime * playerInput;
    }

    public bool IsWalking()
    {
        return isPlayerWalking;
    }
    public bool IsGrounded()
    {
        return false;
    }

    public FacingDirection GetFacingDirection()
    {
        return facingDirection;
    }
}
