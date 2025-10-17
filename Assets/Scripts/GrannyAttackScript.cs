using System.Collections;
using UnityEngine;

public class GrannyAttackScript : MonoBehaviour
{
    [SerializeField] Granny_Input _actions;
    [SerializeField] GrannyController grannyController;

    [Header("Bullet Variables")]
    public float bulletSpeed = 200f;
    public Rigidbody regularShot;
    public Rigidbody chargedShot;
    public Transform bulletSpawnPoint;

    //input actions need an awake function
    private void Awake()
    {
        _actions = new Granny_Input();
        grannyController = GetComponent<GrannyController>();
    }

    private void OnEnable()
    {
        _actions.Enable();
    }

    private void OnDisable()
    {
        _actions.Disable();
    }

    private void Update()
    {
        if (_actions.Player.Attack.triggered)
        {
            if (grannyController.zoomedIn)
            {
                StartCoroutine(RegularShot());
            }
        }
    }

    IEnumerator RegularShot()
    {
        Rigidbody _shot;
        _shot = Instantiate(regularShot, 
            bulletSpawnPoint.position, 
            bulletSpawnPoint.rotation) as Rigidbody;

        _shot.AddForce(bulletSpawnPoint.forward *  bulletSpeed, ForceMode.Force);

        yield return null;
    }
}
