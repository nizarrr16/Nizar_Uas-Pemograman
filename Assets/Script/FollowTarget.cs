using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FollowTarget : MonoBehaviour
{
    public Transform player;
    public Transform Bg1;

    // Use this for initialization 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (player.position.x != transform.position.x && player.position.x > 0 && player.position.x < 22f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 0.1f);
        }

        Bg1.transform.position = new Vector2(transform.position.x * 1.0f, Bg1.transform.position.y);
    }
}
