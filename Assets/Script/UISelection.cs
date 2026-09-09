using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Rendering;


public class NewMonoBehaviourScript : MonoBehaviour
{
    public static bool gazedAt;
    public float filltime = 5f;
    public Image radialImage;
    public UnityEvent onFillComplete; //Evento genérico cuando termine la carga

    private Coroutine fillCoroutine; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gazedAt = false;
        radialImage.fillAmount = 0f;

    }

    public void OnPointerEnter()
    {
        gazedAt = true;
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);//Detener cualquier corutina anterior

        }
        fillCoroutine = StartCoroutine(FillRadial()); //iniciar el llenado   
    }
    public void OnPointerExit()
    {
        gazedAt = false;
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);//Detener cualquier corutina anterior
            fillCoroutine = null;   
        }
        fillCoroutine = StartCoroutine(FillRadial()); //iniciar el llenado   
    }
    private IEnumerator FillRadial()
    {
        float elapsedTime = 0f;
        while (elapsedTime < filltime)
        {
            if(!gazedAt ) //si deja de ser observado
            {
            yield break; //salir de la corutina
            }
            elapsedTime += Time.deltaTime;
         
            radialImage.fillAmount = Mathf.Clamp01(elapsedTime/filltime);//clamp va a dar el valor
            yield return null;  
        }
        //Ejecuta ek evento onFillComplete cuando se completa el llenado final
        onFillComplete?.Invoke();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
