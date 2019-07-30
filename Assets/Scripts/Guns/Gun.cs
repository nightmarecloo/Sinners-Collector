using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    new public Rigidbody2D rigidbody;
    public SpriteRenderer sprite;

    public void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void PhysicsKinematic()
    {
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void PhysicsDynamic()
    {
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
