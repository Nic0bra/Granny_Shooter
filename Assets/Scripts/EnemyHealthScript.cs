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
        
    }

    IEnumerator TakeDamage()
    {
        enemyHealth--;
        if(enemyHealth > 0)
        {
            _rend.material = hitFlashMat;
            yield return new WaitForSeconds(.2f);
            _rend.material = baseMat;
        }
        
    }
}
