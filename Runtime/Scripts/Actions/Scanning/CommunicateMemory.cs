using Apex.AI;
using Apex.Serialization;

public sealed class CommunicateMemory : ActionBase<EnemyContext>
{
    [ApexSerialization, FriendlyName("Require Can Communicate", "If set to true, only units whose 'canCommunicate' is true are allowed to communicate their observations")]
    public bool requireCanCommunicate = true;

    [ApexSerialization, FriendlyName("Max Communication Range", "How far away other entities can be before they can no longer receive communicated observations")]
    public float maxCommunicationRange = 25f;

    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;

        if (requireCanCommunicate && !entity.canCommunicate) {
            return;
        }

        var observations = context.memory.allObservations;
        var count = observations.Count;

        if (count == 0) {
            return;
        }

        var rangeSqr = maxCommunicationRange * maxCommunicationRange;
        for (int i = 0; i < count; i++) {
            var obs = observations[i];
            var e = obs.entity;

            if (e.type != entity.type) {
                continue;
            }

            if ((e.position - entity.position).sqrMagnitude > rangeSqr) {
                // Other entity is out of communication range
                continue;
            }

            if (e is IAIEntity aiEntity) {
                // Send all observations to other entity
                aiEntity.ReceiveCommunicatedMemory(observations);
            }
        }
    }
}
