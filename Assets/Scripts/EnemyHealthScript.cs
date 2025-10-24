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
            StartCoroutine(TakeDamage(25));
        }
        else if (other.gameObject.tag == "RegularBullet")
        {
            StartCoroutine(TakeDamage(50));
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag == "ChargedBullet")
        {
            StartCoroutine(TakeDamage(100));
        }
    }

    IEnumerator TakeDamage(int damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy health = " + enemyHealth);
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
