﻿using System.Diagnostics;
using Debug=UnityEngine.Debug;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public void ScaleUpWithBlocking()
    {
        transform.localScale = Vector3.one;
        Debug.Log("[Target] Start ScaleUpWithBlocking");
        for (int i = 2; i < 5; i++)
        {
            transform.localScale = new Vector3(i, i, i);
            Thread.Sleep(500);
        }
        Debug.Log("[Target] Finished ScaleUpWithBlocking");
    }

    public IEnumerator ScaleUpWithNonBlocking()
    {
        transform.localScale = Vector3.one;
        Debug.Log("[Target] Start ScaleUpWithNonBlocking");
        yield return null;
        for (int i = 2; i < 5; i++)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("[Target] Finished ScaleUpWithNonBlocking");
    }

    public bool ScaleUpWithSync()
    {
        transform.localScale = Vector3.one;
        Debug.Log("[Target] Start ScaleUpWithBlocking");
        for (int i = 2; i < 5; i++)
        {
            transform.localScale = new Vector3(i, i, i);
            Thread.Sleep(500);
        }
        Debug.Log("[Target] Finished ScaleUpWithBlocking");
        return true;
    }

    public void ScaleUpWithAsync(Action onCompleted)
    {
        transform.localScale = Vector3.one;
        Debug.Log("[Target] Start ScaleUpWithBlocking");
        for (int i = 2; i < 5; i++)
        {
            transform.localScale = new Vector3(i, i, i);
            Thread.Sleep(500);
        }
        Debug.Log("[Target] Finished ScaleUpWithBlocking");
        onCompleted();
    }

    
    public void ScaleUpWithNonBlockingAndThread()
    {
        transform.localScale = Vector3.one;
        Thread scaleUpThread = new Thread(() => {
            Debug.Log("[Target] Start ScaleUpWithNonBlocking2");
            for (int i = 2; i < 5; i++)
            {
                Debug.Log(i);
                // transform.localScale = new Vector3(i, i, i);
                Thread.Sleep(500);
            }
            Debug.Log("[Target] Finished ScaleUpWithNonBlocking2");
        });
        scaleUpThread.IsBackground = false;
        scaleUpThread.Start();
    }

    public void ScaleUpWithNonBlockingAndTask()
    {
        transform.localScale = Vector3.one;
        Task.Run(() => {
            Debug.Log("[Target] Start ScaleUpWithNonBlocking3");
            for (int i = 2; i < 5; i++)
            {
                Debug.Log(i);
                // transform.localScale = new Vector3(i, i, i);
                Thread.Sleep(500);
            }
            Debug.Log("[Target] Finished ScaleUpWithNonBlocking3");
        });
    }

    void Start()
    {
        StartCoroutine(CoroutinesCompletedTest());
    }

    public IEnumerator CoroutinesCompletedTest()
    {        
        bool isCompletedA = false;
        bool isCompletedB = false;
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        StartCoroutine(CoroutineA(() => isCompletedA = true));
        StartCoroutine(CoroutineB(() => isCompletedB = true));
        yield return new WaitUntil(() => isCompletedA && isCompletedB);
        Debug.Log("종료 : " + stopWatch.Elapsed.Seconds);

    }

    private IEnumerator CoroutineA(Action onCompleted)
    {
        yield return new WaitForSeconds(4f);
        onCompleted();
    }
    private IEnumerator CoroutineB(Action onCompleted)
    {
        yield return new WaitForSeconds(3f);
        onCompleted();
    }
}
