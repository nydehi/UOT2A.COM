sed -i 's/if ( version < 3 && Weight == 0 )/if ( version < 7 )\r\n\t\t\t\tm_ItemDecays = true;\r\n\r\n\t\t\tif ( version < 3 \&\& Weight == 0 )/' Scripts/Engines/Spawner/Spawner.cs
