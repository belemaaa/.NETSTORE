using System;
using System.Runtime.Serialization;

namespace _netstore.Data
{
    public enum ProductType
    {
        [EnumMember(Value = "Shorts")]
        Shorts,

        [EnumMember(Value = "Jewelry")]
        Jewelry,

        [EnumMember(Value = "Sports")]
        Sports,

        [EnumMember(Value = "HomeAppliances")]
        HomeAppliances,

        [EnumMember(Value = "Electronics")]
        Electronics,

        [EnumMember(Value = "Womens")]
        Womens,

        [EnumMember(Value = "Mens")]
        Mens,

        [EnumMember(Value = "Kids")]
        Kids,

        [EnumMember(Value = "Toys")]
        Toys
    }
}
