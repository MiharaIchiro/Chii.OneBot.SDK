# Chii.OneBot.SDK

源由`Chii.Deskband`的特性，需要`.net framework`架構及反向ws，為了更佳的使用性，以`net462`為基礎，
在這些前提下對現有SDK進行參照，僅有[AuroraNative](https://github.com/timi137137/AuroraNative)符合需求，
但因`Console.ReadKey`等僅在Console下的特性所干預，暫以[cqhttp.WebSocketReverse.NETCore](https://github.com/cqbef/cqhttp.WebSocketReverse.NETCore)為基礎進行修改作暫時性使用，不排除改回使用AuroraNative。

注意：本項目暫基於`Chii.Deskband`的使用而建立，故可能無法對一般開發者提供更多合適的功能性擴展。