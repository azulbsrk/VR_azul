using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System.Threading;

public class Concurrencia : MonoBehaviour
{
    [Header("Activa los metodos")]
    public bool useSincrono;
    public bool useThread;
    public bool useCoroutine;
    public bool useTask;

    [Header("Cosas a mover")]
    public Transform sincroneSphere;
    public Transform threadSphere;
    public Transform coroutineSphere;
    public Transform taskSphere;
    public Transform mainCube;

    //Accion a ejecutar en hilo secundario
    private Queue<Action> mainThreadActions = new Queue<Action>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (useSincrono) MoveSincrono();
        if (threadSphere) MoveWithThread();
        if (coroutineSphere) StartCoroutine(MoveWithCoroutine());
        if (taskSphere) MovewithTask();

    }

    // Update is called once per frame
    void Update()
    {
        //Siempre el giro del cubo de referencia en hilo principal
        mainCube.Rotate(Vector3.up, 50 + Time.deltaTime);

        //Ejecuta las acciones en el hilo principal
        lock (mainThreadActions)
        {
            while (mainThreadActions.Count > 0)
            {
                mainThreadActions.Dequeue().Invoke();
            }
        }
    }

    public void MoveSincrono()
    {
        for (int i = 0; i <= 100; i++)
        {
            sincroneSphere.position += Vector3.right * 0.05f;
        }
        Thread.Sleep(50);
    }

    //Metodo con hilo secundario (PRIVATE)
    public void MoveWithThread()
    {
        new Thread(() =>
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                lock (mainThreadActions)
                {
                    mainThreadActions.Enqueue(() =>
                    {
                        threadSphere.position += Vector3.right * 0.05f;
                    });
                }
            }
        }).Start();
    }

    //Metodo asincrono con task private
    public async void MovewithTask()
    {
        await Task.Run(() =>
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(50);
                lock (mainThreadActions)
                {
                    mainThreadActions.Enqueue(() =>
                    {
                        taskSphere.position += Vector3.right * 0.05f;
                    });
                }
            }
        });
    }

    //Metodo para Coroutine Private
    public IEnumerator MoveWithCoroutine()
    {
        for (int i = 0; i <= 100; i++)
        {
            coroutineSphere.position += Vector3.right * 0.05f;
            yield return new WaitForSeconds(0.050f);
        }
    }
}
