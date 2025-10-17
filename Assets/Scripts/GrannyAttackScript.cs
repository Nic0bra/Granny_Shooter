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

    [Header("Aim Variables")]
    public Transform _indicator;
    public LayerMask aimColliderMask = new LayerMask();

    [Header("Charged Shot Variables")]
    [SerializeField] private float chargeGauge;
    public float chargeTimer = 1.5f;
    public bool isCharging;

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
        //------------------ Charging Actions -------------------
        if (_actions.Player.Attack.IsPressed())
        {
            isCharging = true;
        }
        else isCharging = false;

        if (isCharging)
        {
            chargeGauge += Time.deltaTime;
        }

        if (_actions.Player.Attack.WasReleasedThisFrame())
        {
            if(chargeGauge >= chargeTimer && grannyController.zoomedIn)
            {
                StartCoroutine(ChargedShot());
                chargeGauge = 0;
            }
            else chargeGauge = 0;
        }
        //----------------------------------------------------------

        //Aiming Controls
        Vector2 screenCenterPoint = new Vector2(Screen.width * .5f, Screen.height * .5f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit rayCastHit, 999f, aimColliderMask))
        {
            _indicator.position = rayCastHit.point;
            bulletSpawnPoint.LookAt(rayCastHit.point);
        }
    }

    IEnumerator RegularShot()
    {
        Rigidbody _shot;
        _shot = Instantiate(regularShot, 
            bulletSpawnPoint.position, 
            bulletSpawnPoint.rotation) as Rigidbody;

        _shot.AddForce(bulletSpawnPoint.forward *  bulletSpeed, ForceMode.Force);

        RumbleManager.Instance.RumblePulse(.25f, .25f, .1f);

        yield return null;
    }

    IEnumerator ChargedShot()
    {
        Rigidbody _shot;
        _shot = Instantiate(chargedShot,
            bulletSpawnPoint.position,
            bulletSpawnPoint.rotation) as Rigidbody;

        _shot.AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Force);

        RumbleManager.Instance.RumblePulse(.5f, .5f, .25f);

        yield return null;
    }
}
