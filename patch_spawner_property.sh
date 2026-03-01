sed -i '/m_MobilesSeekHome = value;/!b;n;n;a\
\
\t\t[CommandProperty( AccessLevel.GameMaster )]\r\n\t\tpublic bool ItemDecays\r\n\t\t{\r\n\t\t\tget { return m_ItemDecays; }\r\n\t\t\tset { m_ItemDecays = value; InvalidateProperties(); }\r\n\t\t}' Scripts/Engines/Spawner/Spawner.cs
