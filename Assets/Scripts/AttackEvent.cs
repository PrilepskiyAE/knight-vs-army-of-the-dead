using Unity.Mathematics;
using UnityEngine;

public class AttackEvent : MonoBehaviour
{
    [SerializeField]
    private InfoPlayer infoPlayer = null;
    [SerializeField]
    private float damage = 3;
    [SerializeField] private GameObject prefabToSpawn;

    void AttackAction()
    {
        if (infoPlayer != null)
        {
            infoPlayer.Damage(damage);
        }
    }

    void ShortAction()
    {
        GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        spawnedObject.transform.SetParent(transform);
        Arrow arrowScript = spawnedObject.GetComponent<Arrow>();

        if (arrowScript != null)
        {
            SpriteRenderer parentRenderer = GetComponent<SpriteRenderer>();
            if (parentRenderer != null)
            {
                arrowScript.SetFlipDirection(parentRenderer.flipX);
            }
        }

        spawnedObject.transform.SetParent(null);
    }

}

