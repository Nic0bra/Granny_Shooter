using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    GrannyController grannyController;
    GrannyAttackScript grannyAttack;
    public GameObject aimPanel;

    //Charge Meter Setup
    public Image chargeIcon;
    public float chargeMeter;
    public float chargeTimer = 2;

    private void Awake()
    {
        grannyController = GameObject.FindGameObjectWithTag("Player").
            GetComponent<GrannyController>();

        grannyAttack = GameObject.FindGameObjectWithTag("Player").
            GetComponent<GrannyAttackScript>();
    }
    private void Update()
    {
        if (grannyController.zoomedIn)
            aimPanel.SetActive(true);

        else
            aimPanel.SetActive(false);

        chargeMeter = grannyAttack.chargeGauge - .5f;
        chargeIcon.fillAmount = chargeMeter;

        if(chargeIcon.fillAmount < 1)
            chargeIcon.color = Color.darkRed;

        else
            chargeIcon.color = Color.darkSeaGreen;
    }
}
