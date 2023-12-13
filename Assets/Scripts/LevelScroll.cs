using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroll : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float speed = 5f;
    private float currentSpeed = 0f;

    [SerializeField] private Transform scrollMin;
    [SerializeField] private Transform scrollMax;

    [SerializeField] private Transform screenVerticalBorderPoint;

    private float distanceToScreenVerticalBorder = 1f;

    private float moveValue = 1f;

    private float yInput;
    private Vector2 moveDirection;

    private Rigidbody2D rb;

    private Vector2 mousePos1, mousePos2;

    private void Start()
    {
        distanceToScreenVerticalBorder = Vector2.Distance(transform.position, screenVerticalBorderPoint.position);

        rb = GetComponent<Rigidbody2D>();

        currentSpeed = speed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            mousePos2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentSpeed = speed * Vector2.Distance(mousePos1, mousePos2);
        }

        if (Input.GetMouseButtonUp(0))
        {
            mousePos1 = Vector2.zero;
            mousePos2 = Vector2.zero;

            MoveStop();
        }

        if (mousePos2.y < mousePos1.y)
        {
            MoveUp();
        }

        if (mousePos2.y > mousePos1.y)
        { 
            MoveDown();
        }

        if (rb.transform.position.y < scrollMin.position.y + distanceToScreenVerticalBorder)
        {
            rb.transform.position = new Vector2(rb.transform.position.x, scrollMin.position.y + distanceToScreenVerticalBorder);
        }

        if (rb.transform.position.y > scrollMax.position.y - distanceToScreenVerticalBorder)
        {
            rb.transform.position = new Vector2(rb.transform.position.x, scrollMax.position.y - distanceToScreenVerticalBorder);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void MoveStop()
    {
        StartCoroutine(OnMoveStop());
    }

    private IEnumerator OnMoveStop()
    {
        while (moveDirection.y > 0.01f || moveDirection.y < -0.01f)
        {
            if (moveDirection.y > 0) moveDirection = new Vector2(moveDirection.x, moveDirection.y - Time.deltaTime * 0.05f);
            else moveDirection = new Vector2(moveDirection.x, moveDirection.y + Time.deltaTime * 0.05f);

            yield return null;
        }

        moveDirection = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    public void MoveDown()
    {
        StartCoroutine(OnMoveStop());

        yInput = -moveValue;
        moveDirection = new Vector2(0, yInput).normalized;
    }

    public void MoveUp()
    {
        StartCoroutine(OnMoveStop());

        yInput = moveValue;
        moveDirection = new Vector2(0, yInput).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * currentSpeed, moveDirection.y * currentSpeed);
    }
}
