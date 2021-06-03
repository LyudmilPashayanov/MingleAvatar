
using System.Text.Json;
using System.IO;
using Mingle.Repository;

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
