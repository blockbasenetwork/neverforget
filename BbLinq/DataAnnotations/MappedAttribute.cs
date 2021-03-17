using System;

namespace BlockBase.BBLinq.DataAnnotations
{
    /// <summary>
    /// For properties that can be mapped throught the to string (JSON P.E.)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MappedAttribute : BlockBaseDataAnnotationAttribute
    {

    }
}
