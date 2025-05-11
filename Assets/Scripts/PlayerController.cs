using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private bool isGrounded;
    private bool hasWon = false;
    public bool isInvulnerable = false;
    public GameObject onCollectEffect;

    public float speed = 0;
    public float jumpForce = 5f;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject mainMenu;

    private Vector3 respawnPosition = new Vector3(0, 0.5f, 0);
    private int lives = 3;
    public TextMeshProUGUI liveText;
    public float invulnerabilityDuration = 1f;
    public Transform pickUpParent;

    private float immortalityTimer = 0f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        SetLivesText();
        winTextObject.SetActive(false);
        mainMenu.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= pickUpParent.childCount+2)
        {
            winTextObject.SetActive(true);
            mainMenu.SetActive(true);
            hasWon = true;

            GameObject enemy = GameObject.Find("Enemy");
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
    }

    void SetLivesText()
    {
        liveText.text = "Lives: " + lives.ToString();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (transform.position.y < -5)
        {
            RespawnPlayer();
        }

        
        if (isInvulnerable && immortalityTimer > 0f)
        {
            immortalityTimer -= Time.deltaTime;
            if (immortalityTimer <= 0f)
            {
                isInvulnerable = false;
            }
        }
    }

    void RespawnPlayer()
    {
        transform.position = respawnPosition;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            Instantiate(onCollectEffect, transform.position, transform.rotation);
            count++;
            SetCountText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasWon || isInvulnerable) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives--;
            SetLivesText();

            if (lives > 0)
            {
                StartCoroutine(InvulnerabilityTimer());
                RespawnPlayer();
            }
            else
            {
                winTextObject.SetActive(true);
                mainMenu.SetActive(true);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                gameObject.SetActive(false);
            }
        }

        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }

    private System.Collections.IEnumerator InvulnerabilityTimer()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    
    public void ActivateImmortality(float duration)
    {
        isInvulnerable = true;
        immortalityTimer = duration;
    }
}
