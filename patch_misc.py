files = {
    "Scripts/Items/Misc/OilCloth.cs": (
        b"""\t\tpublic bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tif ( Deleted || !from.CanSee( this ) )
\t\t\t\treturn false;

\t\t\tbase.ScissorHelper( from, new Bandage(), 1 );

\t\t\treturn true;
\t\t}""",
        b"""\t\tpublic bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tbase.ScissorHelper( from, new Bandage(), 1 );

\t\t\treturn true;
\t\t}"""
    ),
    "Scripts/Items/Body Parts/BonePile.cs": (
        b"""\t\tpublic bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tif ( Deleted || !from.CanSee( this ) )
\t\t\t\treturn false;

\t\t\tbase.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 10, 15 ) );

\t\t\treturn true;
\t\t}""",
        b"""\t\tpublic bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tbase.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 10, 15 ) );

\t\t\treturn true;
\t\t}"""
    ),
    "Scripts/Items/Body Parts/RibCage.cs": (
        b"""\t\tpublic bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tif ( Deleted || !from.CanSee( this ) )
\t\t\t\treturn false;

\t\t\tbase.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 3, 5 ) );

\t\t\treturn true;
\t\t}""",
        b"""\t\tpublic bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tbase.ScissorHelper( from, new Bone(), Utility.RandomMinMax( 3, 5 ) );

\t\t\treturn true;
\t\t}"""
    )
}

for path, (search, replace) in files.items():
    with open(path, "rb") as f:
        data = f.read()

    search2 = search.replace(b'\n', b'\r\n')
    replace2 = replace.replace(b'\n', b'\r\n')

    if search in data:
        data = data.replace(search, replace)
        with open(path, "wb") as f:
            f.write(data)
        print("Success1 " + path)
    elif search2 in data:
        data = data.replace(search2, replace2)
        with open(path, "wb") as f:
            f.write(data)
        print("Success2 " + path)
    else:
        print("Search string not found in " + path)
