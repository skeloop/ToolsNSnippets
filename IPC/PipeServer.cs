using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;

public delegate void PipeStreamMessage(string message);

public class PipeServer
{
    public static event PipeStreamMessage? OnPipeStreamMessage;
    
    private NamedPipeServerStream? _pipeServer = null;

    public async Task StartAsync()
    {
        _pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.InOut);

        Console.WriteLine("Wartet auf Verbindung...");
        await _pipeServer.WaitForConnectionAsync();  // Asynchron auf die Verbindung warten

        try
        {
            Console.WriteLine("Verbindung hergestellt.");

            byte[] buffer = new byte[256];  // Puffer für empfangene Daten
            int bytesRead;

            // Nachrichtenempfang in einer Schleife
            while ((bytesRead = await _pipeServer.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("Empfangene Nachricht: " + message);
                OnPipeStreamMessage(message);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Fehler: " + e.Message);
        }
        finally
        {
            _pipeServer?.Dispose();
        }
    }

    // Diese Methode gibt an, ob der Pipe-Server aktiv ist
    public bool Active => _pipeServer != null && _pipeServer.IsConnected;

    // Eine Methode zum Lesen einer Nachricht (Asynchron)
    public async Task<string> ReadMessageAsync()
    {
        byte[] buffer = new byte[256];
        int bytesRead = await _pipeServer.ReadAsync(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
    }

    // Eine Methode zum Senden einer Nachricht an den Client
    public async Task SendMessageAsync(string message)
    {
        if (_pipeServer?.IsConnected == true)
        {
            byte[] response = Encoding.UTF8.GetBytes("Antwort von Server: " + message);
            await _pipeServer.WriteAsync(response, 0, response.Length);
            await _pipeServer.FlushAsync();  // Sicherstellen, dass die Nachricht gesendet wird
        }
    }
}
