using UnityEngine;

public class Lamp : MonoBehaviour
{
    public float swingAngle = 5f;
    public float swingSpeed = 1f;

    private float timeOffset;

    void Start()
    {
        timeOffset = Random.Range(0f, 10f);
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * swingSpeed + timeOffset) * swingAngle;
        transform.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }
}
