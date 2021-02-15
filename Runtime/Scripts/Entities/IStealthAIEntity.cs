namespace UCore.Entities
{
    public interface IStealthAIEntity : IAIEntity
    {
        bool IsHiding { get; set; }
    }
}
