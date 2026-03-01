with open("Scripts/Items/Skill Items/Tailor Items/Misc/Scissors.cs", "rb") as f:
    data = f.read()

search = b"""\t\tpublic static bool CanScissor( Mobile from, IScissorable obj )
\t\t{
\t\t\tif ( obj is Item && ( (Item)obj ).Nontransferable )
\t\t\t{
\t\t\t\tfrom.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
\t\t\t\treturn false;
\t\t\t}

\t\t\t// TODO: Move other general checks from the different implementations here

\t\t\treturn true;
\t\t}"""

replace = b"""\t\tpublic static bool CanScissor( Mobile from, IScissorable obj )
\t\t{
\t\t\tif ( obj is Item )
\t\t\t{
\t\t\t\tItem item = (Item)obj;

\t\t\t\tif ( item.Deleted || !from.CanSee( item ) )
\t\t\t\t\treturn false;

\t\t\t\tif ( !item.IsChildOf( from.Backpack ) )
\t\t\t\t{
\t\t\t\t\tfrom.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
\t\t\t\t\treturn false;
\t\t\t\t}

\t\t\t\tif ( item.Nontransferable )
\t\t\t\t{
\t\t\t\t\tfrom.SendLocalizedMessage( 502440 ); // Scissors can not be used on that to produce anything.
\t\t\t\t\treturn false;
\t\t\t\t}
\t\t\t}

\t\t\treturn true;
\t\t}"""

search2 = search.replace(b'\n', b'\r\n')
replace2 = replace.replace(b'\n', b'\r\n')

if search in data:
    data = data.replace(search, replace)
    with open("Scripts/Items/Skill Items/Tailor Items/Misc/Scissors.cs", "wb") as f:
        f.write(data)
    print("Success1")
elif search2 in data:
    data = data.replace(search2, replace2)
    with open("Scripts/Items/Skill Items/Tailor Items/Misc/Scissors.cs", "wb") as f:
        f.write(data)
    print("Success2")
else:
    print("Search string not found")
