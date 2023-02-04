using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;

    private Vector3 Offset;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        Offset = transform.position - Player.transform.position;
    }

    // 
    void LateUpdate()
    {
        transform.position = Player.transform.position + Offset;
    }
}
