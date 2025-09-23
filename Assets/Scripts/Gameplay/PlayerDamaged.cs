using Platformer.Core;
using Platformer.Model;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has been hurt.
    /// </summary>
    /// <typeparam name="PlayerDamaged"></typeparam>
    public class PlayerDamaged : Simulation.Event<PlayerDamaged>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;

            if (player.audioSource && player.ouchAudio)
                player.audioSource.PlayOneShot(player.ouchAudio);

            player.animator.SetTrigger("hurt");
            player.damageText.Display();
        }
    }
}