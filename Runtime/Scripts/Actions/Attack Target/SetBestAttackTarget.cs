using Apex.Utilities;
using UCore.Entities;

/// <summary>
/// This AI action evaluates a list of options for best attack target, and sets the one scoring the highest
/// </summary>
public sealed class SetBestAttackTarget : ActionWithOptions<IEntity, ContextBase>
{
    public override void Execute(ContextBase context)
    {
        var entity = context.Entity;
        var observations = context.Memory.allObservations;
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

        entity.CurrentAttackTarget = best;

        ListBufferPool.ReturnBuffer(list);
    }
}
