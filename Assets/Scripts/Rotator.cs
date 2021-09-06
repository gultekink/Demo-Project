using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    bool onHit;
    [SerializeField] private float _rotateToAmount;
    [SerializeField] private float _pushStrength;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * _rotateToAmount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        //Vector3 pushVector3 = collision.gameObject.transform.position - transform.position;

        Vector3 m_NewForce = new Vector3(-3.0f, 1.0f, 0.0f);
        rb.AddForce(m_NewForce * _pushStrength, ForceMode.Impulse);

    }
}
