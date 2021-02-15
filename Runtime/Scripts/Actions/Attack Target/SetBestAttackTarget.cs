using Apex.AI;
using Apex.Utilities;

/// <summary>
/// This AI action evaluates a list of options for best attack target, and sets the one scoring the highest
/// </summary>
public sealed class SetBestAttackTarget : ActionWithOptions<IEntity, EnemyContext>
{
    public override void Execute(EnemyContext context)
    {
        var entity = context.entity;
        var observations = context.memory.allObservations;
        var count = observations.Count;
        if (count == 0) {
            return;
        }

        var list = ListBufferPool.GetBuffer<IEntity>(5);

        for (int i = 0; i < count; i++) {
            var obs = observations[i];
            var ent = obs.entity;

            list.Add(ent);
        }

        var best = GetBest(context, list);
        if (best == null) {
            return;
        }

        entity.attackTarget = best;

        ListBufferPool.ReturnBuffer(list);
    }
}
