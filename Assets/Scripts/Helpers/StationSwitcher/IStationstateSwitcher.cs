public interface IStationstateSwitcher
{
    void SwitchState<T>() where T : State;
}
