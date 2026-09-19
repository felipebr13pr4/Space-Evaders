using UnityEngine;

public class Shooting : MonoBehaviour
{
    public virtual void ShootBullet(GameObject bulletObj)
    {
        if (!bulletObj.activeInHierarchy) bulletObj.SetActive(true);
        bulletObj.transform.position = transform.position;
    }
}