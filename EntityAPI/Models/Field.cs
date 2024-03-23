using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public class Field
    {
        [Key] public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }

        public Guid Itemid { get; set; }
    }

    public class FieldDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }
        public Act Action { get; set; } = Act.None;
    }

    //public class FieldBuilder
    //{
    //    public static Field ToField(FieldDto fieldDto)
    //    {
    //        return new Field { Name = fieldDto.Name, Value = fieldDto.Value };
    //    }

    //    public static void Update(Field field, FieldDto fieldDto)
    //    {
    //        field.Name = fieldDto.Name;
    //        field.Value = fieldDto.Value;
    //    }
    //}
}
