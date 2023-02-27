using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class GlowBox : MonoBehaviour
{
    private Rigidbody2D rb;
    private LightController lightController;

    public float squareYSearchOffset = 100f;
    public float moveSpeed;

    private int currentIndex;
    private int facingDirection;

    private Collider2D currentWall;

    public List<GameObject> scoreSquares;
    private bool alterLights = false;

    private List<Color> colors = new List<Color>
    {
        Color.red,
        Color.white,
        Color.blue,
    };

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lightController = GetComponent<LightController>();

        facingDirection = -1;
    }
    public int SearchPlayerUnderRange()
    {
        int score = 0;
        for (int i = 0; i < scoreSquares.Count; i++)
        {
            if (CheckBoxRange(i))
            {
                switch (i)
                {
                    case 0:
                        score = 5;
                        break;
                    case 1:
                        score = 3;
                        break;
                    case 2:
                        score = 1;
                        break;
                    default:
                        score = 0;
                        break;
                }
                Debug.Log($"{i}, {score}");
                return score;
            }
        }
        return score;
    }
    /// <summary>
    /// Check a box range if the player exist there.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>True and if player exists there and false if else.</returns>
    private bool CheckBoxRange(int index)
    {
        var hits = Physics2D.OverlapBoxAll(scoreSquares[index].transform.position, new Vector2(scoreSquares[index].transform.localScale.x, scoreSquares[index].transform.localScale.y * squareYSearchOffset), 0f);
        

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
    public void AffectLights(int scoreIndex)
    {
        currentIndex = CheckScoreIndex(scoreIndex);

        lightController.scoreLights[currentIndex].gameObject.SetActive(true);
        lightController.scoreLights[currentIndex].intensity = 0;

        alterLights = true;
    }
    private int CheckScoreIndex(int score)
    {
        int index;
        switch (score)
        {
            case 1:
                index = 2;
                break;
            case 3:
                index = 1;
                break;
            case 5:
                index = 0;
                break;
            default:
                index = -1;
                break;
        }
        return index;
    }
    private void FixedUpdate()
    {
        Move();

        if (alterLights)
        {
            //TODO: write logic to alter light by index
            lightController.AnimateLight(lightController.scoreLights[currentIndex], lightController.scoreLightData[currentIndex], lightController.pulseTimers[currentIndex], lightController.stayTimers[currentIndex]);
        }
    }
    private void ChangePlayerDirection()
    {
        facingDirection *= -1;
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveSpeed * facingDirection, rb.velocity.y);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
         //   if (other.collider != currentWall)
            {
                ChangePlayerDirection();
                currentWall = other.collider;
            }

        }
    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < scoreSquares.Count; i++)
        {
            Gizmos.color = colors[i];
            Gizmos.DrawCube(scoreSquares[i].transform.position, new Vector3(scoreSquares[i].transform.localScale.x, scoreSquares[i].transform.localScale.y * squareYSearchOffset, 1f));
        }
    }
}
