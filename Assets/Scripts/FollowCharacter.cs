using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{

    private bool followPlayer;
    public GameObject player;
    [SerializeField] private Transform[] view;
    [SerializeField] private float speed;
    private Transform _currentView;

    // Start is called before the first frame update
    void Start()
    {
        followPlayer = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (followPlayer)
        {
            transform.position = player.transform.position + new Vector3(0, 12.2f, -14.9f);
        }

    }
  

}