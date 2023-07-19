using Microsoft.EntityFrameworkCore;

namespace ConsoleApp20 {
    public abstract class Frame { 
        public long Row { get; set; }
        public string Id { get; set; } = string.Empty;

        public abstract void SerializeOriginalToFinal();
    }

    public class Frame<T> : Frame where T : Packet, new() {
        public T Final { get; set; }
        public T Original { get; set; }

        public Frame() {
            Final = new();
            Original = new();
        }

        public override void SerializeOriginalToFinal() {
            //Final = (Original.Clone() as TFields) ?? Final;

            var Json = System.Text.Json.JsonSerializer.Serialize(Original);
            Final = System.Text.Json.JsonSerializer.Deserialize<T>(Json) ?? Final;

        }

    }

    [Owned]
    public abstract class OwnedMember {

    }

    public class Packet : OwnedMember {
        public string Status { get; set; } = string.Empty;
    }

    public class TestPacket1 : Packet {
        public string FirstName { get; set; } = string.Empty;
    }


    public class TestPacket2 : Packet {
        public string LastName { get; set; } = string.Empty;
    }
}