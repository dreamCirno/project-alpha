using Cysharp.Threading.Tasks;
using TEngine;
using UnityEngine;
using AudioType = TEngine.AudioType;

namespace KyuuGames
{
    public class BattleSystem : BehaviourSingleton<BattleSystem>
    {
        private enum ESteps
        {
            None,
            Ready,
            Spawn,
            WaitSpawn,
            WaitWave,
            GameOver,
        }

        private ESteps _steps = ESteps.None;

        /// <summary>
        /// 加载房间
        /// </summary>
        public async UniTaskVoid LoadRoom()
        {
            await UniTask.Yield();

            // 加载背景音乐
            GameModule.Audio.Play(AudioType.Music, "music_background", true);

            // 监听游戏事件
            GameEvent.AddEventListener<Vector3, Quaternion>(ActorEventDefine.PlayerDead, OnPlayerDead);
            GameEvent.AddEventListener<Vector3, Quaternion>(ActorEventDefine.EnemyDead, OnEnemyDead);
            GameEvent.AddEventListener<Vector3, Quaternion>(ActorEventDefine.AsteroidExplosion, OnAsteroidExplosion);
            GameEvent.AddEventListener<Vector3, Quaternion>(ActorEventDefine.PlayerFireBullet, OnPlayerFireBullet);
            GameEvent.AddEventListener<Vector3, Quaternion>(ActorEventDefine.EnemyFireBullet, OnEnemyFireBullet);

            _steps = ESteps.Ready;
        }

        /// <summary>
        /// 销毁房间
        /// </summary>
        public void DestroyRoom()
        {
            // 加载背景音乐
            GameModule.Audio.Stop(AudioType.Music, true);

            // 监听游戏事件
            GameEvent.RemoveEventListener<Vector3, Quaternion>(ActorEventDefine.PlayerDead, OnPlayerDead);
            GameEvent.RemoveEventListener<Vector3, Quaternion>(ActorEventDefine.EnemyDead, OnEnemyDead);
            GameEvent.RemoveEventListener<Vector3, Quaternion>(ActorEventDefine.AsteroidExplosion, OnAsteroidExplosion);
            GameEvent.RemoveEventListener<Vector3, Quaternion>(ActorEventDefine.PlayerFireBullet, OnPlayerFireBullet);
            GameEvent.RemoveEventListener<Vector3, Quaternion>(ActorEventDefine.EnemyFireBullet, OnEnemyFireBullet);
        }

        #region 接收事件

        private void OnPlayerDead(Vector3 position, Quaternion rotation)
        {
        }

        private void OnEnemyDead(Vector3 position, Quaternion rotation)
        {
        }

        private void OnAsteroidExplosion(Vector3 position, Quaternion rotation)
        {
        }

        private void OnPlayerFireBullet(Vector3 position, Quaternion rotation)
        {
        }

        private void OnEnemyFireBullet(Vector3 position, Quaternion rotation)
        {
        }

        #endregion
    }
}