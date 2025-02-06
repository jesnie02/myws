using Fleck;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var server = new WebSocketServer("ws://0.0.0.0:8181");
var wsConnections = new List<IWebSocketConnection>();

server.Start(ws =>
{
    ws.OnOpen = () =>
    {
        wsConnections.Add(ws);
    };
    ws.OnMessage = message =>
    {
        foreach (var webSocketConnection in wsConnections)
        {
            webSocketConnection.Send(message);
        }
    };
});

app.Run();