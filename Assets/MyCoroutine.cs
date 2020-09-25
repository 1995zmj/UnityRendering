using System.Collections;
using UnityEngine;

public class MyCoroutine : MonoBehaviour
{
    private int i;
    void Start()
    {
        var t = ScheduleManager.CreateSchedule(1, () =>
        {
            Debug.Log("SSS");
        });
        t.Start();
        // i = 0;
        // StartCoroutine(Thread1());
        // var tempGameObject = new GameObject("zmj");
        // UnityEngine.Object.DontDestroyOnLoad(tempGameObject);
        // var gameobjct = GameObject.Find("zmj");
        // gameobjct.SetActive(false);
    }

    void Update()
    {
        
    }

    IEnumerator Thread1()
    {
        while(true){
            Debug.Log("i="+ i);
            i++;
            if(i >= 10)
            {
                
                break;
            }
            //等待一面
            yield return StartCoroutine(Thread2());
        }
    }

    IEnumerator Thread2()
    {
        Debug.Log("show idle");
        yield return null;
    }
}
