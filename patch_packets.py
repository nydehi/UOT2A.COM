import re

with open('Scripts/Engines/RemoteAdmin/Packets.cs', 'rb') as f:
    content = f.read()

content = content.replace(
b"\t\t\tEnsureCapacity( 1 + 2 + (10*4) + netVer.Length+1 + os.Length+1 );",
b"\t\t\tEnsureCapacity( 1 + 2 + (11*4) + netVer.Length+1 + os.Length+1 );"
)

content = content.replace(
b"\t\t\tm_Stream.Write( (uint) GC.GetTotalMemory( false ) );                        // TODO: uint not sufficient for TotalMemory (long). Fix protocol.",
b"\t\t\tm_Stream.Write( (ulong) GC.GetTotalMemory( false ) );"
)

content = content.replace(
b"\t\t\tlong memory = GC.GetTotalMemory( false );\n\t\t\tm_Stream.Write( (uint)(memory >> 32) );                                   // Memory high bytes\n\t\t\tm_Stream.Write( (uint)memory );                                           // Memory low bytes",
b"\t\t\tlong memory = GC.GetTotalMemory( false );\n\t\t\tm_Stream.Write( (ulong)memory );                                          // Memory"
)

with open('Scripts/Engines/RemoteAdmin/Packets.cs', 'wb') as f:
    f.write(content)
