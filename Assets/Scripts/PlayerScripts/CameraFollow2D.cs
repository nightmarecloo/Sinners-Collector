using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraFollow2D : MonoBehaviour {

	public float damping = 5f;
	public Vector2 offset = new Vector2(4f, 1f);
	public static bool faceLeft;
	private Transform player;
    
	void Start ()
	{
		offset = new Vector2(Mathf.Abs(offset.x), offset.y);
		FindPlayer(faceLeft);
	}

	public void FindPlayer(bool playerFaceLeft)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		if(playerFaceLeft)
		{
			transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
		}
		else
		{
			transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
		}
	}

	void Update ()
    {
        if (player)
		{
			Vector3 target;
			if(faceLeft)
			{
				target = new Vector3(player.position.x - offset.x, player.position.y + offset.y, transform.position.z);
			}
			else
			{
				target = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, damping * Time.deltaTime);
            transform.position = currentPosition;
        }
}    }