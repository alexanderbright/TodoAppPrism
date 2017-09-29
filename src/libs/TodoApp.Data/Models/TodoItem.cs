using System;
using SQLite;

namespace TodoApp.Data.Models
{
  [Table("TodoItem")]
  public class TodoItem : IEquatable<TodoItem>
  {
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public bool Done { get; set; }

    public TodoItem Clone()
    {
      return (TodoItem)MemberwiseClone();
    }

    public bool Equals(TodoItem other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Notes, other.Notes) && Done == other.Done;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((TodoItem) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        var hashCode = Id;
        hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (Notes != null ? Notes.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ Done.GetHashCode();
        return hashCode;
      }
    }
  }
}