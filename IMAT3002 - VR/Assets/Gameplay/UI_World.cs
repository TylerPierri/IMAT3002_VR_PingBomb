
using UnityEngine;

public class UI_World : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void FixedUpdate()
    {
        gameObject.transform.LookAt(player.transform);
    }
}
