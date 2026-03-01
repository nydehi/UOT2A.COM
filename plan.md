1. **Understand Context**: In `Scripts/Items/Skill Items/Magical/Misc/PotionKeg.cs`, the `OnDoubleClick` method handles the action of double-clicking a potion keg. If the user doesn't have an empty bottle in their backpack (`pack.ConsumeTotal(typeof(Bottle), 1)` returns false), there's a `// TODO: Target a bottle` comment. The objective is to implement targeting an empty bottle to fill it.

2. **Target Implementation**:
   - Create a nested `InternalTarget` class inside `PotionKeg` inheriting from `Target`.
   - The constructor will take `PotionKeg keg` as an argument.
   - In `OnTarget(Mobile from, object targeted)`, check if `targeted` is a `Bottle`.
   - If it is a `Bottle`:
     - Validate distance and accessibility.
     - Check `keg.Held > 0`.
     - Consume 1 `Bottle`.
     - Fill the bottle by calling `keg.FillBottle()`.
     - Try dropping the filled potion in the backpack. If successful, decrement `Held`, play sound, send messages.
     - If backpack full, send message, and drop to world or delete potion (existing code drops/deletes, check logic). Actually existing code says: `from.SendLocalizedMessage( 502244 ); // ...but there is no room for the bottle in your backpack.` and `pot.Delete();`.
   - If `targeted` is a `PotionKeg` check if its the same keg or whatever.
   - If `targeted` is not a `Bottle`, send an appropriate localized message if there is one (or just do nothing). Actually, look at the message numbers: 502227 might be "You must target an empty bottle". Let's check localization messages or just follow typical UO patterns. Wait, actually we can just look at `Target` logic.

Let me refine the target logic:
```csharp
		private class InternalTarget : Target
		{
			private PotionKeg m_Keg;

			public InternalTarget( PotionKeg keg ) : base( 2, false, TargetFlags.None )
			{
				m_Keg = keg;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Keg.Deleted || m_Keg.Held <= 0 )
					return;

				if ( targeted is Bottle )
				{
					if ( m_Keg.IsChildOf( from.Backpack ) || from.InRange( m_Keg.GetWorldLocation(), 2 ) )
					{
						Item item = (Item)targeted;

						if ( item.IsChildOf( from.Backpack ) || from.InRange( item.GetWorldLocation(), 2 ) )
						{
							if ( item.Amount > 0 )
							{
                                item.Consume( 1 );

								from.SendLocalizedMessage( 502242 ); // You pour some of the keg's contents into an empty bottle...

								BasePotion pot = m_Keg.FillBottle();

								Container pack = from.Backpack;

								if ( pack != null && pack.TryDropItem( from, pot, false ) )
								{
									from.SendLocalizedMessage( 502243 ); // ...and place it into your backpack.
									from.PlaySound( 0x240 );

									if ( --m_Keg.Held == 0 )
										from.SendLocalizedMessage( 502245 ); // The keg is now empty.
								}
								else
								{
									from.SendLocalizedMessage( 502244 ); // ...but there is no room for the bottle in your backpack.
									pot.Delete();
								}
							}
						}
						else
						{
							from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
						}
					}
					else
					{
						from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
					}
				}
				else
				{
					from.SendLocalizedMessage( 502227 ); // That cannot be used to hold a potion. (Wait, let's verify if 502227 is the correct message)
				}
			}
		}
```
Wait, let's check what 502227 is.
