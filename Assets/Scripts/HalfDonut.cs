using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{
    private Vector3 startPosition;
    public GameObject halfDonut;
    [SerializeField]  float frequency = 5f;
    [SerializeField] float magnitude = 5f;
    [SerializeField] float offset = 5f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        halfDonut.transform.position = startPosition + transform.right * Mathf.Sin(Time.time * frequency * offset) * magnitude;
    }
}
