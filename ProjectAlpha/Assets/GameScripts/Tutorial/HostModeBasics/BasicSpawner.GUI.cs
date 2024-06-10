using Fusion;
using UnityEngine;

namespace Tutorial
{
    public partial class BasicSpawner
    {
        private void OnGUI()
        {
            if (_runner == null)
            {
                var x = Screen.width - 200;

                if (GUI.Button(new Rect(x, 0, 200, 40), "Host"))
                {
                    StartGame(GameMode.Host);
                }

                if (GUI.Button(new Rect(x, 40, 200, 40), "Join"))
                {
                    StartGame(GameMode.Client);
                }
            }
        }
    }
}