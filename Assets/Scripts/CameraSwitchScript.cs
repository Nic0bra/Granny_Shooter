using UnityEngine;
using Unity.Cinemachine;

public class CameraSwitchScript : MonoBehaviour
{
    [SerializeField] GrannyController gCTRL;
    
    [Header("Cameras")]
    public CinemachineCamera platformCam;
    public CinemachineCamera shooterCam;

    private void Awake()
    {
        gCTRL = GameObject.FindGameObjectWithTag("Player").GetComponent<GrannyController>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gCTRL.zoomedIn)
        {
            platformCam.Priority = 0;
            shooterCam.Priority = 1;
        }
        else
        {
            platformCam.Priority = 1;
            shooterCam.Priority = 0;
        }
    }
}
