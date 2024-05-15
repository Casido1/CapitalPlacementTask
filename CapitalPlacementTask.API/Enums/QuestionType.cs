using System.ComponentModel;

namespace CapitalPlacementTask.API.Enums
{
    public enum QuestionType
    {
        [Description("Paragraph")]
        Paragragh = 1,
        [Description("Yes/No")]
        YesNo,
        [Description("DropDown")]
        DropDown,
        [Description("MultipleChoice")]
        MultipleChoice,
        [Description("Date")]
        Date,
        [Description("Number")]
        Number
    }
}
