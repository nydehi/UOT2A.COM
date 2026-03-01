sed -i 's/\/\/ TODO: Make item decay an option on the spawner/if ( Spawner != null )\r\n\t\t\t\t\treturn (Movable \&\& Visible \&\& Spawner.ItemDecays);/' Server/Item.cs
sed -i 's/return (Movable && Visible\/\* && Spawner == null\*\/);/return (Movable \&\& Visible);/' Server/Item.cs
