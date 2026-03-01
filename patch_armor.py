with open("Scripts/Items/Armor/BaseArmor.cs", "rb") as f:
    data = f.read()

search = b"""\t\tpublic bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tif ( !IsChildOf( from.Backpack ) )
\t\t\t{
\t\t\t\tfrom.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
\t\t\t\treturn false;
\t\t\t}

\t\t\tCraftSystem system = DefTailoring.CraftSystem;"""

replace = b"""\t\tpublic bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tCraftSystem system = DefTailoring.CraftSystem;"""

search2 = search.replace(b'\n', b'\r\n')
replace2 = replace.replace(b'\n', b'\r\n')

if search in data:
    data = data.replace(search, replace)
    with open("Scripts/Items/Armor/BaseArmor.cs", "wb") as f:
        f.write(data)
    print("Success1 BaseArmor")
elif search2 in data:
    data = data.replace(search2, replace2)
    with open("Scripts/Items/Armor/BaseArmor.cs", "wb") as f:
        f.write(data)
    print("Success2 BaseArmor")
else:
    print("Search string not found in BaseArmor")

with open("Scripts/Items/Clothing/BaseClothing.cs", "rb") as f:
    data = f.read()

search = b"""\t\tpublic virtual bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tif ( !IsChildOf( from.Backpack ) )
\t\t\t{
\t\t\t\tfrom.SendLocalizedMessage( 502437 ); // Items you wish to cut must be in your backpack.
\t\t\t\treturn false;
\t\t\t}

\t\t\tCraftSystem system = DefTailoring.CraftSystem;"""

replace = b"""\t\tpublic virtual bool Scissor( Mobile from, Scissors scissors )
\t\t{
\t\t\tCraftSystem system = DefTailoring.CraftSystem;"""

search2 = search.replace(b'\n', b'\r\n')
replace2 = replace.replace(b'\n', b'\r\n')

if search in data:
    data = data.replace(search, replace)
    with open("Scripts/Items/Clothing/BaseClothing.cs", "wb") as f:
        f.write(data)
    print("Success1 BaseClothing")
elif search2 in data:
    data = data.replace(search2, replace2)
    with open("Scripts/Items/Clothing/BaseClothing.cs", "wb") as f:
        f.write(data)
    print("Success2 BaseClothing")
else:
    print("Search string not found in BaseClothing")
