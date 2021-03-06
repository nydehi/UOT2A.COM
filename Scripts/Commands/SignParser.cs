using System.Collections.Generic;
using System.IO;
using Server.Items;

namespace Server.Commands
{
    public class SignParser
	{
		private class SignEntry
		{
			public string m_Text;
			public Point3D m_Location;
			public int m_ItemID;
			public int m_Map;

			public SignEntry( string text, Point3D pt, int itemID, int mapLoc )
			{
				m_Text = text;
				m_Location = pt;
				m_ItemID = itemID;
				m_Map = mapLoc;
			}
		}

		public static void Initialize()
		{
			CommandSystem.Register( "SignGen", AccessLevel.Administrator, new CommandEventHandler( SignGen_OnCommand ) );
		}

		[Usage( "SignGen" )]
		[Description( "Generates world/shop signs on all facets." )]
		public static void SignGen_OnCommand( CommandEventArgs c )
		{
			Parse( c.Mobile );
		}

		public static void Parse( Mobile from )
		{
			string cfg = Path.Combine( Core.BaseDirectory, "Data/signs.cfg" );

			if ( File.Exists( cfg ) )
			{
				from.SendMessage( "Generating signs, please wait." );

				using ( StreamReader ip = new StreamReader( cfg ) )
				{
					string line;

					while ( (line = ip.ReadLine()) != null )
					{
						string[] split = line.Split( ' ' );

						SignEntry e = new SignEntry(
							line.Substring( split[0].Length + 1 + split[1].Length + 1 + split[2].Length + 1 + split[3].Length + 1 + split[4].Length + 1 ),
							new Point3D( Utility.ToInt32( split[2] ), Utility.ToInt32( split[3] ), Utility.ToInt32( split[4] ) ),
							Utility.ToInt32( split[1] ), Utility.ToInt32( split[0] ) );

                        Add_Static(e.m_ItemID, e.m_Location, Map.Felucca, e.m_Text);

                    }
                }

				from.SendMessage( "Sign generating complete." );
			}
			else
			{
				from.SendMessage( "{0} not found!", cfg );
			}
		}

		private static Queue<Item> m_ToDelete = new Queue<Item>();

		public static void Add_Static( int itemID, Point3D location, Map map, string name )
		{
			IPooledEnumerable eable = map.GetItemsInRange( location, 0 );

			foreach ( Item item in eable )
			{
				if ( item is Sign && item.Z == location.Z && item.ItemID == itemID )
					m_ToDelete.Enqueue( item );
			}

			eable.Free();

			while ( m_ToDelete.Count > 0 )
				m_ToDelete.Dequeue().Delete();

			Item sign;

			if ( name.StartsWith( "#" ) )
			{
				sign = new LocalizedSign( itemID, Utility.ToInt32( name.Substring( 1 ) ) );
			}
			else
			{
				sign = new Sign( itemID );
				sign.Name = name;
			}

			sign.MoveToWorld( location, map );
		}
	}
}