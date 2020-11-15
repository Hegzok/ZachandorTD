using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextUI : MonoBehaviour
{
    [SerializeField]
    DamagePopUp damagePopUpPrefab;

    public void ReturnToPool()
    {
        DataStorage.DamagePopUpPool.ReturnToPool(damagePopUpPrefab);
    }
}
