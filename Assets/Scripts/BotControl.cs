using System.Collections;
using UnityEngine;


public class BotControl : MonoBehaviour
{
    public float angle;
    [SerializeField] private float _speed;
    [SerializeField] private float _pushPower; 
    public Animator animator;
    public float[] random;
    public bool _onPlatform;
    public bool isMoving;
    private Rigidbody _rigidbody;
    private Vector3 _startPos;
    private bool isGameOver;
    private bool isGameStart;

    // Start is called before the first frame update
    void Start()
    {
        isGameStart = false;
        isGameOver = false;
        _rigidbody = GetComponent<Rigidbody>();
        isMoving = true;
        _onPlatform = false;
        animator = GetComponent<Animator>();
        _startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            isGameStart = true;
        }

        if (Input.GetMouseButton(0))
        {
            isGameStart = true;
        }


        if (!isGameOver)
        {
            if (isGameStart)
            {

                if (isMoving)
                {
                    transform.Translate(Vector3.forward * _speed * Time.deltaTime);
                }

                RayCasting();

                if (_onPlatform)
                {
                    Vector3 velocity = _rigidbody.velocity;
                    velocity.x = (Vector3.right * Random.Range(random[0], random[1])).x;
                    _rigidbody.velocity = velocity;
                }

                if (transform.position.y < -7)
                {
                    BeginStart();
                }

                if (transform.position.x > 6)
                {
                    _rigidbody.rotation = Quaternion.Euler(0, -50f, 0);
                    StartCoroutine(changeRotation(1.5f));
                }

            }
        }
    }

    void RayCasting()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + (Vector3.up * 2), transform.forward, out hit, 3f))
        {
            if (hit.collider.tag == "HorizontalObstacle")
            {
                isMoving = false;
                animator.SetBool("moving",false);
            }
        }
        else
        {
            isMoving = true;
            animator.SetBool("moving", true);
        }

        if (Physics.Raycast(transform.position + (Vector3.up* angle), transform.forward, out hit, 12f))
        {
            if (hit.collider.name == "Half_Donut_01")
            {
                _rigidbody.rotation = Quaternion.Euler(0, -50f, 0);
                StartCoroutine(changeRotation(1.5f));
            }
        }
        if (Physics.Raycast(transform.position + (Vector3.up * angle), transform.forward, out hit, 5f))
        {
            
            if (hit.collider.name == "Half_Donut_01_01" || hit.collider.name == "Half_Donut_Stick")
            {
                _rigidbody.rotation = Quaternion.Euler(0, -90f, 0);
                StartCoroutine(changeRotation(3.4f));
            }
        }
        if (Physics.Raycast(transform.position + (Vector3.up * angle), transform.forward, out hit, 5f))
        {

            if (hit.collider.name == "Half_Donut_01_01" || hit.collider.name == "Half_Donut_Stick_2")
            {
                _rigidbody.rotation = Quaternion.Euler(0, 90f, 0);
                StartCoroutine(changeRotation(2.4f));
            }
        }
        if (Physics.Raycast(transform.position + (Vector3.up * angle), transform.forward, out hit, 5f))
        {
          
            if (hit.collider.name == "StaticObstacle")
            {
                _rigidbody.rotation = Quaternion.Euler(0, -75f, 0);
                StartCoroutine(changeRotation(1.5f));
            }
        }
        if (Physics.Raycast(transform.position + (Vector3.up * angle), transform.forward, out hit, 5f))
        {

            if (hit.collider.name == "StaticObstacle (3)")
            {
                _rigidbody.rotation = Quaternion.Euler(0, 60f, 0);
                StartCoroutine(changeRotation(1.5f));
            }
        }
        if (Physics.Raycast(transform.position + (Vector3.up * angle), transform.forward, out hit, 5f))
        {

            if (hit.collider.name == "StaticObstacle (2)")
            {
                _rigidbody.rotation = Quaternion.Euler(0, -60f, 0);
                StartCoroutine(changeRotation(1.5f));
            }
        }
        if (Physics.Raycast(transform.position + (Vector3.up * angle), transform.forward, out hit, 5f))
        {

            if (hit.collider.name == "Half_Donut_01_02" || hit.collider.name == "Half_Donut_Stick_3")
            {
                _rigidbody.rotation = Quaternion.Euler(0, -90, 0);
                StartCoroutine(changeRotation(2.3f));
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.tag == "HorizontalObstacle")
        {
            BeginStart();
        }
        if (collision.gameObject.tag == "StaticObstacle")
        {
            BeginStart();
            
        }
        if (collision.gameObject.tag == "HalfDonut")
        {
            BeginStart();

        }
        if (collision.gameObject.tag == "Rotator")
        {
            BeginStart();
        }

        if (collision.gameObject.tag == "Platform")
        {
            _onPlatform = true; 
            _rigidbody.rotation = Quaternion.identity;
            //Change rotation after collision
            _rigidbody.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            _onPlatform = false;
            //Change rotation after collision
            _rigidbody.rotation = Quaternion.Euler(0, 0, 0);
        }
        //if (collision.gameObject.name == "RotatingPlatform 3")
        //{
        //    Vector3 velocity = _rigidbody.velocity;
        //    velocity.x = (Vector3.right * 2.4f).x;
        //    _rigidbody.velocity = velocity;
            
        //}
        if (collision.gameObject.name == "FinishLine")
        {
            StartCoroutine(FinishGame(0.3f));

        }
        if (collision.gameObject.name == "RotatingPlatform 3")
        {
            _rigidbody.rotation = Quaternion.Euler(0, 30, 0);
            StartCoroutine(changeRotation(1));
        }
    }

    public void BeginStart()
    {
        transform.position = _startPos;
    }

    IEnumerator changeRotation(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _rigidbody.rotation = Quaternion.Euler(0, 0, 0);
    }
    IEnumerator FinishGame(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("moving", false);
        isGameOver = true;
    }
}
