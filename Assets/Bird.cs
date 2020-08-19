using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPosition;
    private float sittingAround;
    private bool birdWasLaunched;

    [SerializeField] float _addForce = 250;

    private void Awake()
    {
        _initialPosition = transform.position;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void Update()
    {
        if(birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            sittingAround += Time.deltaTime;
        }

        if(transform.position.x > 25 || transform.position.x < -35 || transform.position.y < -5 || sittingAround > 2.5)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _addForce);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        birdWasLaunched = true;
        GetComponent<LineRenderer>().enabled = false;

    }

    private void OnMouseDrag()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = newPosition;
        GetComponent<LineRenderer>().enabled = true;
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
    }
}
