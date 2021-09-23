using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;

namespace Chii.OneBot.SDK.WebSocket
{
    class Chunk<T> : ReadOnlySequenceSegment<T>
    {
        public Chunk(ReadOnlyMemory<T> memory)
        {
            Memory = memory;
        }
        public Chunk<T> Add(ReadOnlyMemory<T> mem)
        {
            var segment = new Chunk<T>(mem)
            {
                RunningIndex = RunningIndex + Memory.Length
            };

            Next = segment;
            return segment;
        }
    }

    sealed class WebSocketReceiveResultProcessor : IDisposable
    {
        Chunk<byte> startChunk = null;
        Chunk<byte> currentChunk = null;

        public WebSocketReceiveResultProcessor() { }

        public bool Receive(WebSocketReceiveResult result, ArraySegment<byte> buffer, out ReadOnlySequence<byte> frame)
        {
            if (result.EndOfMessage && result.MessageType == WebSocketMessageType.Close)
            {
                frame = default;
                return false;
            }
            var slice = buffer.AsMemory(0, result.Count);

            if (startChunk == null)
            {
                startChunk = currentChunk = new Chunk<byte>(slice);
            }
            else
            {
                currentChunk = currentChunk.Add(slice);
            }

            if (result.EndOfMessage && startChunk != null)
            {
                if (startChunk.Next == null)
                {
                    frame = new ReadOnlySequence<byte>(startChunk.Memory);
                }
                else
                {
                    frame = new ReadOnlySequence<byte>(startChunk, 0, currentChunk, currentChunk.Memory.Length);
                }
                startChunk = currentChunk = null;
                return true;
            }
            else
            {
                frame = default;
                return false;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
