using System.Collections;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public int enemyHealth = 5;

    public SkinnedMeshRenderer _rend;
    public Material baseMat;
    public Material hitFlashMat;

    private void Start()
    {
        baseMat = _rend.material;
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "PlayerHitSphere")
        {
            StartCoroutine(TakeDamage());
        } 
    }

    IEnumerator TakeDamage()
    {
        enemyHealth--;
        RumbleManager.Instance.RumblePulse(.3f, .6f, .25f);
        if(enemyHealth > 0)
        {
            _rend.material = hitFlashMat;
            HitStopManager.Instance.DoHitStop(.1f);
            yield return new WaitForSeconds(.2f);
            _rend.material = baseMat;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
