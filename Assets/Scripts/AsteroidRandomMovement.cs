using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRandomMovement : MonoBehaviour
{
    public bool AsteroidMovement = false;

    [SerializeField] float minRotSpeed = 1f;
    [SerializeField] float maxRotSpeed = 5f;
    float rotSpeed;
    Vector3 pos;

    [SerializeField] float minVelocity = 0.1f;
    [SerializeField] float maxVelocity = 0.25f;

    private void Start()
    {
        InitRotation();

        if (AsteroidMovement)
        {
            ExecuteMovement();
        }
    }

    private void Update()
    {
        ExecuteRotation();
    }

    void ExecuteMovement()
    {
        float velocity = Random.Range(minVelocity, maxVelocity);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * velocity, ForceMode.Impulse);
    }

    void InitRotation()
    {
        pos = this.gameObject.GetComponent<Renderer>().bounds.center;
        rotSpeed = Random.Range(minRotSpeed, maxRotSpeed);
    }

    void ExecuteRotation()
    {
        this.transform.RotateAround(pos, Vector3.up, rotSpeed * Time.deltaTime);
    }
}
