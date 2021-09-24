# Chii.OneBot.SDK

源由`Chii.Deskband`的特性，需要`.net framework`架构及反向ws，为了更佳的使用性，以`net462`为基础，
在这些前提下对现有SDK进行参照，仅有[AuroraNative](https://github.com/timi137137/AuroraNative)符合需求，
但因`Console.ReadKey`等仅在Console下的特性所干预，暂以[cqhttp.WebSocketReverse.NETCore](https://github.com/cqbef/cqhttp.WebSocketReverse.NETCore)为基础进行修改作暂时性使用，不排除改回使用AuroraNative。

注意：本项目暂基于`Chii.Deskband`的使用而建立，故可能无法对一般开发者提供更多合适的功能性扩展。