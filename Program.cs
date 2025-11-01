using System;
using Vosk;
using NAudio.Wave;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Vosk.Vosk.SetLogLevel(0);

        var model = new Model("model");
        var rec = new VoskRecognizer(model, 16000.0f);

        var waveIn = new WaveInEvent
        {
            WaveFormat = new WaveFormat(16000, 1)
        };

        waveIn.DataAvailable += (s, a) =>
        {
            if (rec.AcceptWaveform(a.Buffer, a.BytesRecorded))
                Console.WriteLine(rec.Result());
            else
                Console.WriteLine(rec.PartialResult());
        };

        waveIn.StartRecording();
        Console.WriteLine("🎤 Konuşmaya başlayabilirsin (çıkmak için 'q' tuşuna bas)...");

        while (Console.ReadKey(true).Key != ConsoleKey.Q)
            Thread.Sleep(100);

        waveIn.StopRecording();
        Console.WriteLine("Program sonlandırıldı.");
    }
}
