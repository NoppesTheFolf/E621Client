using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json.Converters;
namespace Noppes.E621.Converters
{
    // Custom converter for Rectangle
    public class NotePositionConverter : JsonConverter<Note>
    {
        public override Note ReadJson(JsonReader reader, Type objectType, Note? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject token = JObject.Load(reader);
            var note = new Note();
            serializer.Populate(token.CreateReader(), note);
            int x = token["x"]!.Value<int>();
            int y = token["y"]!.Value<int>();
            int width = token["width"]!.Value<int>();
            int height = token["height"]!.Value<int>();
            note.Position = new Rectangle(x, y, width, height);
            return note;
        }

        public override void WriteJson(JsonWriter writer, Note? value, JsonSerializer serializer)
        {
            if (value == null) writer.WriteNull();
            else
            {
                var dateConverter = new IsoDateTimeConverter();
                var stringConverter = new EmptyStringConverter();

                writer.WriteStartObject(); // Start the JSON object

                // Manually write each property of the Note object
                writer.WritePropertyName("id");
                writer.WriteValue(value.Id);

                writer.WritePropertyName("created_at");
                dateConverter.WriteJson(writer, value.CreatedAt, serializer);

                writer.WritePropertyName("updated_at");
                dateConverter.WriteJson(writer, value.UpdatedAt, serializer);

                writer.WritePropertyName("creator_id");
                writer.WriteValue(value.CreatorId);

                writer.WritePropertyName("x");
                writer.WriteValue(value.Position.X);

                writer.WritePropertyName("y");
                writer.WriteValue(value.Position.Y);

                writer.WritePropertyName("width");
                writer.WriteValue(value.Position.Width);

                writer.WritePropertyName("height");
                writer.WriteValue(value.Position.Height);

                writer.WritePropertyName("version");
                writer.WriteValue(value.Version);

                writer.WritePropertyName("is_active");
                writer.WriteValue(value.IsActive);

                writer.WritePropertyName("post_id");
                writer.WriteValue(value.PostId);

                writer.WritePropertyName("body");
                stringConverter.WriteJson(writer, value.Body, serializer);

                writer.WritePropertyName("creator_name");
                stringConverter.WriteJson(writer, value.CreatorName, serializer);

                writer.WriteEndObject(); // End the JSON object
            }
        }
    }
}
