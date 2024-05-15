using System.ComponentModel;

namespace CapitalPlacementTask.API.Enums
{
    public enum Gender
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female,
        [Description("I'd rather not say")]
        Other

    }
}
