using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxRunSpeed = 5;
    public float jumpSpeed = 5;
    public bool facingRight = true;
    public GameObject gameController;
    public Text scoreText;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Collider2D coll;
    private bool onGround = true;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = coll.IsTouchingLayers();
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * maxRunSpeed, rb.velocity.y);
        facingRight = move < 0;
        sr.flipX = facingRight;
        if (onGround) anim.SetFloat("runSpeed", Mathf.Abs(move));
        anim.SetBool("jumping", !onGround);
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pickup"))
        {
            collider.gameObject.SetActive(false);
            score++;
            scoreText.text = score.ToString();
            GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
            if (pickups.Length == 0)
            {
                SceneManager.LoadScene("second");
            }
        }
    }
}
