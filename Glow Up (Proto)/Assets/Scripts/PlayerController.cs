using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public PlayerData playerData;

    private Rigidbody2D rb;
    private LineRenderer line;

    private int health = 3;
    private int facingDirection;

    public float moveSpeed;
    public Vector2 jumpVelocity;
    public float groundCheckDistance;
    public LayerMask whatIsGround;

    public float groundStayTime;
    private float _groundStayTime;
    public float timeToAdvance = 1f;

    public bool isAdvancing { get; private set; }

    private Collider2D currentWall;

    private List<GameObject> healthIcons = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        line = GetComponentInChildren<LineRenderer>();

        facingDirection = 1;
        _groundStayTime = groundStayTime;
        health = playerData.health;
    }
    private void Update()
    {
        UpdateLine();
        if (Input.GetButtonDown("Fire1") && !isAdvancing)
            StartCoroutine(CheckPlayerBoxRange());
    }
    void FixedUpdate()
    {
        line.gameObject.SetActive(!isAdvancing);
        if (!isAdvancing)
        {
            Move();
        }
    }
    private void UpdateLine()
    {
        line.positionCount = 2;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, GameManager.instance.currentGlowBox.transform.position);
    }
    private void Move()
    {
     //   rb.angularVelocity = moveSpeed * facingDirection;
        rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
    }
    private void ChangePlayerDirection()
    {
        facingDirection *= -1;
    }
    
    private IEnumerator CheckPlayerBoxRange()
    {
        int score = GameManager.instance.currentGlowBox.SearchPlayerUnderRange();
        if (score == 0)
        {
            DecreaseHealth();
            UpdateHealthIcons();

            if(health <= 0)
            {
                GameManager.instance.GameEnd();
            }
            // display damage effects
        }
        else
        {
            GameManager.instance.currentGlowBox.AffectLights(score);
            ScoreSystem.GameScore.scores[ConstNames.MAIN_SCORE] += score;
            yield return new WaitForSeconds(timeToAdvance);
            WorldSpawner.instance.InstantiateLevel();
            AdvancePlayer();
        }
    }
    private void AdvancePlayer()
    {
        isAdvancing = true;
        rb.AddForce(new Vector2(-facingDirection * jumpVelocity.x, jumpVelocity.y));
    }
    public bool CheckIfGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
          //  if (other.collider != currentWall)
            {
                ChangePlayerDirection();
                currentWall = other.collider;
            }

        }
        //if (other.collider.CompareTag("Ground"))
        //{
        //    isAdvancing = false;
        //}
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            if(_groundStayTime >= 0)
            {
                _groundStayTime -= Time.deltaTime;
            }
            else
            {
                isAdvancing = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
        {
            isAdvancing = true;
            _groundStayTime = groundStayTime;
        }
    }

    private void IncreaseHealth() => health++;
    private void DecreaseHealth() => health--;
    public void InstantiateHealthIcons(GameObject icon, Transform spawnParent)
    {
        for (int i = 0; i < playerData.health; i++)
        {
            var iconGO = Instantiate(icon, spawnParent);
            healthIcons.Add(iconGO);
        }
    }
    private void UpdateHealthIcons()
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            healthIcons[i].SetActive(false);
        }
        for (int i = 0; i < health; i++)
        {
            healthIcons[i].SetActive(true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(Vector2.down * groundCheckDistance));
    }
}
