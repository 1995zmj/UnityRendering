
using System;
using UnityEngine;
using System.Collections.Generic;
public class Schedule
{
	private bool running;
	private bool paused;
	private bool stopped;

	private Action _action;
 	private int _iterations;   //事件的迭代（执行次数）  
	private float _interval;   //执行时间间隔  
	private float _dueTime;    //下一个事件执行的时间 DueTime = Time.time + Time.deltaTime  

	public bool IsReclaimed { get; set; }
	public bool GameUpdate()
	{
		//暂停
		if (running != true)
			return true;
		
		_dueTime += Time.deltaTime;
		if (_dueTime >= _interval)
		{
			_dueTime -= _interval;
			_iterations++;
			if (_action != null)
			{
				_action();
			}
			
		}
		
		return true;
	}

	public void Initialize(float interval, Action action)
	{
		_iterations = 0;
		_dueTime = 0;
		_interval = interval;
		_action = action;
	}
    	
	public void Pause()
	{
		paused = true;
	}
    	
	public void Unpause()
	{
		paused = false;
	}
    	
	public void Start()
	{
		running = true;
	}
    	
	public void Stop()
	{
		stopped = true;
		running = false;
	}
}
public class ScheduleManager : MonoBehaviour
{
    static ScheduleManager singleton;
    
    private List<Schedule> schedules;

    private void Update()
    {
	    for (int i = 0; i < schedules.Count; i++)
	    {
		    if (!schedules[i].GameUpdate())
		    {
			    // 预留取消
		    }
	    }
    }

    public static Schedule CreateSchedule(float interval, Action action)
    {
    	if(singleton == null) {
    		GameObject go = new GameObject("ScheduleManager");
    		singleton = go.AddComponent<ScheduleManager>();
            singleton.schedules = new List<Schedule>();
    		DontDestroyOnLoad(singleton);
    	}

        var schedule = new Schedule();
        singleton.schedules.Add(schedule);
        schedule.Initialize(interval,action);
        return schedule;
    }
}
