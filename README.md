# P2P-CHAT
 P2P-Chat is a simple chat using P2P (peer-to-peer) architecture with socket
 
## Peer-to-Peer
 Peer-to-peer is a computer network architecture where each of the points or nodes in the network works both as a client and as a server, allowing sharing of services and data without the need for a central server
 
<img src="https://www.gta.ufrj.br/ensino/eel878/redes1-2016-1/16_1/p2p/images/funcionamento.png" width="250">

## Commands

| Command | Description |
| ---- | ---- |
| `/help` | Shows the list of chat commands |
| `/clear` | Clear messages from the chat |
| `/nick` | Renames the user's nickname |
| `/quit` | Quit chat and close application |
| `/left` | Quit chat, close application and save messages history log |
| `/color` | Change the color of user messages |
| `/me` | Send a private message |

## Dependencies

packages.config

```xml
<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Newtonsoft.Json" version="13.0.1" targetFramework="net48" />
</packages>
```

## License

[MIT](https://github.com/Krauzy/p2p-chat/blob/main/LICENSE)
