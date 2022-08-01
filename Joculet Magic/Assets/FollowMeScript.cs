using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMeScript : MonoBehaviour
{
    public GameObject player;
    public int Offset;
    public float OffsetSmoothness;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, playerPosition, 4 * OffsetSmoothness * Time.deltaTime);
    }
}
