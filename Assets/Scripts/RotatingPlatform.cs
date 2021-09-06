using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField]  float pushPower;
    private Rigidbody _rigidbody;

    public float rotateToAmount = 1;

    void Start()
    {
        
            _rigidbody = GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        
            transform.Rotate(Vector3.forward * rotateToAmount);
    }

    //private void LeftAddForce()
    //{
    //    Vector3 velocity = _rigidbody.velocity;
    //    velocity.x = (Vector3.left * pushPower).x;
    //    _rigidbody.velocity = velocity;
    //}

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRigibody = collision.gameObject.GetComponent<Rigidbody>();

        Vector3 velocity = enemyRigibody.velocity;
        velocity.x = (Vector3.left *pushPower).x;
        enemyRigibody.velocity = velocity;

    }
}
