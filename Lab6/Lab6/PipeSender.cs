namespace Lab6;
using System.IO.Pipes;

internal static class PipeSender
{
    public static async Task<string?> AsyncSend(string channelName, string channelMessage)
    {
        using NamedPipeClientStream stream = new NamedPipeClientStream(".", channelName, PipeDirection.InOut);
        await stream.ConnectAsync(5000);
        using StreamReader reader = new StreamReader(stream);
        using StreamWriter writer = new StreamWriter(stream);
        writer.AutoFlush = true;
        await writer.WriteLineAsync(channelMessage);
        return await reader.ReadLineAsync();
    }
}