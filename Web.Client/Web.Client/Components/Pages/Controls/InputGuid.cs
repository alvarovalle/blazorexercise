using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Web.Client.Components.Pages.Controls;

public partial class InputGuid : InputBase<Guid>
{
    public ElementReference? Element { get; protected set; }

    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out Guid result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            result = new Guid();
            validationErrorMessage = null;
            return true;
        }

        if (Guid.TryParse(value, out Guid guidValue))
        {
            result = guidValue;
            validationErrorMessage = null;
            return true;
        }
        else
        {
            result = new Guid();
            validationErrorMessage = "The value could not be parsed to a Guid.";
            return false;
        }
    }

    protected override string FormatValueAsString(Guid value)
    {
        // Converts the Guid? to string, handling null appropriately
        return value.ToString() ?? string.Empty;
    }

    private string GetDebuggerDisplay()
    {
        return Element.Value.ToString();
    }
}

