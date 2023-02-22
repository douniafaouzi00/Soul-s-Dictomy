using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ParticlesPlayer
{
    [SerializeField]
    private GameObject damageEffect;
    [SerializeField]
    private GameObject healEffect;

    public void DamageEffect(Transform tranf)
    {
        Effect(tranf, damageEffect);
    }

    public void HealEffect(Transform tranf)
    {
        Effect(tranf, healEffect);
    }

    private void Effect(Transform tranf, GameObject effect)
    {
        GameObject go = MonoBehaviour.Instantiate(effect, tranf.position, Quaternion.identity, tranf);
        go.transform.localPosition = effect.transform.position;
        MonoBehaviour.Destroy(go, 1f);
    }
}
