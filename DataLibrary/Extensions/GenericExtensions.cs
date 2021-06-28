using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace DataLibrary.Extensions
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Format indent
        /// Prevent Self referencing loop detected
        /// </summary>
        private static JsonSerializerSettings SettingsIgnoreReferenceLooping() => new()
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        /// <summary>
        /// Format indent
        /// </summary>
        /// <returns></returns>
        private static JsonSerializerSettings SettingsDefault() => new() {Formatting = Formatting.Indented};

        /// <summary>
        /// Convert <see cref="TModel"/> to json file
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        public static void ModeListToJson<TModel>(this List<TModel> list, string fileName)
        {
            JsonConvert.DefaultSettings = SettingsIgnoreReferenceLooping;
            var json = JsonConvert.SerializeObject(list);

            File.WriteAllText(fileName, json);
        }

    }
}