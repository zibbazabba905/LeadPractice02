using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDeath : MonoBehaviour
{
    public float FadeTime { get; set; } = 0.1f;

    private Rigidbody Rigidbody;
    private Collider Collider;
    private CCMovment CCMovment;
    private MovementLogic MovementLogic;
    private CharacterController CharacterController;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        CCMovment = GetComponent<CCMovment>();
        MovementLogic = GetComponent<MovementLogic>();
        CharacterController = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        MakeInert();
        FadeTargetObjects();
    }
    private void MakeInert()
    {
        Destroy(MovementLogic);
        Destroy(Rigidbody);
        Destroy(Collider);
        Destroy(CCMovment);
        Destroy(CharacterController);
    }
    private void FadeTargetObjects()
    {
        Renderer[] rendList = transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer part in rendList)
        {
            StartCoroutine(FadeOut(part));
        }
    }

    private IEnumerator FadeOut(Renderer rend)
    {
        Color initialColor = rend.material.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = 0f;
        while (elapsedTime < FadeTime)
        {
            elapsedTime += Time.deltaTime;
            rend.material.color = Color.Lerp(initialColor, targetColor, elapsedTime / FadeTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}