namespace Lab6;
using System.IO.Pipes;

internal static class PipeSender
{
    public static void Send(string channelName, string channelMessage)
    {
        using NamedPipeClientStream stream = new NamedPipeClientStream(".", channelName, PipeDirection.Out);
        stream.Connect(5000);
        using StreamWriter writer = new StreamWriter(stream);
        writer.AutoFlush = true;
        writer.WriteLine(channelMessage);
    }
}