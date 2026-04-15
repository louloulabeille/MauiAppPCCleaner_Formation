using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MauiAppPCCleaner_Formation.Infrastructure.System
{
    public class JsonOption
    {
        public static JsonSerializerOptions GetJsonOptions()
        {
            JsonSerializerOptions option = new()
            {
                PropertyNameCaseInsensitive = true,
            };

            return option;
        }

    }
}
