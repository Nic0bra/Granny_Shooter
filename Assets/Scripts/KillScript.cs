using UnityEngine;

public class KillScript : MonoBehaviour
{
    public float killTime = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, killTime);
    }
}
