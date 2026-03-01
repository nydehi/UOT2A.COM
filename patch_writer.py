import re

with open('Server/Network/PacketWriter.cs', 'r') as f:
    content = f.read()

new_content = content.replace(
"""\t\tpublic void Write( uint value )
\t\t{
\t\t\tm_Buffer[0] = (byte)(value >> 24);
\t\t\tm_Buffer[1] = (byte)(value >> 16);
\t\t\tm_Buffer[2] = (byte)(value >>  8);
\t\t\tm_Buffer[3] = (byte) value;

\t\t\tm_Stream.Write( m_Buffer, 0, 4 );
\t\t}

\t\t/// <summary>
\t\t/// Writes a sequence of bytes to the underlying stream""",
"""\t\tpublic void Write( uint value )
\t\t{
\t\t\tm_Buffer[0] = (byte)(value >> 24);
\t\t\tm_Buffer[1] = (byte)(value >> 16);
\t\t\tm_Buffer[2] = (byte)(value >>  8);
\t\t\tm_Buffer[3] = (byte) value;

\t\t\tm_Stream.Write( m_Buffer, 0, 4 );
\t\t}

\t\t/// <summary>
\t\t/// Writes an 8-byte signed integer value to the underlying stream.
\t\t/// </summary>
\t\tpublic void Write( long value )
\t\t{
\t\t\tm_Buffer[0] = (byte)(value >> 56);
\t\t\tm_Buffer[1] = (byte)(value >> 48);
\t\t\tm_Buffer[2] = (byte)(value >> 40);
\t\t\tm_Buffer[3] = (byte)(value >> 32);
\t\t\tm_Buffer[4] = (byte)(value >> 24);
\t\t\tm_Buffer[5] = (byte)(value >> 16);
\t\t\tm_Buffer[6] = (byte)(value >>  8);
\t\t\tm_Buffer[7] = (byte) value;

\t\t\tm_Stream.Write( m_Buffer, 0, 8 );
\t\t}

\t\t/// <summary>
\t\t/// Writes an 8-byte unsigned integer value to the underlying stream.
\t\t/// </summary>
\t\tpublic void Write( ulong value )
\t\t{
\t\t\tm_Buffer[0] = (byte)(value >> 56);
\t\t\tm_Buffer[1] = (byte)(value >> 48);
\t\t\tm_Buffer[2] = (byte)(value >> 40);
\t\t\tm_Buffer[3] = (byte)(value >> 32);
\t\t\tm_Buffer[4] = (byte)(value >> 24);
\t\t\tm_Buffer[5] = (byte)(value >> 16);
\t\t\tm_Buffer[6] = (byte)(value >>  8);
\t\t\tm_Buffer[7] = (byte) value;

\t\t\tm_Stream.Write( m_Buffer, 0, 8 );
\t\t}

\t\t/// <summary>
\t\t/// Writes a sequence of bytes to the underlying stream"""
)

with open('Server/Network/PacketWriter.cs', 'w') as f:
    f.write(new_content)
