using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispersNodeAnimation : MonoBehaviour
{   
    public float rotationSpeed1 = 1f;
    public float radius = 1f;
    public float rotationSpeed2 = 1f;
    public float amplitude = 1f;

    private float angle1 = 0;
    private float angle2 = 0;
    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        angle1 += rotationSpeed1 * Time.deltaTime;
        angle2 += rotationSpeed2 * Time.deltaTime;

        Vector3 offset1 = Quaternion.Euler(0, angle1, 0) * (new Vector3(0,0,1) * radius);
        Vector3 offset2 = new Vector3(0, amplitude * Mathf.Sin(angle2), 0);
        transform.localPosition = originalPosition + offset1 + offset2;
    }
}
