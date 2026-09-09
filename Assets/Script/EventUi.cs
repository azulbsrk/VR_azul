using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
public class EventUi : MonoBehaviour
{
    public List<GameObject> objects; //lista de objetos
    public List<string> menssages; // lista de mensajes a mostrar
    public int currentIndex = 0;
    public TextMeshProUGUI textMeshPro; //Componente de texto en el objeto


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateVisibility();   
        UpdateText();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Metodo para el ciclo de objetos

    public void CycleObjects()
    { 
    //incrementa el indice y vuelve al principio si es necesario

        currentIndex=(currentIndex+1)%objects.Count;
        //Actualiza la visibilidad de los objetos
        UpdateVisibility();


    }

    private void UpdateVisibility()
    {
        for (int i = 0; i < objects.Count; i++)
        { 
            //solo el objeto en el indice actual es visible
            objects [i].SetActive(i==currentIndex);
        }
    }

    //metodo para ciclo de textos
    public void CycleText()
    {
        currentIndex = (currentIndex + 1) % menssages.Count;
        UpdateText();
    }

    private void UpdateText()
    {
        if (menssages.Count > 0 && textMeshPro != null)
        {
            textMeshPro.text = menssages[currentIndex];
        }
    }
    //Cambiar scena por nobre
    public void ChangeSceneByName(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeSceneByIndex(string sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadCurrentScene()
    { 
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void Exit()
    {

        Application.Quit();
        Debug.Log("salio del juego ");

    }
}
