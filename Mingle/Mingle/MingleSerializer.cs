using Mingle.Entinies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Mingle.Services;

namespace Mingle
{
    public static class MingleSerializer
    {
        static string FileName = @".\SavedAvatars.json";

        public static AvatarsRepository DeserializeAvatars() 
        {
            if (File.Exists(FileName))
            {
                AvatarsRepository avatars = JsonSerializer.Deserialize<AvatarsRepository>(File.ReadAllText(FileName));
                return avatars;
            }
            return null;
        }

        public static void SerializeAvatars(AvatarsRepository repo) 
        {
            string jsonString = JsonSerializer.Serialize(repo);
            File.WriteAllText(FileName, jsonString);
        }
    }
}
