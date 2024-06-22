using System.Runtime.Serialization;

namespace Domain;
public interface IEntity
{
    public Guid Id { get; set; }
}

[Serializable]
public class EntityNotFoundException : Exception
{
    public Type Type { get; }
    public Guid Id { get; }

    public EntityNotFoundException(Type type, Guid id) : base(GetMessage(type, id))
    {
        Type = type;
        Id = id;
    }

    public EntityNotFoundException(Type type, Guid id, Exception inner) : base(GetMessage(type, id), inner)
    {
        Type = type;
        Id = id;
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        if (info == null)
        {
            throw new ArgumentNullException(nameof(info));
        }

        Type = (Type)(info.GetValue(nameof(Type), typeof(Type)) ?? typeof(object));
        Id = (Guid)(info.GetValue(nameof(Id), typeof(Guid)) ?? default(Guid));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        if (info == null)
        {
            throw new ArgumentNullException(nameof(info));
        }

        info.AddValue(nameof(Type), Type, typeof(Type));
        info.AddValue(nameof(Id), Id, typeof(Guid));
    }

    private static string GetMessage(Type type, Guid id) => $"{type.Name} not found: {id}";
}
