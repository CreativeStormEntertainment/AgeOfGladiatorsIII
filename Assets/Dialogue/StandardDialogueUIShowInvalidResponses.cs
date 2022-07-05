using UnityEngine;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class StandardDialogueUIShowInvalidResponses : StandardDialogueUI
{

    public string showInvalidFieldName = "Show Invalid";

    public override void ShowResponses(Subtitle subtitle, Response[] responses, float timeout)
    {
        responses = CheckInvalidResponses(responses);
        base.ShowResponses(subtitle, responses, timeout);
    }

    private Response[] CheckInvalidResponses(Response[] responses)
    {
        if (!HasAnyInvalid(responses)) return responses; // If no invalid responses, we can skip the special code.
        var list = new List<Response>();
        for (int i = 0; i < responses.Length; i++)
        {
            var response = responses[i];
            //--- Was: if (response.enabled || Field.LookupBool(response.destinationEntry.fields, showInvalidFieldName))
            //--- To allow runtime changes, we need to use a more sophisticated AllowShowInvalid() method:
            if (response.enabled || AllowShowInvalid(response))
            {
                list.Add(response);
            }
        }
        return list.ToArray();
    }

    private bool HasAnyInvalid(Response[] responses)
    {
        if (responses == null) return false;
        for (int i = 0; i < responses.Length; i++)
        {
            if (!responses[i].enabled) return true;
        }
        return false;
    }

    private bool AllowShowInvalid(Response response)
    {
        // See if this response has a "Show Invalid" field in Lua:
        var luaResult = Lua.Run("return Dialog[" + response.destinationEntry.id + "].Show_Invalid");
        if (luaResult.Equals(Lua.noResult) || luaResult.asString == "nil")
        {
            // If not, return the design-time Show Invalid field value from the database:
            return Field.LookupBool(response.destinationEntry.fields, showInvalidFieldName);
        }
        else
        {
            // Otherwise return the runtime Lua value:
            return luaResult.asBool;
        }
    }

}
