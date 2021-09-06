using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VR;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject paintWallObject;
    [SerializeField] GameObject pivotObject;
    [SerializeField] private float _speed;
    [SerializeField] private float _pushPower; 
    private float _rotateSpeed = 0.32f;
    private PaintWall paintWall;
    public Transform[] views;
    public float transitionSpeed;
    private Transform currentview;
    public bool cam2;
    Vector3 originalPos;
    public bool restart;
    public bool camCollider;
    public bool camCollider2;
    public Animator animator;
    public bool onPlatform;
    private Rigidbody _rigidbody;
    private Quaternion _rotationY;
    private bool isGameOver;
    private bool istouching;
    private bool isMousetouching;
    public Vector3 myCamPos = Vector3.zero;
    private Vector3 lastMousePosition;

    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        isMousetouching = true;
        istouching = false;
        isGameOver = false;
        myCamPos = transform.position;
        cam2 = false;
        restart = false;
        _rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        animator.SetBool("isRunning", false);
        onPlatform = false;
        camCollider = false;
    }

    void FixedUpdate()
    {
        if (!isGameOver)
            {
                if (Input.touchCount > 0)
                {
                    istouching = true;
                    isMousetouching = false;
                    transform.Translate(Vector3.forward * _speed * Time.deltaTime);

                    Touch _touch = Input.GetTouch(0);

                    if (_touch.phase == TouchPhase.Moved)
                    {
                        _speed = 5;
                         _rotateSpeed = 0.28f;
                        _rotationY = Quaternion.Euler(0f, -_touch.deltaPosition.x * _rotateSpeed, 0f);
                        _rigidbody.rotation = _rotationY * transform.rotation;
                        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        animator.SetBool("isRunning", true);
                    }
                }
                else
                {
                    animator.SetBool("isRunning", false);
                    _speed = 0;
                }
            }

            if (transform.position.y < -9f)
            {
                RestartGame();
            }


            if (!isGameOver)
            {
                if (Input.GetMouseButton(0))
                {
                    if (!istouching && isMousetouching)
                    {
                        _speed = 5;
                        transform.Translate(Vector3.forward * _speed * Time.deltaTime);


                        _rotateSpeed = 0.004f;


                        if (Input.mousePosition != lastMousePosition)
                        {
                            lastMousePosition = Input.mousePosition;
                            ChangeRotation();
                        }

                        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        animator.SetBool("isRunning", true);
                    }
                }

                else
                {
                    animator.SetBool("isRunning", false);
                    _speed = 0;
                }

                if (transform.position.y < -9f)
                {
                    RestartGame();
                }
            }
    }

    void ChangeRotation()
    {
        _rotationY = Quaternion.Euler(0f, -Input.mousePosition.x * _rotateSpeed, 0f);
        _rigidbody.rotation = _rotationY * transform.rotation;
    }

    public void RestartGame()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        _rotationY = Quaternion.Euler(0f, 0f, 0f);
        _rigidbody.rotation = _rotationY;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            onPlatform = true;

        }
        else

        {
            onPlatform = false;
        }

        if (collision.gameObject.tag == "HorizontalObstacle")
        {
            RestartGame();
        }
        if (collision.gameObject.tag == "StaticObstacle")
        {
            RestartGame();
        }
        if (collision.gameObject.tag == "HalfDonut")
        {
            RestartGame();
        }
        if (collision.gameObject.tag == "Rotator")
        {
            RestartGame();
        }
        if (collision.gameObject.name == "FinishLine")
        {

            StartCoroutine(FinishGame(0.3f));

        }

        if (collision.gameObject.name == "Platform (3)")
        {
            cam2 = true;
        }
    }

    IEnumerator FinishGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("isRunning", false);
        isGameOver = true;
    }
}
