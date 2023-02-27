using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            target = GameManager.instance.player.transform;
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
