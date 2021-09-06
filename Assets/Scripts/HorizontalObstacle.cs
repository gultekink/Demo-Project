using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField]  Vector3 vector3;
    float maxValue = 6f;
    float minValue = -6.5f; 
    float currentValue; 
    float direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 vector3 = new Vector3(Random.Range(-6.5f, 6),this.vector3.y,this.vector3.z);
        gameObject.transform.position = vector3;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, rotateSpeed, 0, Space.Self);

        transform.RotateAround(transform.position, transform.up, Time.deltaTime * rotateSpeed);

        currentValue += Time.deltaTime * direction * speed; 

        if (currentValue >= maxValue)
        {
            direction *= -1;
            currentValue = maxValue;
        }
        else if (currentValue <= minValue)
        {
            direction *= -1;
            currentValue = minValue;
        }

        vector3.x = currentValue;
        transform.position = vector3;
    }
}
