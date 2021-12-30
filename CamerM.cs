using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerM : MonoBehaviour
{
    [SerializeField]
    private float fosx;
    [SerializeField]
    private float fosy;
    [SerializeField]
    private float fosz;
    public Player player;
    private Vector3 camerafos;
    [SerializeField]
    private float speed;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    private void LateUpdate()
    {
        camerafos.x = player.transform.position.x + fosx;
        camerafos.y = player.transform.position.y + fosy;
        camerafos.z = fosz;
        transform.position = Vector3.Lerp(transform.position, camerafos, speed * Time.deltaTime); 
    }
}
