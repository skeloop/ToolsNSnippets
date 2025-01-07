using System;
using System.IO.Pipes;
using System.Text;

public class PipeClient
{
    NamedPipeClientStream pipeClient;

    public static bool Ping()
    {
        PipeClient client = new PipeClient();
        Console.WriteLine("Pipe |> Verbinden mit Server...");
        client.Start();

        // Nachricht an den Server senden
        client.Send("$ping");

        // Antwort vom Server empfangen
        string response = client.Read();
        if(response == "confirm") Console.WriteLine("Server erreichbar");
        // Verbindung schließen
        client.Close();
        return response == "confirm";
    }

    // Startet die Verbindung mit dem Pipe-Server
    public void Start()
    {
        try
        {
            pipeClient = new NamedPipeClientStream(".\\pipe\\GemeniPipe", "", PipeDirection.InOut);
            pipeClient.Connect(5000);  // Timeout von 5 Sekunden
            Console.WriteLine("Verbunden mit Server.");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Fehler bei der Verbindung zum Server: {ex.Message}");
        }
    }

    // Sendet eine Nachricht an den Pipe-Server
    public void Send(string message)
    {
        try
        {
            if(pipeClient.IsConnected)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                pipeClient.Write(buffer, 0, buffer.Length);
                pipeClient.Flush();  // Sicherstellen, dass die Daten gesendet werden
            }
            else
            {
                Console.WriteLine("Die Pipe ist nicht verbunden.");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Fehler beim Senden der Nachricht: {ex.Message}");
        }
    }

    // Liest die Antwort vom Pipe-Server
    public string Read()
    {
        try
        {
            if(pipeClient.IsConnected)
            {
                byte[] responseBuffer = new byte[256];
                int bytesRead = pipeClient.Read(responseBuffer, 0, responseBuffer.Length);
                string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesRead);
                Console.WriteLine("Empfangene Antwort: " + response);
                return response;
            }
            else
            {
                Console.WriteLine("Die Pipe ist nicht verbunden.");
                return string.Empty;
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Fehler beim Lesen der Antwort: {ex.Message}");
            return string.Empty;
        }
    }

    // Schließt die Pipe-Verbindung
    public void Close()
    {
        try
        {
            if(pipeClient != null && pipeClient.IsConnected)
            {
                pipeClient.Close();
                Console.WriteLine("Verbindung zur Pipe geschlossen.");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Fehler beim Schließen der Verbindung: {ex.Message}");
        }
    }
}
