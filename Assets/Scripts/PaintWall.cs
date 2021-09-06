using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaintWall : MonoBehaviour
{
   
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject Brush;
    [SerializeField] private float brushSize;

    private Player _player;
    private bool _touchOnWall;

    // Use this for initialization
    void Start()
    {
        //_player = _player.GetComponent<Player>();
        _touchOnWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && _touchOnWall)
            {
                var paint = Instantiate(Brush, hit.point + Vector3.up * 0.1f, Quaternion.Euler(0, -90, 90), transform);
                paint.transform.localScale = Vector3.one * brushSize;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (hit.collider.gameObject.tag == "Plane")
                {
                    _touchOnWall = true;
                }
                else
                {
                    _touchOnWall = false;
                }
            }
        }
    }
}
