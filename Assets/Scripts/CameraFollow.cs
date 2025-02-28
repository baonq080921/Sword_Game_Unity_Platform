using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Transform target;
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float yOffSet = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y + yOffSet,-10f );

        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.time);
    }
}
