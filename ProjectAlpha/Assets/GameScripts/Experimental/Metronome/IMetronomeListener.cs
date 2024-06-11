namespace ProjectAlpha
{
    public interface IMetronomeListener
    {
        void OnHeartbeat();
        void OnPlay();
        void OnPause();
        void OnResume();
        void OnStop();
    }
}