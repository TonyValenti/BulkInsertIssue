using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp20 {
    public static class FrameEnumerableExtensions2 {

        [return: NotNullIfNotNull("This")]
        public static T? CopySourceToDest<T>(this T? This) where T : Frame {
            if (This is { }) {

                This.SerializeOriginalToFinal();
            }

            return This;
        }
    }
}