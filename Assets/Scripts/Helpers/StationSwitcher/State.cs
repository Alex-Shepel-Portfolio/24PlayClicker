using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
  protected IStationstateSwitcher stateSwitcher;
  protected bool isNeedUpdate;
  
  protected State(IStationstateSwitcher stateSwitcher, bool isNeedUpdate)
  {
    this.stateSwitcher = stateSwitcher;
    this.isNeedUpdate = isNeedUpdate;
  }

  public abstract void Start();

  public abstract void Stop();

  public abstract void GetOtherState(int stateIndex);

  public virtual void SetUpdateStatus(bool isNeedUpdate)
  {
  }
  
  public virtual void Update()
  {
  }
  
}
