using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public float Force;
    public float RopeForce;

    public bool IsHooking {
        get {
            return _IsHooking;
        }
        set {
            _IsHooking = value;
            lineRenderer.enabled = IsHooking;
        }
    }

    private bool _IsHooking;

    private LineRenderer lineRenderer;
    private Rigidbody2D rb2d;

    private Camera mainCam;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        mainCam = Camera.main;

        if(lineRenderer == null)
        {
            Debug.LogError("Line renderer missing!");
            enabled = false;
        }

        lineRenderer.enabled = false;
    }

    Vector3 direction;
    Vector3 mousePos;

    public void Hooked()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit;

        if ((hit = Physics2D.Raycast(mousePos, Vector3.forward, Mathf.Infinity)))
        {
            print($"{hit.collider.gameObject.name}");
            if (hit.collider.gameObject.GetComponent<BoxCollider2D>() == null)
            {
                return;
            }
        } else
        {
            return;
        }
        IsHooking = true;

        direction = mousePos - transform.position;
        direction /= direction.magnitude;
        rb2d.AddForce(direction * Force);
    }

    float timeAt = 0f;

    private void Update()
    {
        if (IsHooking)
        {
            Vector2 playerPos = transform.position;
            playerPos.y += .8f;

            timeAt += RopeForce * Time.deltaTime;
            lineRenderer.SetPosition(0, Vector3.Lerp(transform.position, mousePos, timeAt));
            lineRenderer.SetPosition(1, playerPos);

            if ((mousePos - transform.position).sqrMagnitude > 0.2f) return;

            rb2d.AddForce(direction * Force * 0.5f);
        } else
        {
            timeAt = 0f;
        }
    }

    public void Unhook()
    {
        if (!IsHooking) return;

        IsHooking = false;

        //rb2d.velocity = new Vector2(rb2d.velocity.x, Force * 0.3f * Time.deltaTime);
    }
}
