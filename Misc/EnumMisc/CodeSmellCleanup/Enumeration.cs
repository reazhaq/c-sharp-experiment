using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Misc.EnumMisc.CodeSmellCleanup;

/// <summary>
/// https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
/// </summary>
public abstract class Enumeration : IComparable, IEquatable<Enumeration>, IComparable<Enumeration>
{
    private readonly int value;
    private readonly string displayName;

    protected Enumeration(int value, string displayName)
    {
        this.value = value;
        this.displayName = displayName;
    }

    public int Value => value;

    public string DisplayName => displayName;

    public override string ToString() => displayName;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    {
        var type = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance = new T();
            var locatedValue = info.GetValue(instance) as T;

            if (locatedValue != null)
            {
                yield return locatedValue;
            }
        }
    }

    public bool Equals(Enumeration other)
    {
        if (other == null) return false;
        return value == other.value && displayName == other.displayName;
    }

    public override bool Equals(object obj) =>
        Equals(obj as Enumeration);

    public override int GetHashCode() => value.GetHashCode();

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Value - secondValue.Value);
        return absoluteDifference;
    }

    public static T FromValue<T>(int value) where T : Enumeration, new()
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Value == value);
        return matchingItem;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
    {
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
        return matchingItem;
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
        {
            var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
            throw new ApplicationException(message);
        }

        return matchingItem;
    }

    public int CompareTo(object other) =>
        CompareTo(other as Enumeration);

    public int CompareTo(Enumeration other) =>
        other is null ? 1 : Value.CompareTo(other.Value);
}
