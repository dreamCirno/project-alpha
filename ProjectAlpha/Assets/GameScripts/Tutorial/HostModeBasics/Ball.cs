using Fusion;

namespace Tutorial
{
    public class Ball : NetworkBehaviour
    {
        [Networked] private TickTimer life { get; set; }

        public void Init()
        {
            life = TickTimer.CreateFromSeconds(Runner, 5.0f);
        }

        public override void FixedUpdateNetwork()
        {
            if (life.Expired(Runner))
            {
                Runner.Despawn(Object);
                return;
            }

            var ballTransform = transform;
            ballTransform.position += 5 * ballTransform.forward * Runner.DeltaTime;
        }
    }
}