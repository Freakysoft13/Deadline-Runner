public class SlowMovement : Effect {

    private float originalSpeed;

    public override void ApplyEffect(Player player) {
        originalSpeed = player.speed;
        player.speed /= 2;
    }

    public override void RemoveEffect(Player player) {
        player.speed = originalSpeed;
    }
}
