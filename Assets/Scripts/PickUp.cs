using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] private Weapon pickUp;

    [SerializeField] private GameObject explosionEffect;

    private void Awake()
    {
        Player.OnNukeActivated.AddListener(OnNuked);
    }

    private void OnDestroy()
    {
        Player.OnNukeActivated.RemoveListener(OnNuked);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.CompareTag("Player"))

        {
            PickMeUp(collision.attachedRigidbody.GetComponent<Player>());
        }
    }
    protected abstract void PickMeUp(Player playerInTrigger);

    private void OnNuked()
    {
        PlayExplosionEffect();
        Destroy(gameObject);
    }
    private void PlayExplosionEffect()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }
}

